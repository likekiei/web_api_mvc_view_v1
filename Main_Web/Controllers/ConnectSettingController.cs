using Main_Common.Enum.E_ProjectType;
using Main_Common.Model.Account;
using Main_Common.Model.Main;
using Main_EF.Interface;
using Main_Service.Service.S_ConnectSetting;
using Main_Service.Service.S_Login;
using Main_Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Main_Web.Controllers
{
    /// <summary>
    /// 連線資訊相關
    /// </summary>
    [Authorize] //登入驗證
    public class ConnectSettingController : BaseWebController
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
        /// <summary>
        /// 【Main Service】使用者相關
        /// </summary>
        public readonly ConnectSettingService_Main _ConnectSettingService_Main;
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
        /// <param name="connectSettingService_Main">連線設定相關</param>
        public ConnectSettingController(IHttpContextAccessor httpContextAccessor,
            MainSystem_DTO mainSystem_DTO,
            LoginService_Main loginService_Main,
            ConnectSettingService_Main connectSettingService_Main)
            : base(httpContextAccessor, mainSystem_DTO, loginService_Main)
        {
            this._MainSystem_DTO = mainSystem_DTO;
            this._LoginService_Main = loginService_Main;
            this._ConnectSettingService_Main = connectSettingService_Main;
        }
        #endregion

        //--【方法】=================================================================================

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [TypeFilter(typeof(IsLegal), Arguments = new object[] { E_Function.連線設定 })]
        public IActionResult Index()
        {
            return View();
        }

        #region == 客製DB連線相關 ==
        /// <summary>
        /// 檢查客製DB連線
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult CheckCustomDbLink_ByLoginConnectString()
        {
            var result = this._ConnectSettingService_Main.CheckDbLink(false);
            return Content(result.Message);
        }

        /// <summary>
        /// 資料庫移轉-更新至最新 [只針對客製資料庫]
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Update_DbMigration_ByCustomDb()
        {
            var result = this._ConnectSettingService_Main.Update_DbMigration(null);
            return Content(result.Message);
        }

        /// <summary>
        /// 資料庫移轉-指定版本更新 [只針對客製資料庫]
        /// </summary>
        /// <param name="targetMigrationVersion">指定移轉目標版本(如果沒給，則移轉至最新)</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Update_DbMigrationTarget_ByCustomDb(string? targetMigrationVersion)
        {
            if (string.IsNullOrEmpty(targetMigrationVersion)) { return Content("請輸入移轉版本"); }

            var result = this._ConnectSettingService_Main.Update_DbMigration(targetMigrationVersion);
            return Content(result.Message);
        }
        #endregion
    }
}
