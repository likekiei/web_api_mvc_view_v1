using Main_Common.Enum.E_ProjectType;
using Main_Common.Enum.E_StatusType;
using Main_Common.ExtensionMethod;
using Main_Common.GlobalSetting;
using Main_Common.Model.Account;
using Main_Common.Model.Data;
using Main_Common.Model.Data.Company;
using Main_Common.Model.Main;
using Main_Common.Model.Message;
using Main_Common.Model.Result;
using Main_Common.Model.Tool;
using Main_Common.Tool;
using Main_EF.Migrations;
using Main_Service.Service.S_Company;
using Main_Service.Service.S_Log;
using Main_Service.Service.S_Login;
using Main_Service.Service.S_User;
using Main_Web.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Security.Claims;

namespace Main_Web.Controllers
{
    /// <summary>
    /// 帳號登入相關
    /// </summary>
    public class AccountController : BaseWebController
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
        /// <param name="mainSystem_DTO">主系統資料</param>
        /// <param name="companyService_Main">公司相關</param>
        /// <param name="logService_Main">Log相關</param>
        /// <param name="loginService_Main">登入相關</param>
        /// <param name="userService_Main">使用者相關</param>
        public AccountController(IHttpContextAccessor httpContextAccessor,
            MainSystem_DTO mainSystem_DTO,
            CompanyService_Main companyService_Main,
            LogService_Main logService_Main,
            LoginService_Main loginService_Main,
            UserService_Main userService_Main)
            : base(httpContextAccessor, mainSystem_DTO, loginService_Main)
        {
            this._MainSystem_DTO = mainSystem_DTO;
            this._CompanyService_Main = companyService_Main;
            this._LogService_Main = logService_Main;
            this._LoginService_Main = loginService_Main;
            this._UserService_Main = userService_Main;
        }
        #endregion

        //--【方法】=================================================================================

        public IActionResult Index()
        {
            return View();
        }

        #region == 【一般】登入 ==
        /// <summary>
        /// 【View】登入
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            #region == 參數 ==
            var methodParam = new MethodParameter(Guid.NewGuid()); // 方法的通用屬性參數
            var methodInfo = System.Reflection.MethodBase.GetCurrentMethod(); // 方法Info
            #endregion

            var model = new Login_Model();

            #region == 取預選公司資料 ==
            // 取預選公司DTO
            var defaultCompanyDTO = _CompanyService_Main.GetDefaultSelected_Company(methodParam.BindKey);
            // [有無資料][T：有，填入值]
            if (defaultCompanyDTO != null)
            {
                model.CompanyId = defaultCompanyDTO.Id;
                model.CompanyNo = defaultCompanyDTO.No;
                model.CompanyName = defaultCompanyDTO.Name;
            }
            #endregion

            #region == 下拉選單-公司 ==
            ViewBag.DropList_Company = new List<SelectListItem>();
            #endregion

