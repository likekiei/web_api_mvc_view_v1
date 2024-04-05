using Jose;
using Main_Common.Enum.E_ProjectType;
using Main_Common.Enum.E_StatusType;
using Main_Common.Model.Account;
using Main_Common.Model.Data.Connect;
using Main_Common.Model.Data.Log;
using Main_Common.Model.Main;
using Main_Common.Model.Result;
using Main_Common.Model.ResultApi;
using Main_Service.Service.S_Company;
using Main_Service.Service.S_Log;
using Main_Service.Service.S_Login;
using Main_Service.Service.S_User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Net.Http;
using System.Text;
using Web_API.Filters;

namespace Web_API.Controllers
{
    /// <summary>
    /// 登入相關
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        #region == 【DI注入用宣告】 ==
        /// <summary>
        /// 【DTO】主系統資料
        /// </summary>
        public readonly MainSystem_DTO _MainSystem_DTO;
        /// <summary>
        /// 【Main Service】公司相關
        /// </summary>
        public readonly CompanyService_Main _CompanyService_Main;
        /// <summary>
        /// 【Main Service】Log相關
        /// </summary>
        public readonly LogService_Main _LogService_Main;
        /// <summary>
        /// 【Main Service】登入相關
        /// </summary>
        public readonly LoginService_Main _LoginService_Main;
        /// <summary>
        /// 【Main Service】使用者相關
        /// </summary>
        public readonly UserService_Main _UserService_Main;
        #endregion

        #region == 【全域宣告】 ==
        // ...
        #endregion

        #region == 【建構】 ==
        /// <summary>
        /// 建構
        /// </summary>
        /// <param name="httpContextAccessor">HttpContext</param>
        ///// <param name="mainSystem_DTO">主系統資料</param>
        /// <param name="companyService_Main">公司相關</param>
        ///// <param name="logService_Main">Log相關</param>
        /// <param name="loginService_Main">登入相關</param>
        /// <param name="userService_Main">使用者相關</param>
        public LoginController(IHttpContextAccessor httpContextAccessor,
            MainSystem_DTO mainSystem_DTO,
            CompanyService_Main companyService_Main,
            LogService_Main logService_Main,
            LoginService_Main loginService_Main,
            UserService_Main userService_Main)
        //    : base(httpContextAccessor)
        {
            this._MainSystem_DTO = mainSystem_DTO;
            this._CompanyService_Main = companyService_Main;
            this._LogService_Main = logService_Main;
            this._LoginService_Main = loginService_Main;
            this._UserService_Main = userService_Main;
        }
        #endregion

        //--【方法】=================================================================================

        /// <summary>
        /// 取得登入結果及Token
        /// </summary>
        /// <param name="input">登入資料</param>
        /// <returns></returns>      
        [HttpPost]
        [TypeFilter(typeof(Logs))]
        public Login_Token GetToken(Api_Login_Model input)
        {
            var Login_Time_unitl = Convert.ToDateTime(DateTime.UtcNow.AddHours(9).ToString()); //當前時間(不含毫秒)
            //var todayFull = DateTime.UtcNow.AddHours(8); //當前時間(含毫秒)
            var secretUser = "ATTN_APIKey"; // ConfigurationManager.AppSettings["TokenKey"]; //加密Key
            var result = new Login_Token(true); // 預設失敗

            var cookie_KeepDay = 1;
            var cDate = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString()); //Cookie存活時間(開始)
            var dDate = cDate.Date.AddDays(cookie_KeepDay); //Cookie存活時間(結束)

            UserSession_Model userInfo = null; //使用者資訊
                                               //Connect_DTO connectDTO_Main = null; //連線資訊(Main DB)

            #region == 取公司 (Error Return) ==
            //_CompanyService_Main = new CompanyService_Main();

            //// 預設的公司
            //var company_DTO = _CompanyService_Main.GetEditModel_Company(1);
            //// [條件：是否有值][T：無值]
            //if (company_DTO == null)
            //{
            //    return new Login_Token()
            //    {
            //        Result = false,
            //        Message = "【登入失敗】查無預設的公司[0000]，請確認「公司維護」的資料正確",
            //    };
            //}
            #endregion

            #region == 取登入者資訊 (Error Return) ==
            //_User_Service_Main = new User_Service_Main();

            // 查詢值
            var queryUser = new Login_Model
            {
                //Company_ID = company_DTO.ID,
                Account = input.Account,
                Password = input.Password,
                //Is_NeedCheckPassword = true,
            };

