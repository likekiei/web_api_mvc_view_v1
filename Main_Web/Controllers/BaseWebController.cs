using Azure;
using Main_Common.Enum.E_ProjectType;
using Main_Common.ExtensionMethod;
using Main_Common.GlobalSetting;
using Main_Common.Model.Account;
using Main_Common.Model.Main;
using Main_Common.Model.Result;
using Main_Common.Model.Tool;
using Main_Service.Service.S_Login;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Main_Web.Controllers
{
    /// <summary>
    /// 【WEB用】Controller前置處理
    /// </summary>
    public class BaseWebController : Controller
    {
        #region == 【全域宣告】 ==
        // ...
        #endregion

        /// <summary>
        /// 建構執行
        /// </summary>
        /// <param name="httpContextAccessor">HttpContext</param>
        /// <param name="mainSystem_DTO">主系統資料</param>
        /// <param name="loginService_Main">登入相關</param>
        public BaseWebController(IHttpContextAccessor httpContextAccessor, 
            MainSystem_DTO mainSystem_DTO,
            LoginService_Main loginService_Main)
        {
            #region == 參數 ==
            ClaimsIdentity? identity = null;
            string? claim_Sid = null;
            Guid? loginId = null;
            #endregion

            try
            {
                #region == 設定語系 (本次執行) ==
                try
                {
                    //var vvvA = "zh-TW";
                    //var vvvB = "en-US";
                    var setLocalizer = "";
                    // [有無語系Cookie][T：有][F：無]
                    if (httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(GlobalParameter.LanguageCookieName, out setLocalizer))
                    {
                        // 依語系Cookie的值設定
                        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(setLocalizer);
                        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(setLocalizer);
                    }
                    else
                    {
                        // 固定設定值
                        setLocalizer = "zh-TW";
                        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(setLocalizer);
                        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(setLocalizer);

                        // 設定語系Cookie
                        httpContextAccessor.HttpContext.Response.Cookies.Append(GlobalParameter.LanguageCookieName, setLocalizer);
                    }
                }
                catch (Exception)
                {
                    // 不處理
                    // 語系切換如果發生例外，就讓系統繼續沿用原本的設定，或是系統的預設設定
                }
                #endregion

                #region == 取值-登入驗證的身份 ==
                // 取用於登入驗證的身份
                identity = httpContextAccessor.HttpContext.User.Identities.Where(x => x.AuthenticationType == CookieAuthenticationDefaults.AuthenticationScheme).FirstOrDefault();

                // [是否有身分驗證 or 聲明清單][T：無]
                if (identity == null || identity.Claims.Count() == 0)
                {
                    mainSystem_DTO.Result = new ResultSimple(false, "登入驗證的資訊不完整，請嘗試重新登入 or 聯絡相關人員");
                    return;
                }
                #endregion

                #region == 取值-登入者資訊 ==
                // 取登入Id
                claim_Sid = identity.Claims.Where(x => x.Type == ClaimTypes.Sid).Select(x => x.Value).FirstOrDefault();
                // 轉型別
                loginId = claim_Sid.EM_StringToGuid();
                // [T：有值][F：無值]
                if (loginId.HasValue)
                {
                    // 取登入者資訊
                    mainSystem_DTO.UserSession = loginService_Main.GetInfo_LoginUser(loginId.Value, false);
                    // [T：有值][F：無值]
                    if (mainSystem_DTO.UserSession != null)
                    {
                        // 整理權限DTO
                        mainSystem_DTO.Permission = new Permission_DTO
                        {
                            CompanyId = mainSystem_DTO.UserSession.CompanyId,
                            CompanyLevelId = mainSystem_DTO.UserSession.CompanyLevelId,
                            PermissionTypeId = mainSystem_DTO.UserSession.PermissionTypeId,
                            IsBackDoor = mainSystem_DTO.UserSession.IsBackDoor,
                        };
                    }
                    else
                    {
                        mainSystem_DTO.Result = new ResultSimple(false, "查無登入者資訊，請嘗試重新登入 or 聯絡相關人員");
                        return;
                    }
                }
                else
                {
                    mainSystem_DTO.Result = new ResultSimple(false, "無法處理的登入者Id，請嘗試重新登入 or 聯絡相關人員");
                    return;
                }
                #endregion
            }
            catch (Exception)
            {
                mainSystem_DTO.Result = new ResultSimple(false, "BaseController處理過程異常，聯絡相關人員");
                return;
            }
        }
    }
}
