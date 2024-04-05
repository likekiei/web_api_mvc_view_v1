using Main_Common.Enum.E_ProjectType;
using Main_Common.Enum.E_StatusType;
using Main_Common.Model.Data.Log;
using Main_Common.Model.Data.User;
using Main_Common.Model.Main;
using Main_Service.Service.S_Log;
using Main_Service.Service.S_Login;
using Main_Service.Service.S_User;
using Microsoft.AspNetCore.Mvc;

namespace Main_Web.Controllers
{
    /// <summary>
    /// Log
    /// </summary>
    public class LogController : BaseWebController
    {
        #region == 【DI注入用宣告】 ==
        /// <summary>
        /// 【DTO】主系統資料
        /// </summary>
        public readonly MainSystem_DTO _MainSystem_DTO;
        /// <summary>
        /// 【Main Service】Log相關
        /// </summary>
        public readonly LogService_Main _LogService_Main;
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
        /// <param name="logService_Main">Log相關</param>
        /// <param name="loginService_Main">登入相關</param>
        public LogController(IHttpContextAccessor httpContextAccessor,
            MainSystem_DTO mainSystem_DTO,
            LogService_Main logService_Main,
            LoginService_Main loginService_Main)
            : base(httpContextAccessor, mainSystem_DTO, loginService_Main)
        {
            this._MainSystem_DTO = mainSystem_DTO;
            this._LogService_Main = logService_Main;
            this._LoginService_Main = loginService_Main;
        }
        #endregion

        //--【方法】=================================================================================

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 【View】跳轉Log簡易訊息View
        /// </summary>
        /// <param name="actionBindKey">執行綁定Key</param>
        /// <remarks>
        /// <para>方法跟參數的名稱盡量不要改動，會影響到超連結的生成。</para>
        /// <para>如果要改，請記得一起調整[UrlTool.Get_LogSimpleUrl]方法。</para>
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ActionView_LogSimple(Guid actionBindKey)
        {
            //ViewBag.Permission_DTO = this._MainSystem_DTO.Permission; // 權限DTO

            var queryDTO = new Log_Filter { BindKey_ByAction = actionBindKey };
            var models = this._LogService_Main.GetList_Log(queryDTO, null).Data;
            return View("Index_SimpleView", models);
        }
    }
}
