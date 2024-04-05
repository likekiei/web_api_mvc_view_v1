using Main_Common.Enum.E_ProjectType;
using Main_Common.GlobalSetting;
using Main_Common.Model.Account;
using Main_Common.Model.Main;
using Main_Service.Service.S_Company;
using Main_Service.Service.S_Login;
using Main_Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main_Web.Controllers
{
    /// <summary>
    /// 【開發人員設定用】初始化設定/整頓/測試
    /// </summary>
    [Authorize] //登入驗證
    public class Z_DeveloperSettingController : BaseWebController
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
        /// 【Main Service】公司相關
        /// </summary>
        public readonly CompanyService_Main _CompanyService_Main;
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
        /// <param name="companyService_Main">公司相關</param>
        public Z_DeveloperSettingController(IHttpContextAccessor httpContextAccessor,
            MainSystem_DTO mainSystem_DTO,
            LoginService_Main loginService_Main,
            CompanyService_Main companyService_Main)
            : base(httpContextAccessor, mainSystem_DTO, loginService_Main)
        {
            this._MainSystem_DTO = mainSystem_DTO;
            this._LoginService_Main = loginService_Main;
            this._CompanyService_Main = companyService_Main;
        }
        #endregion

        //--【方法】=================================================================================

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        //[TypeFilter(typeof(IsLegal), Arguments = new object[] { E_Function.後台維護 })]
        [AllowAnonymous]
        public IActionResult Index(string account, string password)
        {
            // [帳密檢查][T：允許][F：拒絕]
            if (account == GlobalParameter.Account_RD && password == GlobalParameter.Password_RD) //暗碼
            {
                return View();
            }
            else
            {
                TempData["Message"] = "帳密錯誤，不允許進入";
                return RedirectToAction("Index", "Home");
            }
        }

        #region == 公司相關 ==
        /// <summary>
        /// 【初始資料】生成公司相關的初始資料
        /// </summary>
        /// <returns></returns>
        //[TypeFilter(typeof(IsLegal), Arguments = new object[] { E_Function.初始化設定 })]
        [AllowAnonymous]
        public ActionResult Init_CompanyRelatedTable()
        {
            var msgs = new List<string>();

            #region == 處理 ==
            // 初始化公司
            var result = this._CompanyService_Main.Init_CompanyRelatedTable();
            // [T：失敗]
            if (!result.IsSuccess)
            {
                //msgs.Add($"【{connectDTO.Connect_Group_Name}】{result}");
                return Content("【失敗】");
            }

            return Content("【成功】");
            #endregion

            #region == 處理 ==
            //// 取各群組的連線資訊(Main)
            //var filter = new Connect_Filter { E_DB_Type = E_DB_Type.客製資料庫 };
            //var connects_Main = _Connect_Service_Main.GetInfo_ConnectDTO(filter);

            //// 走訪連線
            //foreach (var connectDTO in connects_Main)
            //{
            //    _UserSessionModel.ConnectDTO_Main = connectDTO; // 替換
            //    _Temp_Service_Main = new Temp_Service_Main(_UserSessionModel);
            //    // 初始化公司
            //    var result = _Temp_Service_Main.公司_Init相關Table();
            //    // [T：失敗]
            //    if (!result.IsSuccess)
            //    {
            //        msgs.Add($"【{connectDTO.Connect_Group_Name}】{result}");
            //    }

            //    return Content("【成功】");
            //}
            #endregion

            //// return [T：失敗][F：成功]
            //if (msgs.Count() > 0)
            //{
            //    return Content($"【失敗】\r\n{string.Join("\r\n", msgs)}");
            //}
            //else
            //{
            //    return Content("【成功】");
            //}
        }
        #endregion
    }
}