            // 取使用者
            //var Users = _User_Repository.GetAll(); //.ToList();// .GetAlls();
            userInfo = _UserService_Main.Get_User_Info(queryUser);
            // [條件：是否有值][T：無值]
            if (userInfo == null)
            {
                return new Login_Token()
                {
                    Result = false,
                    Message = $"【登入失敗】無法登入的使用者[{input.Account}]，請確認「使用者維護」的資料正確",
                };
            }
            else
            {
                //userInfo.Login_KeepDay = cookie_KeepDay;
                //userInfo.LoginFrom_Type_ID = E_LoginFrom_Type.Api;

                try
                {
                    var payload = new Token_Model
                    {
                        Login_ID = userInfo.LoginId,
                        Login_Name = input.Account,
                        Result = true,
                        Message = "登入成功",
                        Login_Time = Login_Time_unitl
                    };
                    //登入者資訊
                    //Session["userInfo"] = input.Account;
                    //UserSessionModel
                    result.Token = JWT.Encode(payload, Encoding.UTF8.GetBytes(secretUser), JwsAlgorithm.HS256);
                    result.Result = true;
                    result.Message = "【登入成功】";
                }
                catch (Exception ex)
                {
                    return new Login_Token()
                    {
                        Result = false,
                        Message = $"【登入失敗】產生Token失敗",
                    };
                }
            }
            #endregion

            #region == 登入紀錄處理 (Error Return) ==
            #region == 參數 ==
            var methodParam = new MethodParameter(Guid.NewGuid()); // 方法的通用屬性參數
            this._MainSystem_DTO.MethodBase = System.Reflection.MethodBase.GetCurrentMethod(); // 方法Info
            this._MainSystem_DTO.BindKey_ByException = methodParam.BindKey;
            this._MainSystem_DTO.Set_LogConfig(E_DBTable.Login_Record, E_LogType.執行紀錄, E_Action.登入);
            //input.BindKey = methodParam.BindKey; // 綁定Key
            //UserSession_Model userInfo = null; //使用者資訊
            #endregion

            // 添加執行Log
            this._LogService_Main.Add_LogActionRecord(this._MainSystem_DTO.MethodBase, methodParam.BindKey, methodParam.TodayFull);



            // 放入連線資訊
            //userInfo.ConnectDTO_Main = connectDTO_Main;
            // 放入其他資訊
            //userInfo.Bind_Key = Guid.NewGuid();
            //userInfo.Login_KeepDay = cookie_KeepDay;
            //userInfo.LoginFrom_Type_ID = E_LoginFrom_Type.Api;

            // 登入紀錄結果
            //var login_Result = _LoginService_Main.Create_LoginStatus(userInfo);
            // [條件：處理結果][T：成功][F：失敗]
            if (userInfo!=null)
            {
                #region == 產生Token ==
                // [條件：是否登入成功][T：成功][F：失敗]
                if (result.Result)
                {
                    try
                    {
                        var payload = new Token_Model
                        {
                            Login_ID = userInfo.LoginId,
                            Login_Name = input.Account,
                            Result = true,
                            Message = "登入成功",
                            Login_Time = Login_Time_unitl
                        };
                        //登入者資訊
                        //Session["userInfo"] = input.Account;
                        //UserSessionModel
                        result.Token = JWT.Encode(payload, Encoding.UTF8.GetBytes(secretUser), JwsAlgorithm.HS256);
                        result.Result = true;
                        result.Message = "【登入成功】";
                    }
                    catch (Exception ex)
                    {
                        return new Login_Token()
                        {
                            Result = false,
                            Message = $"【登入失敗】產生Token失敗",
                        };
                    }
                }
                else
                {
                    // 
                }
                #endregion

                #region == 【Log】寫入 (只記錄成功的) ==
                //try
                //{
                //    _Log_Service_Main = new Log_Service_Main();

                //    var logDTO = new Log_DTO
                //    {
                //        IsSuccess = true,
                //        Is_BackDoor = userInfo.Is_BackDoor,
                //        DB_Table_ID = E_DB_Table.Login_Record,
                //        Action_ID = E_Action.登入,
                //        StatusCode_ID = E_StatusCode.成功,
                //        //Table_Key_Main = $"[Connect_Group_ID:{userInfo.Connect_Group_ID}]",
                //        Message = "【登入】",
                //        Function_Path = "Main_Web/AccountController/Login",
                //        Log_Date = todayFull,
                //        Action_Date = todayFull,
                //    };

                //    #region == 深複製一個userInfo (Log用) ==
                //    // 深複製一個userInfo，避免此區操作影響到原資料
                //    var copy_userInfo = userInfo.CopyModel();
                //    // 結果DTO(Log需要，故額外建構)
                //    var resultTemp = new ResultOutput_Data<UserSessionModel>(true, copy_userInfo);
                //    //不要記錄到連線資訊
                //    copy_userInfo.ConnectDTO_Main = null;
                //    #endregion

                //    //填入json資料(請求)
                //    _Log_Service_Main.Set_DataJson_Query<Login_Model>(ref logDTO, queryUser);
                //    //填入json資料(回傳)
                //    _Log_Service_Main.Set_DataJson_Result<ResultOutput_Data<UserSessionModel>>(ref logDTO, resultTemp);
                //    //寫入Log
                //    _Log_Service_Main.Create_Log(logDTO);
                //}
                //catch (Exception)
                //{
                //    // 不處理例外
                ////}
                #endregion
            }
            else
            {
                return new Login_Token()
                {
                    Result = false,
                    Message = $"【登入失敗】", /*{login_Result.Message}".Replace("\r\n", ""),*/
                };
            }
            #endregion

            return result;
        }

        

    }
}
