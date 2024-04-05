using Main_Common.Enum.E_ProjectType;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Main_Common.GlobalSetting;
using Azure;
using System.Security.Claims;
using Main_Common.ExtensionMethod;
using Main_Common.Model.Account;
using Main_Service.Service.S_User;
using Main_Common.Model.Main;

namespace Main_Web.Filters
{
    /// <summary>
    /// 驗證是否有使用該方法的權限
    /// </summary>
    /// <remarks>傳入參數(Arguments)有一定的順序規則，請確保傳入與接收參數的順序是對應的</remarks>
    public class IsLegal : Attribute, IActionFilter
    {
        #region == 【全域宣告】 ==
        /// <summary>
        /// 功能代碼
        /// </summary>
        public readonly E_Function _FunctionCode;
        /// <summary>
        /// 【DTO】主系統資料
        /// </summary>
        public readonly MainSystem_DTO _MainSystem_DTO;
        #endregion

        #region == 【建構】 ==
        /// <summary>
        /// 建構
        /// </summary>
        /// <param name="functionCode">功能代碼</param>
        /// <param name="mainSystem_DTO">主系統資料</param>
        /// <remarks>傳入參數有一定的順序規則，請確保傳入與接收參數的順序是對應的</remarks>
        public IsLegal(E_Function functionCode,
            MainSystem_DTO mainSystem_DTO)
        {
            _FunctionCode = functionCode;
            _MainSystem_DTO = mainSystem_DTO;
        }
        #endregion

        //--【方法】=================================================================================

        /// <summary>
        /// 在執行 Action 之前執行
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //UserSession_Model? loginInfo = null;
            ClaimsIdentity? identity = null;
            string? claim_Sid = null;
            Guid? userId = null;

            #region == 檢查有無錯誤 (默認Controller階段的時候就會針對登入者進行驗證檢查) ==
            // [是否皆有值][T：任意資料無值]
            if(_MainSystem_DTO == null || _MainSystem_DTO.UserSession == null)
            {
                SetErrorResult(context, "Account", "Login", "身分驗證相關資料不完整，無法正常進行驗證，請嘗試重新登入 or 聯絡相關人員");
            }

            // [主系統資料是否有異常][T：有錯誤]
            if (_MainSystem_DTO.Result.IsSuccess == false)
            {
                SetErrorResult(context, "Account", "Login", $"主系統資料異常，{_MainSystem_DTO.Result.Message}");
            }
            #endregion

            #region == 取值-登入驗證的身份 ==
            //// 取用於登入驗證的身份
            //identity = context.HttpContext.User.Identities.Where(x => x.AuthenticationType == CookieAuthenticationDefaults.AuthenticationScheme).FirstOrDefault();

            //// [是否有身分驗證 or 聲明清單][T：無]
            //if (identity == null || identity.Claims.Count() == 0)
            //{
            //    SetErrorResult(context, "登入驗證的資訊不完整，請嘗試重新登入 or 聯絡相關人員", "Account", "Login");
            //}
            #endregion

            #region == 取值-登入者資訊 ==
            //// 取使用者Id
            //claim_Sid = identity.Claims.Where(x => x.Type == ClaimTypes.Sid).Select(x => x.Value).FirstOrDefault();
            //// 轉型別
            //userId = claim_Sid.EM_StringToGuid();
            //// [T：有值][F：無值]
            //if (userId.HasValue)
            //{
            //    // 暫時先這樣測試
            //    loginInfo = new UserSession_Model
            //    {
            //        Account = "AccountAAA",
            //        Password = "PasswordAAA",
            //        User_Name = "User_NameAAA",
            //        Functions = System.Enum.GetValues(typeof(E_Function)).Cast<E_Function>().ToList(),
            //    };
            //    //var userInfo = _UserService_Main.Get_User_ById(userId.Value);
            //    // [T：查無使用者]
            //    if(loginInfo == null)
            //    {
            //        SetErrorResult(context, "查無登入者資訊，請嘗試重新登入 or 聯絡相關人員", "Account", "Login");
            //    }
            //}
            //else
            //{
            //    SetErrorResult(context, "無法處理的登入者Id，請嘗試重新登入 or 聯絡相關人員", "Account", "Login");
            //}
            #endregion

            #region == 檢查-功能代碼 ==
            // [使用者有無特定權限][T：查無權限]
            //if (!_MainSystem_DTO.UserSession.Functions.Where(x => x == _FunctionCode).Any()) //該使用者查無特定權限
            //{
            //    SetErrorResult(context, "Home", "Index", $"無此權限[{_FunctionCode.ToString()}]");
            //}

            //// [使用者有無特定權限][T：查無權限]
            //if (!loginInfo.Functions.Where(x => x == _FunctionCode).Any()) //該使用者查無特定權限
            //{
            //    SetErrorResult(context, "無此權限", "Home", "Index");
            //}
            #endregion
        }

        /// <summary>
        /// 在執行 Action 之後執行
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // ...
        }

        /// <summary>
        /// 訊息回傳
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="message"></param>
        private static void SetErrorResult(ActionExecutingContext filterContext, string controller, string action, string message)
        {
            // 判斷是否為Ajax請求
            bool isAjax = filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            // [T：Ajax請求][F：非Ajax請求]
            if (isAjax) //ajax的請求
            {
                var jsonResult = new JsonResult("");
                //jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                jsonResult.Value = new { _isSuccess = false, _message = message };
                filterContext.Result = jsonResult;
            }
            else //非ajax的請求
            {
                ((Controller)filterContext.Controller).TempData["Message"] = message;
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = controller, action = action }));
            }
        }
    }
}
