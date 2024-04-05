using Main_Common.Enum.E_ProjectType;
using Main_Common.GlobalSetting;
using Main_Common.Model.Account;
using Main_Common.Model.Main;
using Main_Service.Service.S_Login;
using Main_Service.Service.S_User;
using Main_Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main_Web.Controllers
{
    /// <summary>
    /// 【開發人員用】初始化設定/整頓/測試
    /// </summary>
    [Authorize] //登入驗證
    public class Z_TempController : BaseWebController
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
        /// <param name="loginService_Main">登入相關</param>
        /// <param name="userService_Main">使用者相關</param>
        public Z_TempController(IHttpContextAccessor httpContextAccessor,
            MainSystem_DTO mainSystem_DTO,
            LoginService_Main loginService_Main,
            UserService_Main userService_Main)
            : base(httpContextAccessor, mainSystem_DTO, loginService_Main)
        {
            this._MainSystem_DTO = mainSystem_DTO;
            this._LoginService_Main = loginService_Main;
            this._UserService_Main = userService_Main;
        }
        #endregion

        //--【方法】=================================================================================

        /// <summary>
        /// Home
        /// </summary>
        /// <returns></returns>
        //[TypeFilter(typeof(IsLegal), Arguments = new object[] { E_Function.後台維護 })]
        public IActionResult Index(string account, string password)
        {
            #region == 開發人員帳密檢查 ==
            if (account == GlobalParameter.Account_RD && password == GlobalParameter.Password_RD) //暗碼
            {
                //userInfo = new UserSession_Model(E_BackDoorType.RD);

            }
            #endregion

            return View();
        }

        #region == Temp ==
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
        #endregion
    }
}
