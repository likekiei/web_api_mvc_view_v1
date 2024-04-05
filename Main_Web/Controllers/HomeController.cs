using Main_Common.Enum.E_ProjectType;
using Main_Common.Model.Account;
using Main_Common.Model.Main;
using Main_Resources.Model.Validation;
using Main_Service.Service.S_Login;
using Main_Service.Service.S_User;
using Main_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace Main_Web.Controllers
{
    /// <summary>
    /// Home
    /// </summary>
    public class HomeController : BaseWebController
    {
        #region == 【DI注入用宣告】 ==
        /// <summary>
        /// 【DTO】主系統資料
        /// </summary>
        public readonly MainSystem_DTO _MainSystem_DTO;
        /// <summary>
        /// 【Main Service】登入相關
        /// </summary>
        public readonly LoginService_Main _LoginService_Main;
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
        /// <param name="loginService_Main">登入相關</param>
        public HomeController(IHttpContextAccessor httpContextAccessor,
            MainSystem_DTO mainSystem_DTO,
            LoginService_Main loginService_Main)
            : base(httpContextAccessor, mainSystem_DTO, loginService_Main)
        {
            this._MainSystem_DTO = mainSystem_DTO;
            this._LoginService_Main = loginService_Main;
        }
        #endregion

        //--【方法】=================================================================================

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 【EXE】SubmitTemp，提供給只需要格式的form
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SubmitTemp(string input)
        {
            return Json(new { _isSuccess = false, _message = "該action提供給只需要格式的form，不應存在有效的submit" });
        }

        #region == 構成主板所需的View ==
        /// <summary>
        /// 【View】取得最上方區塊
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetTopBar_View()
        {
            return PartialView("_Layout_TopBar", this._MainSystem_DTO.UserSession);
        }

        /// <summary>
        /// 【View】取得上方Menu區塊
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetMenuBar_View()
        {
            var functions = this._MainSystem_DTO.UserSession != null ? this._MainSystem_DTO.UserSession.Functions : new List<E_Function>();
            return PartialView("_Layout_MenuBar", functions);
        }

        /// <summary>
        /// 【View】取得側邊Menu區塊
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetSideBar_View()
        {
            var functions = this._MainSystem_DTO.UserSession != null ? this._MainSystem_DTO.UserSession.Functions : new List<E_Function>();
            return PartialView("_Layout_SideBar", functions);
        }

        /// <summary>
        /// 【View】輪播區塊
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetCarousel_View()
        {
            return PartialView("_Carousel");
        }

        /// <summary>
        /// 【View】Footer區塊
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetFooter_View()
        {
            return PartialView("_Layout_Footer", this._MainSystem_DTO.UserSession);
        }
        #endregion
    }
}