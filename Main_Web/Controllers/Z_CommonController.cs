using Main_Common.GlobalSetting;
using Main_Common.Model.Account;
using Main_Common.Model.Data.Company;
using Main_Common.Model.Data.Connect;
using Main_Common.Model.Data.Role;
using Main_Common.Model.Main;
using Main_Common.Model.Result;
using Main_Common.Model.Tool;
using Main_Service.Service.S_Company;
using Main_Service.Service.S_Log;
using Main_Service.Service.S_Login;
using Main_Service.Service.S_Role;
using Main_Web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Main_Web.Controllers
{
    /// <summary>
    /// 通用處理(無權限限制)
    /// </summary>
    public class Z_CommonController : BaseWebController
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
        /// 【Main Service】登入相關
        /// </summary>
        public readonly LoginService_Main _LoginService_Main;
        /// <summary>
        /// 【Main Service】角色相關
        /// </summary>
        public readonly RoleService_Main _RoleService_Main;
        /// <summary>
        /// 【Helper】通用檢查相關Helper
        /// </summary>
        public readonly MyCheckHelper _MyCheckHelper;
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
        /// <param name="loginService_Main">登入相關</param>
        /// <param name="roleService_Main">角色相關</param>
        /// <param name="myCheckHelper">通用檢查相關Helper</param>
        public Z_CommonController(IHttpContextAccessor httpContextAccessor,
            MainSystem_DTO mainSystem_DTO,
            CompanyService_Main companyService_Main,
            LoginService_Main loginService_Main,
            RoleService_Main roleService_Main,
            MyCheckHelper myCheckHelper)
            : base(httpContextAccessor, mainSystem_DTO, loginService_Main)
        {
            this._MainSystem_DTO = mainSystem_DTO;
            this._CompanyService_Main = companyService_Main;
            this._LoginService_Main = loginService_Main;
            this._RoleService_Main = roleService_Main;
            this._MyCheckHelper = myCheckHelper;
        }
        #endregion

        //--【方法】=================================================================================

        public IActionResult Index()
        {
            return View();
        }

        #region == 公司 ==
        /// <summary>
        /// 【過濾】公司下拉選單
        /// </summary>
        /// <param name="input">查詢值</param>
        /// <returns></returns>
        public JsonResult SimpleDropList_Company(Company_Filter input)
        {
            #region == 分頁DTO ==
            //處理分頁用
            Pageing_DTO pageingDTO = null;
            if (input.PageNumber.HasValue)
            {
                pageingDTO = new Pageing_DTO
                {
                    IsEnable = true,
                    PageNumber = input.PageNumber.HasValue ? input.PageNumber.Value : 1,
                    PageSize = input.PageSize ?? GlobalParameter.PageSize_ByDropList,
                };
            }
            #endregion

            //1個空格默認空查詢
            input.Keyword = input.Keyword == " " ? "" : input.Keyword;

            //取得簡易下拉清單
            var datas = this._CompanyService_Main.GetSimpleDropList_Company_Obj(input, pageingDTO);
            return Json(new { data = datas.Data, totalCount = datas.PageingDTO.TotalCount });
        }

        /// <summary>
        /// 【過濾】公司下拉選單(未登入DB) 【沒有足夠的資料能處理，而且目前也用不到】
        /// </summary>
        /// <param name="input">查詢值</param>
        /// <returns></returns>
        //public JsonResult SimpleDrop_Company_NotDB(Company_Filter input)
        //{
        //    var _Connect_Service_Main = new Connect_Service_Main();
        //    Connect_DTO connectDTO_Main = null; //連線資訊(Main DB)
        //    ResultOutput_Data<object> datas = null;

        //    #region == 檢查連線 ==
        //    //客製-連線測試
        //    var checkDb_Custom = _Connect_Service_Main.CheckDbLink(false);
        //    if (!checkDb_Custom.IsSuccess) //false
        //    {
        //        return Json(new { IsSuccess = false, Message = "公司下拉選單資料抓取失敗，連線失敗" }, JsonRequestBehavior.AllowGet);
        //    }
        //    #endregion

        //    //建構
        //    _Company_Service_Main = new Company_Service_Main(new UserSessionModel { ConnectDTO_Main = connectDTO_Main });

        //    //1個空格默認空查詢
        //    input.Keyword = input.Keyword == " " ? "" : input.Keyword;

        //    #region == 取資料-簡易下拉清單 ==
        //    // 是否為後門帳密(系統用/開發用)。  [T：是，全取][F：否，正常過濾]
        //    if ((input.User_No == _DefaultSetting_DTO.Account && input.Password == _DefaultSetting_DTO.Password)
        //        || (input.User_No == _DefaultSetting_DTO.Account_TK && input.Password == _DefaultSetting_DTO.Password_TK)
        //        || (input.User_No == _DefaultSetting_DTO.Account_RD && input.Password == _DefaultSetting_DTO.Password_RD)
        //        )
        //    {
        //        input.Is_SearchAll = true;
        //        datas = _Company_Service_Main.GetSimpleDrop_Company_Obj_NotLogin(input, null);
        //    }
        //    else
        //    {
        //        input.Is_SearchAll = false;
        //        datas = _Company_Service_Main.GetSimpleDrop_Company_Obj_NotLogin(input, null);
        //    }
        //    #endregion

        //    return Json(new { data = datas.Data }, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region == 角色 ==
        /// <summary>
        /// 【過濾】角色下拉選單
        /// </summary>
        /// <param name="input">查詢值</param>
        /// <returns></returns>
        public JsonResult SimpleDropList_Role(Role_Filter input)
        {
            #region == 分頁DTO ==
            //處理分頁用
            Pageing_DTO pageingDTO = null;
            if (input.PageNumber.HasValue)
            {
                pageingDTO = new Pageing_DTO
                {
                    IsEnable = true,
                    PageNumber = input.PageNumber.HasValue ? input.PageNumber.Value : 1,
                    PageSize = input.PageSize ?? GlobalParameter.PageSize_ByDropList,
                };
            }
            #endregion

            //1個空格默認空查詢
            input.Keyword = input.Keyword == " " ? "" : input.Keyword;

            //取得簡易下拉清單
            var datas = this._RoleService_Main.GetSimpleDropList_Role_Obj(input, pageingDTO);
            return Json(new { data = datas.Data, totalCount = datas.PageingDTO.TotalCount });
        }
        #endregion
    }
}
