using Jose;
using Main_Common.Enum.E_ProjectType;
using Main_Common.Enum.E_StatusType;
using Main_Common.ExtensionMethod;
using Main_Common.Model.Account;
using Main_Common.Model.Data.Company;
using Main_Common.Model.Data.Connect;
using Main_Common.Model.Data.User;
using Main_Common.Model.Main;
using Main_Common.Model.Message;
using Main_Common.Model.Result;
using Main_Common.Model.ResultApi;
using Main_Common.Tool;
using Main_EF.Interface;
using Main_EF.Migrations;
using Main_EF.Table;
using Main_Service.Service.S_Log;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Service.Service.S_Login
{
    /// <summary>
    /// 【Main】登入相關
    /// </summary>
    public class LoginService_Main
    {
        #region == 【DI注入用宣告】 ==
        /// <summary>
        /// 資料庫工作單元
        /// </summary>
        public readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// 【DTO】主系統資料
        /// </summary>
        public readonly MainSystem_DTO _MainSystem_DTO;
        /// <summary>
        /// 【Main Service】Log相關
        /// </summary>
        public readonly LogService_Main _LogService_Main;
        ///// <summary>
        ///// 【Main Service】登入相關
        ///// </summary>
        //public readonly LoginService_Main _Login_Service_Main;
        /// <summary>
        /// 訊息處理
        /// </summary>
        public readonly Message_Tool _Message_Tool;
        #endregion

        #region == 【全域宣告】 ==
        // ...
        #endregion

        //--【建構】=================================================================================

        #region == 建構 ==
        /// <summary>
        /// 建構
        /// </summary>
        /// <param name="unitOfWork">資料庫工作單元</param>
        /// <param name="mainSystem_DTO">主系統資料</param>
        /// <param name="logService_Main">Log相關</param>
        /// <param name="messageTool">訊息處理</param>
        public LoginService_Main(IUnitOfWork unitOfWork,
            MainSystem_DTO mainSystem_DTO,
            LogService_Main logService_Main,
            //LoginService_Main _login_Service_Main,
            Message_Tool messageTool)
        {
            this._unitOfWork = unitOfWork;
            this._MainSystem_DTO = mainSystem_DTO;
            this._LogService_Main = logService_Main;
            //this._Login_Service_Main = _login_Service_Main;
            this._Message_Tool = messageTool;
            //_UserSession_Model = new UserSession_Model();
        }
        #endregion

        //--【方法】=================================================================================

        #region == 登入狀態 ================================================================
        #region == 檢查相關-登入狀態 ==
        /// <summary>
        /// 檢查是否存在(登入狀態Id) [true存在]
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        //public bool Check_IsExist_LoginStatus(Guid? key)
        //{
        //    var check = _unitOfWork._LoginStatusRepository_Main.GetAlls(x => x.Id == key).Any();
        //    return check;
        //}
        #endregion

        #region == 取資料相關-登入狀態 ==
        /// <summary>
        /// 取得登入者資訊
        /// </summary>
        /// <param name="key">登入狀態Key</param>
        /// <returns>查無則回傳null</returns>
        public UserSession_Model GetInfo_LoginUser(Guid key)
        {
            UserSession_Model result = null;
            LoginStatus loginStatus = null;
            User userInfo = null;

            #region == 取值-登入狀態 (Error return) ==
            // 登入狀態
            loginStatus = _unitOfWork._LoginStatusRepository_Main.Get(x => x.Id == key);
            // [T：查無]
            if (loginStatus == null)
            {
                return null;
            }
            #endregion

            #region == 取值+整理 (Error return) ==
            // [是否為後門登入][T：是，初始化後門資料][F：否，正常取使用者資料]
            if (loginStatus.UserId == 0 && loginStatus.IsBackDoor)
            {
                // 依後門種類初始化資料
                result = new UserSession_Model(loginStatus.BackDoorTypeId);
                result.LoginId = loginStatus.Id;
            }
            else
            {
                // 使用者
                userInfo = _unitOfWork._UserRepository.Get(x => x.Id == loginStatus.UserId);
                // [T：有值][F：無值]
                if (userInfo == null)
                {
                    result = new UserSession_Model
                    {
                        LoginId = loginStatus.Id,
                        UserId = userInfo.Id,
                        UserNo = userInfo.No,
                        UserName = userInfo.Name,
                        CompanyId = userInfo.CompanyId,
                        CompanyNo = userInfo.CompanyInfo.No,
                        CompanyName = userInfo.CompanyInfo.Name,
                        CompanyLevelId = userInfo.CompanyInfo.CompanyLevelId,
                        Account = userInfo.No,
                        Password = userInfo.Password,
                        Mail = userInfo.Mail,
                        RoleId = userInfo.RoleId,
                        //RoleName = ERole.AdminBackDoor.ToString(),
                        //RoleTypeId = E_Role.AdminBackDoor,
                        Functions = System.Enum.GetValues(typeof(E_Function)).Cast<E_Function>().ToList(), // string[]轉Enum
                        IsBackDoor = loginStatus.IsBackDoor,
                        IsNeedCheckPassword = loginStatus.IsNeedCheckPassword,
                    };
                }
                else
                {
                    return null;
                }
            }

            #region == 反序列化連線資訊 ==
            //try
            //{
            //    if (!string.IsNullOrEmpty(data.ConnectDTO_Main_Json))
            //    {
            //        result.ConnectDTO_Main = JsonConvert.DeserializeObject<Connect_DTO>(data.ConnectDTO_Main_Json);
            //    }
            //}
            //catch (Exception)
            //{
            //    //addData.Connect_Custom_Json = JsonConvert.SerializeObject("序列化失敗");
            //}
            #endregion
            #endregion

            return result;
        }

        /// <summary>
        /// 取得API登入者資訊
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public UserSession_Model GetInfo_User(Guid key)
        {
            UserSession_Model result = null;
            LoginStatus loginStatus = null;
            //User userInfo = null;

            #region == 取值-登入狀態 (Error return) ==
            // 登入狀態
            var userInfo = _unitOfWork._UserRepository.Get(x => x.GUID == key);
            // [T：查無]
            if (userInfo == null)
            {
                return null;
            }
            else
            {
                result = new UserSession_Model
                {
                    LoginId = userInfo.GUID,
                    UserId = userInfo.Id,
                    UserNo = userInfo.No,
                    UserName = userInfo.Name,
                    CompanyId = userInfo.CompanyId,
                    //CompanyNo = userInfo.CompanyInfo.No,
                    //CompanyName = userInfo.CompanyInfo.Name,
                    CompanyLevelId = E_CompanyLevel.最高級,
                    Account = userInfo.No,
                    Password = userInfo.Password,
                    Mail = userInfo.Mail,
                    RoleId = userInfo.RoleId,
                    //RoleName = ERole.AdminBackDoor.ToString(),
                    //RoleTypeId = E_Role.AdminBackDoor,
                    //Functions = System.Enum.GetValues(typeof(E_Function)).Cast<E_Function>().ToList(), // string[]轉Enum
                    IsBackDoor = false, //loginStatus.IsBackDoor,
                    IsNeedCheckPassword = true, //loginStatus.IsNeedCheckPassword,
                };
            }

            #region == 反序列化連線資訊 ==
            //try
            //{
            //    if (!string.IsNullOrEmpty(data.ConnectDTO_Main_Json))
            //    {
            //        result.ConnectDTO_Main = JsonConvert.DeserializeObject<Connect_DTO>(data.ConnectDTO_Main_Json);
            //    }
            //}
            //catch (Exception)
            //{
            //    //addData.Connect_Custom_Json = JsonConvert.SerializeObject("序列化失敗");
            //}
            #endregion
            #endregion

            return result;
        }

        /// <summary>
        /// 取得登入者資訊
        /// </summary>
        /// <param name="key">登入狀態Key</param>
        /// <param name="isGetNewUserData">[T：取資料庫內最新的使用者資料][F：取登入當下紀錄的使用者資料]</param>
        /// <returns>查無則回傳null</returns>
        public UserSession_Model GetInfo_LoginUser(Guid key, bool isGetNewUserData)
        {
            UserSession_Model result = null;
            //LoginStatus loginStatus = null;
            User userInfo = null;

            #region == 取登入狀態資料 ==
            var query = new LoginStatus_Filter { Id = key };
            result = this._unitOfWork._LoginStatusRepository_Main.GetLoginData(query);
            #endregion

            #region == 判斷是否將使用者資料，替換成當前資料庫內最新的資料 ==
            // [是否要取最新的使用者資料][T：要]
            if (isGetNewUserData)
            {
                // 使用者
                userInfo = _unitOfWork._UserRepository.Get(x => x.Id == result.UserId);
                // [T：有值][F：無值]
                if (userInfo == null)
                {
                    //result.User_Id = userInfo.Id;
                    result.UserNo = userInfo.No;
                    result.UserName = userInfo.Name;
                    result.CompanyId = userInfo.CompanyId;
                    result.CompanyNo = userInfo.CompanyInfo.No;
                    result.CompanyName = userInfo.CompanyInfo.Name;
                    result.CompanyLevelId = userInfo.CompanyInfo.CompanyLevelId;
                    result.Account = userInfo.No;
                    result.Password = userInfo.Password;
                    result.Mail = userInfo.Mail;
                    result.RoleId = userInfo.RoleId;
                    // 這幾個目前還沒有，以後基本資料完善後，要記得補
                    //result.Role_Name = userInfo.Role_Type_Id.ToString();
                    //result.RoleType_Id = userInfo.Role_Type_Id;
                    //result.Functions = userInfo.Function_Id_TXTs.Split(',').EM_ToEnum_Function_ByIntStrings(); // string[]轉Enum
                }
            }
            #endregion

            return result;
        }
        #endregion

        #region == 存資料相關-登入狀態 ==
        /// <summary>
        /// 【單筆】新增登入狀態
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[新增]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<Guid?> Create_LoginStatus(UserSession_Model input)
        {
            var result = new ResultSimpleData<Guid?>(); // 回傳成功的資料
            // 附值
            var inputs = new List<UserSession_Model> { input };
            // 取得結果
            var resultData = this.Creates_LoginStatus(inputs);
            // 整理結果
            result.IsSuccess = resultData.IsSuccess;
            result.E_StatusCode = resultData.E_StatusCode;
            result.Title = resultData.Title;
            result.Message = resultData.Message;
            result.Data = resultData.Data != null && resultData.Data.Count() > 0 ? resultData.Data.FirstOrDefault() : new Nullable<Guid>();
            return result;
        }

        /// <summary>
        /// 【多筆】新增登入狀態
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[新增]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<List<Guid>> Creates_LoginStatus(List<UserSession_Model> inputs)
        {
            var result = new ResultSimpleData<List<Guid>>(true, new List<Guid>());
            var methodParam = new MethodParameter(); // 方法的通用屬性參數

            #region == 迴圈處理 ==
            foreach (var input in inputs)
            {
                // 重置Model
                methodParam.Reset_Message(input.BindKey);
                // 關鍵值訊息 (通用的)(如不一樣請客製)
                methodParam.ComFocusText = $"公司Key[{input.CompanyId}]---使用者Key[{input.UserId}]---使用者名稱[{input.UserName}]";

                #region == 【檢查】必填 (Error continue) ==
                //// 檢查指定屬性有無值
                //methodParam.ErrorTexts = DataValidationTool.Check_ModelAttrIsNull(input, new Dictionary<string, string>()
                //{
                //    { nameof(input.Company_Id), "公司" },
                //    { nameof(input.User_Id), "使用者Key" },
                //    { nameof(input.User_No), "使用者代號" },
                //    { nameof(input.User_Name), "使用者名稱" },
                //});

                //// [T：有錯誤]
                //if (methodParam.ErrorTexts.Count() > 0)
                //{
                //    // 添加Log訊息
                //    methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.檢查異常, methodParam.ComFocusText);
                //    methodParam.MessageDTO.Message = $"請檢查以下項目是否有值「{string.Join("、", methodParam.ErrorTexts)}」";
                //    this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                //    result.IsSuccess = false;
                //    continue;
                //}
                #endregion

                #region == 處理 ==
                // 是否存在
                methodParam.CheckResult = this._unitOfWork._LoginStatusRepository_Main.CheckExist(input.LoginId);
                // [T：不存在，新增][F：存在，Error]
                if (!methodParam.CheckResult)
                {
                    #region == 整理資訊 ==
                    var addData = new LoginStatus
                    {
                        //Id = input.Id,
                        UserId = input.UserId,
                        UserNo = input.UserNo,
                        UserName = input.UserName,
                        CompanyId = input.CompanyId,
                        CompanyNo = input.CompanyNo,
                        CompanyName = input.CompanyName,
                        CompanyLevelId = input.CompanyLevelId,
                        CompanyLevelName = input.CompanyLevelId.ToString(),
                        Account = input.Account,
                        Password = input.Password,
                        Mail = input.Mail,
                        RoleId = input.RoleId,
                        RoleName = input.RoleName,
                        PermissionTypeId = input.PermissionTypeId,
                        PermissionTypeName = input.PermissionTypeId.ToString(),
                        FunctionId_TXTs = string.Join(",", input.Functions.Select(x => ((int)x).ToString())),
                        IsBackDoor = input.IsBackDoor,
                        IsNeedCheckPassword = input.IsNeedCheckPassword,
                        BackDoorTypeId = input.BackDoorTypeId,
                        BackDoorTypeName = input.BackDoorTypeId.ToString(),
                        LoginDate = methodParam.Today,
                        LoginKeepDay = input.LoginKeepDay,
                        RequestLastDate = methodParam.Today,
                        LoginFromTypeId = input.LoginFromTypeId,
                        LoginFromTypeName = input.LoginFromTypeId.ToString(),
                    };

                    #region == 序列化連線資訊 ==
                    //try
                    //{
                    //    if (input.ConnectDTO_Main != null)
                    //    {
                    //        addData.ConnectDTO_Main_Json = JsonConvert.SerializeObject(input.ConnectDTO_Main);
                    //    }
                    //}
                    //catch (Exception)
                    //{
                    //    //addData.Connect_Custom_Json = JsonConvert.SerializeObject("序列化失敗");
                    //}
                    #endregion
                    #endregion

                    #region == 【DB】寫入 ==
                    try
                    {
                        _unitOfWork._LoginStatusRepository_Main.Add(addData);
                        _unitOfWork.Save();

                        // 添加Log訊息
                        methodParam.TmpFocusText = $"公司Key[{input.CompanyId}]---Key[{addData.Id}]---使用者Key[{input.UserId}]---使用者名稱[{input.UserName}]";
                        methodParam.MessageDTO = new Message_DTO(true, input.BindKey, E_StatusLevel.正常, E_StatusCode.成功, methodParam.TmpFocusText);
                        methodParam.MessageDTO.Message = $"新增成功";
                        this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                        result.Data.Add(addData.Id);
                    }
                    catch (Exception ex)
                    {
                        // 添加Log訊息
                        methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.資料存取異常, methodParam.ComFocusText);
                        methodParam.MessageDTO.Message = $"資料庫存取異常";
                        methodParam.MessageDTO.Message_Exception = ex.Message;
                        this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                        result.IsSuccess = false;
                        continue;
                    }
                    #endregion
                }
                else
                {
                    // 添加Log訊息
                    methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.存在相同資料, methodParam.ComFocusText);
                    methodParam.MessageDTO.Message = $"存在相同Key";
                    this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                    result.IsSuccess = false;
                    continue;
                }
                #endregion
            }
            #endregion

            #region == 整理錯訊 [沒有直接Error，才會執行到這] ==
            if (result.IsSuccess)
            {
                result.Title = "【新增成功】";
                result.E_StatusCode = E_StatusCode.成功;
            }
            else
            {
                result.Title = "【新增失敗】";
                result.E_StatusCode = E_StatusCode.失敗;
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 【單筆】刪除登入狀態
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[新增]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<Guid?> Delete_LoginStatus(UserSession_Model input)
        {
            var result = new ResultSimpleData<Guid?>(); // 回傳成功的資料
            // 附值
            var inputs = new List<UserSession_Model> { input };
            // 取得結果
            var resultData = this.Deletes_LoginStatus(inputs);
            // 整理結果
            result.IsSuccess = resultData.IsSuccess;
            result.E_StatusCode = resultData.E_StatusCode;
            result.Title = resultData.Title;
            result.Message = resultData.Message;
            result.Data = resultData.Data != null && resultData.Data.Count() > 0 ? resultData.Data.FirstOrDefault() : new Nullable<Guid>();
            return result;
        }

        /// <summary>
        /// 【多筆】刪除登入狀態
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[新增]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<List<Guid>> Deletes_LoginStatus(List<UserSession_Model> inputs)
        {
            var result = new ResultSimpleData<List<Guid>>(true, new List<Guid>());
            var methodParam = new MethodParameter(); // 方法的通用屬性參數

            #region == 迴圈處理 ==
            foreach (var input in inputs)
            {
                // 重置Model
                methodParam.Reset_Message(input.BindKey);
                // 關鍵值訊息 (通用的)(如不一樣請客製)
                methodParam.ComFocusText = $"LoginKey[{input.LoginId}]";

                #region == 處理 ==
                // 是否存在
                var delData = this._unitOfWork._LoginStatusRepository_Main.Get(x => x.Id == input.LoginId);
                // 是否存在。 [T：存在，刪除][F：不存在，Error]
                if (delData != null)
                {
                    // 把還需要用到的值保留起來，因為刪掉之後就抓不到了
                    var tempVal = new LoginStatus_DTO
                    {
                        Id = delData.Id,
                        UserId = delData.UserId,
                    };

                    #region == 【DB】寫入 ==
                    try
                    {
                        _unitOfWork._LoginStatusRepository_Main.Delete(delData);
                        _unitOfWork.Save();

                        // 添加Log訊息
                        methodParam.MessageDTO = new Message_DTO(true, methodParam.BindKey, E_StatusLevel.正常, E_StatusCode.成功, methodParam.ComFocusText);
                        methodParam.MessageDTO.Message = $"刪除成功";
                        this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                        result.Data.Add(tempVal.Id.Value);
                    }
                    catch (Exception ex)
                    {
                        // 添加Log訊息
                        methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.資料存取異常, methodParam.ComFocusText);
                        methodParam.MessageDTO.Message = $"資料庫存取異常";
                        methodParam.MessageDTO.Message_Exception = ex.Message;
                        this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                        result.IsSuccess = false;
                        continue;
                    }
                    #endregion
                }
                else
                {
                    // 添加Log訊息
                    methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.查無項目, methodParam.ComFocusText);
                    methodParam.MessageDTO.Message = $"查無項目";
                    this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                    result.IsSuccess = false;
                    continue;
                }
                #endregion
            }
            #endregion

            #region == 整理錯訊 [沒有直接Error，才會執行到這] ==
            if (result.IsSuccess)
            {
                result.Title = "【刪除成功】";
                result.E_StatusCode = E_StatusCode.成功;
            }
            else
            {
                result.Title = "【刪除失敗】";
                result.E_StatusCode = E_StatusCode.失敗;
            }
            #endregion

            return result;
        }
        #endregion

        #region == Check Login ==
        /// <summary>
        /// Check API Login
        /// </summary>
        /// <param name="token"></param>
        public ResultgetUser Check_Login(string token)
        {
            #region == 參數 ==
            UserSession_Model _UserSession_Model = new UserSession_Model();
            Token_Model token_Model = null;
            var secretUser = "ATTN_APIKey"; // ConfigurationManager.AppSettings["TokenKey"]; //加密Key
            var result = new ResultgetUser();
            #endregion

            string Message = "";
            #region == 驗證登入Token (Error Return) ==
            if (token == "")
            {
                result = new ResultgetUser(false, E_StatusCode.Token驗證錯誤, "驗證錯誤：請在 Header 放入 Bearer Token");
            }
            else
            {
                token = token.Substring(7, token.Length - 7);
                try //解密Token
                {
                    token_Model = JWT.Decode<Token_Model>(
                        token,
                        Encoding.UTF8.GetBytes(secretUser),
                        JwsAlgorithm.HS256);
                }
                catch (Exception)
                {
                    result = new ResultgetUser(false, E_StatusCode.Token驗證錯誤, "驗證錯誤：Token解密失敗");
                }
                if (token_Model != null)
                {
                    //檢查Token是否過期
                    var today = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString()); //當前時間(不含毫秒)
                    var login_add_1hour = token_Model.Login_Time; // Convert.ToDateTime(token_Model.Login_Time.AddHours(9).ToString()); //當前時間(不含毫秒)
                    if (today > login_add_1hour) //登入超過1個小時，登入逾時
                    {
                        return new ResultgetUser(false, E_StatusCode.登入逾時, "登入逾時，請嘗試重新登入");
                    }

                    #region == 建立當前登入中的使用者的Model (Error Return) ==
                    // [條件：有無登入ID][T：有]
                    if (token_Model.Login_ID.HasValue)
                    {
                        Guid login_ID = token_Model.Login_ID.Value;

                        #region == 取登入資訊 ==
                        var userinfo = GetInfo_User(login_ID);
                        if (userinfo==null)
                        {
                            return new ResultgetUser(true, E_StatusCode.登入異常, "解密Token成功，但取登入資訊時發生異常，請嘗試重新登入 or 聯絡相關人員");
                        }
                        // 登入相關
                        //var _Login_Service_Main = new Login_Service_Main();

                        try
                        {
                            // 取登入資訊
                            result = new ResultgetUser(true, E_StatusCode.成功, "登入成功", GetInfo_User(login_ID));                    
                        }
                        catch (Exception)
                        {
                            return new ResultgetUser(true, E_StatusCode.登入異常, "解密Token成功，但取登入資訊時發生異常，請嘗試重新登入 or 聯絡相關人員");
                        }
                        #endregion

                    }
                    #endregion

                }
                else
                {
                    return new ResultgetUser(false, E_StatusCode.Token驗證錯誤, "驗證錯誤：Token解密失敗");
                }

            }

            #endregion

            return result;
        }
        #endregion

        #endregion
    }
}