            return View(model);
        }

        /// <summary>
        /// 登入-正常登入
        /// </summary>
        /// <param name="input">登入資訊</param>
        /// <returns></returns>
        [TypeFilter(typeof(MyResultFilter))]
        [TypeFilter(typeof(MyExceptionFilter))]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Login(Login_Model input)
        {
            #region == 備註 ==
            ///為了避免剛好有使用者的帳密跟暗碼相同，所以登入者資訊，以越後面取得的userInfo為主，後面覆蓋前面
            #endregion

            #region == 參數 ==
            var methodParam = new MethodParameter(Guid.NewGuid()); // 方法的通用屬性參數
            this._MainSystem_DTO.MethodBase = System.Reflection.MethodBase.GetCurrentMethod(); // 方法Info
            this._MainSystem_DTO.BindKey_ByException = methodParam.BindKey;
            this._MainSystem_DTO.Set_LogConfig(E_DBTable.Login_Record, E_LogType.執行紀錄, E_Action.登入);
            input.BindKey = methodParam.BindKey; // 綁定Key
            UserSession_Model userInfo = null; //使用者資訊
            #endregion

            // 添加執行Log
            this._LogService_Main.Add_LogActionRecord(this._MainSystem_DTO.MethodBase, methodParam.BindKey, methodParam.TodayFull);

            #region == 檢查-必填 (Error return) ==
            // 檢查指定屬性有無值
            methodParam.ErrorTexts = DataValidationTool.Check_ModelAttrIsNull(input, new Dictionary<string, string>()
                {
                    { nameof(input.CompanyId), "公司" },
                    { nameof(input.Account), "帳號" },
                    { nameof(input.Password), "密碼" },
                });

            // [T：有錯誤]
            if (methodParam.ErrorTexts.Count() > 0)
            {
                // 添加Log訊息
                methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.權限異常, methodParam.ComFocusText);
                methodParam.MessageDTO.Message = $"請檢查以下項目是否有值「{string.Join("、", methodParam.ErrorTexts)}」";
                this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);
                // 回傳訊息
                methodParam.ResultJsonDTO = new ResultJson_DTO(false, "【登入失敗】", $"請檢查以下項目是否有值「{string.Join("、", methodParam.ErrorTexts)}」");
                return Json(methodParam.ResultJsonDTO);
            }
            #endregion

            #region == 取公司資訊 ==
            var companyDTO = this._CompanyService_Main.GetDTO_Company(methodParam.BindKey, input.CompanyId);
            // [T：查無]
            if (companyDTO == null)
            {
                // 添加Log訊息
                methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.權限異常, methodParam.ComFocusText);
                methodParam.MessageDTO.Message = $"查無公司";
                this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);
                // 回傳訊息
                methodParam.ResultJsonDTO = new ResultJson_DTO(false, "【登入失敗】", $"查無公司");
                return Json(methodParam.ResultJsonDTO);
            }
            #endregion

            #region == 取連線資訊 ==
            // ...
            #endregion

            #region == 檢查連線 ==
            // ...
            #endregion

            #region == 取登入者資訊(依順序覆蓋，後蓋前) ==
            #region == 【L1】【GetInfo】開發者用暗碼，擁有全部權限 [如果會有問題，請直接在DB裡面新增一組開發用帳號] ==
            if (input.Account == GlobalParameter.Account_RD && input.Password == GlobalParameter.Password_RD) //暗碼
            {
                userInfo = new UserSession_Model(E_BackDoorType.RD);
            }
            #endregion

            #region == 【L1】【GetInfo】天崗用暗碼，擁有全部權限 [如果會有問題，請直接在DB裡面新增一組開發用帳號] ==
            if (input.Account == GlobalParameter.Account_TK && input.Password == GlobalParameter.Password_TK) //暗碼
            {
                userInfo = new UserSession_Model(E_BackDoorType.TK);
            }
            #endregion

            #region == 【L1】【GetInfo】客戶用最高權限暗碼，擁有全部權限 [如果會有問題，請直接在DB裡面新增一組開發用帳號] ==
            if (input.Account == GlobalParameter.Account && input.Password == GlobalParameter.Password) //暗碼
            {
                userInfo = new UserSession_Model(E_BackDoorType.COM);
            }
            #endregion

            #region == 【L1】填補公司資料 ==
            // [有無使用者][T：有，補資料]
            if (userInfo != null)
            {
                userInfo.CompanyId = companyDTO.Id.Value;
                userInfo.CompanyNo = companyDTO.No;
                userInfo.CompanyName = companyDTO.Name;
                userInfo.CompanyLevelId = companyDTO.CompanyLevelId;
            }
            #endregion

            #region == 【L1】【GetInfo】正規系統使用者 ==
            userInfo = _UserService_Main.Get_User_Info(input);
            //if (userInfo != null)
            //{

            //}
           
            //input.Is_NeedCheckPassword = true;
            //checkUserInfo = _User_Service_Main.GetLoginInfo(input);
            #endregion
            #endregion

            #region == 登入處理 ==
            // [有無使用者][T：無][F：有，登入作業]
            if (userInfo == null)
            {
                // 添加Log訊息
                methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.權限異常, methodParam.ComFocusText);
                methodParam.MessageDTO.Message = $"登入資訊不正確";
                this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);
                // 回傳訊息
                methodParam.ResultJsonDTO = new ResultJson_DTO(false, "【登入失敗】", $"登入資訊不正確");
                return Json(methodParam.ResultJsonDTO);
            }
            else
            {
                #region == 保存登入狀態 ==
                // 放入其他資訊
                userInfo.BindKey = Guid.NewGuid();
                userInfo.LoginKeepDay = GlobalParameter.AuthCookieKeepDay;
                userInfo.LoginFromTypeId = E_LoginFromType.Web;

                // 登入結果
                var result_Login = this._LoginService_Main.Create_LoginStatus(userInfo);
                // [T：登入失敗][F：登入成功]
                if (result_Login.IsSuccess == false)
                {
                    // 回傳訊息
                    methodParam.ResultJsonDTO = new ResultJson_DTO(result_Login.IsSuccess, "【登入失敗】", result_Login.Message)
                    {
                        logUrl = UrlTool.Get_LogSimpleUrl(_MainSystem_DTO.BindKey_ByAction),
                    };
                    return Json(methodParam.ResultJsonDTO);
                }
                #endregion

                #region == 生成登入驗證Cookie ==
                // 創建Claims聲明清單
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userInfo.UserName ?? "Null"), // 使用者名稱
                        new Claim(ClaimTypes.Role, userInfo.RoleName ?? "Null"), // 角色名稱
                        new Claim(ClaimTypes.Sid, result_Login.Data.GetValueOrDefault().ToString()), // 登入狀態Key
                    };

                // 創建Identity身份 (聲明, 身份驗證方案)
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // 創建容器，放入聲明身份
                var claimPrincipal = new ClaimsPrincipal(claimIdentity);

                // 創建認證屬性
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // 表示身份驗證票據將在瀏覽器關閉後持續存在
                };

                // 寫入Cookie
                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    claimPrincipal,
                    authProperties
                    );

                // 回傳訊息
                methodParam.ResultJsonDTO = new ResultJson_DTO(true, "【登入成功】", "")
                {
                    toUrl = UrlTool.Get_HomeUrl(false),
                };
                return Json(methodParam.ResultJsonDTO);
                #endregion
            }
            #endregion
        }
        #endregion

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            var todayFull = DateTime.UtcNow.AddHours(8); //當前時間(含毫秒)
            var today = Convert.ToDateTime(todayFull.ToString()); //當前時間(不含毫秒)

            #region == 刪除登入狀態 ==
            try
            {
                // [有無資料][T：有資料]
                if (this._MainSystem_DTO.UserSession != null)
                {
                    // 刪除登入狀態
                    var delete_Result = this._LoginService_Main.Delete_LoginStatus(this._MainSystem_DTO.UserSession);
                }
            }
            catch (Exception)
            {
                // 不考慮失敗，沒刪到就算了，理論上Guid不會重複，未來有需要在做一個功能去刪除已經無法使用的登入資訊。
            }
            #endregion

            // 登出Cookie驗證
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
