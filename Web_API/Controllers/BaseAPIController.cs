using Jose;
using Main_Common.Model.Account;
using Main_Common.Model.Handle;
using Main_Service.Service.S_Company;
using Main_Service.Service.S_Log;
using Main_Service.Service.S_Login;
using Main_Service.Service.S_User;
using Microsoft.AspNetCore.Mvc;
//using Main_Web_APP.Service.S_Login;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace Web_API.Controllers
{
    /// <summary>
    /// API用，Controller前置處理
    /// </summary>
    //[Route("api/[controller]/[action]")]
    //[ApiController]
    public class BaseAPIController : ControllerBase
    {
        #region == 【DI注入用宣告】 ==
        /// <summary>
        /// 【DTO】主系統資料
        /// </summary>
        //public readonly MainSystem_DTO _MainSystem_DTO;
        /// <summary>
        /// 【Main Service】公司相關
        /// </summary>
        //public readonly CompanyService_Main _CompanyService_Main;
        /// <summary>
        /// 【Main Service】Log相關
        /// </summary>
        //public readonly LogService_Main _LogService_Main;
        /// <summary>
        /// 【Main Service】登入相關
        /// </summary>
        public readonly LoginService_Main _LoginService_Main;
        /// <summary>
        /// 【Main Service】使用者相關
        /// </summary>
        //public readonly UserService_Main _UserService_Main;
        #endregion

        #region == 【全域宣告】 ==
        /// <summary>
        /// 登入者資訊
        /// </summary>
        protected internal UserSession_Model _UserSession_Model { get; private set; }
        /// <summary>
        /// 權限資訊
        /// </summary>
        //protected internal Permission_DTO _Permission_DTO { get; set; }
        #endregion

        #region == 【建構】 ==
        /// <summary>
        /// 建構
        /// </summary>
        /// <param name="httpContextAccessor">HttpContext</param>
        ///// <param name="mainSystem_DTO">主系統資料</param>
        ///// <param name="companyService_Main">公司相關</param>
        /// <param name="logService_Main">Log相關</param>
        /// <param name="loginService_Main">登入相關</param>
        ///// <param name="userService_Main">使用者相關</param>
        public BaseAPIController(
        //    IHttpContextAccessor httpContextAccessor
        //    //MainSystem_DTO mainSystem_DTO,
        //    CompanyService_Main company_Service,
            //LogService_Main logService_Main,
            LoginService_Main _login_Service_Main,
            UserSession_Model _userSession_Model
        //    UserService_Main userService_Main)
        )
        //    : base(httpContextAccessor)
        {
            //    //this._MainSystem_DTO = mainSystem_DTO;
            //this._CompanyService_Main = companyService_Main;
            //    //this._LogService_Main = logService_Main;
            this._LoginService_Main = _login_Service_Main;
            //   this._UserService_Main = userService_Main;
            this._UserSession_Model = _userSession_Model;
        }
        #endregion

        //--【方法】=================================================================================

        //protected override void Initialize(HttpControllerContext controllerContext)
        //{   
        //    base.Initialize(controllerContext);

        //    Token_Model token_Model = null;
        //    var secretUser = "ATTN_APIKey"; // ConfigurationManager.AppSettings["TokenKey"]; //加密Key

        //    #region == 驗證登入Token (Error Return) ==
        //    if (Request.Headers.Authorization == null || Request.Headers.Authorization.Scheme != "Bearer")
        //    {
        //        //throw new Exception("驗證錯誤：請在 Header 放入 Bearer Token");
        //        throw new HttpResponseException(
        //            Request.CreateErrorResponse(
        //                HttpStatusCode.InternalServerError, "驗證錯誤：請在 Header 放入 Bearer Token"));
        //    }
        //    else
        //    {
        //        try //解密Token
        //        {
        //            token_Model = Jose.JWT.Decode<Token_Model>(
        //                Request.Headers.Authorization.Parameter,
        //                Encoding.UTF8.GetBytes(secretUser),
        //                JwsAlgorithm.HS256);
        //        }
        //        catch (Exception)
        //        {
        //            //throw new Exception("驗證錯誤：Token解密失敗");
        //            throw new HttpResponseException(
        //                Request.CreateErrorResponse(
        //                    HttpStatusCode.InternalServerError, "驗證錯誤：Token解密失敗"));
        //        }

        //        //解密完後，檢查登入狀態
        //        if (!token_Model.Result) //登入狀態為false，未成功登入
        //        {
        //            throw new HttpResponseException(
        //                Request.CreateErrorResponse(
        //                    HttpStatusCode.InternalServerError, "解密Token成功，但資料顯示未成功登入，請嘗試重新登入"));
        //        }

        //        var today = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString()); //當前時間(不含毫秒)
        //        var login_add_1hour = Convert.ToDateTime(token_Model.Login_Time.AddHours(9).ToString()); //當前時間(不含毫秒)
        //        if (today>login_add_1hour) //登入超過1個小時，登入逾時
        //        {
        //            throw new HttpResponseException(
        //                Request.CreateErrorResponse(
        //                    HttpStatusCode.InternalServerError, "登入逾時，請嘗試重新登入"));
        //        }
        //    }
        //    #endregion

        //    #region == 建立當前登入中的使用者的Model (Error Return) ==
        //    // [條件：有無登入ID][T：有]
        //    if (token_Model.Login_ID.HasValue)
        //    {
        //        Guid login_ID = token_Model.Login_ID.Value;

        //        #region == 取登入資訊 ==

        //        // 登入相關
        //        //var _Login_Service_Main = new Login_Service_Main();

        //        try
        //        {
        //            // 取登入資訊
        //            _UserSession_Model = _LoginService_Main.GetInfo_LoginUser(login_ID);
        //        }
        //        catch (Exception)
        //        {
        //            throw new HttpResponseException(
        //                Request.CreateErrorResponse(
        //                    HttpStatusCode.InternalServerError, "解密Token成功，但取登入資訊時發生異常，請嘗試重新登入 or 聯絡相關人員"));
        //        }

        //        // [條件：有無登入資訊][T：無]
        //        if (_UserSession_Model == null)
        //        {
        //            throw new HttpResponseException(
        //                Request.CreateErrorResponse(
        //                    HttpStatusCode.InternalServerError, "解密Token成功，但未取得登入資訊，請嘗試重新登入 or 聯絡相關人員"));
        //        }
        //        #endregion
                
        //        #region == 整理權限資料 ==
        //        // [條件：有無使用者資料][T：有]
        //        //if (_UserSession_Model != null)
        //        //{
        //        //    _Permission_DTO = new Permission_DTO
        //        //    {
        //        //        Company_ID = _UserSession_Model.Company_ID,
        //        //        Company_Level_ID = _UserSession_Model.Company_Level_ID,
        //        //        Role_Type_ID = _UserSession_Model.Role_Type_ID,
        //        //        Is_BackDoor = _UserSession_Model.Is_BackDoor,
        //        //    };
        //        //}
        //        #endregion
        //    }
        //    #endregion
        //}
    }
}
