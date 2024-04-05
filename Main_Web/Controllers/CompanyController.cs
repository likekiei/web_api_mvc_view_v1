using Main_Common.Model.Tool;
using Main_Common.Model.Data.Company;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Main_Common.Enum.E_ProjectType;
using Main_Common.Enum.E_StatusType;
using Main_Service.Service.S_Company;
using Microsoft.AspNetCore.Authorization;
using Main_Web.Filters;
using Main_Common.Model.Main;
using Main_Service.Service.S_Login;
using Main_Common.Tool;
using System.Reflection;
using Main_Service.Service.S_Log;
using Main_Common.Model.Result;
using Main_Common.Model.Message;
using Main_Web.Helper;
using Main_Common.GlobalSetting;
using Main_Common.Model.Data.User;
using Main_Common.Model.Data.Log;
using Main_Common.ExtensionMethod;
using Main_Common.Model.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Main_EF.Migrations;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Main_Web.Controllers
{
    /// <summary>
    /// 公司相關
    /// </summary>
    [Authorize] //登入驗證
    public class CompanyController : BaseWebController
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
        /// <param name="logService_Main">Log相關</param>
        /// <param name="loginService_Main">登入相關</param>
        /// <param name="companyService_Main">公司相關</param>
        /// <param name="myCheckHelper">通用檢查相關Helper</param>
        public CompanyController(IHttpContextAccessor httpContextAccessor,
            MainSystem_DTO mainSystem_DTO,
            CompanyService_Main companyService_Main,
            LogService_Main logService_Main,
            LoginService_Main loginService_Main,
            MyCheckHelper myCheckHelper)
            : base(httpContextAccessor, mainSystem_DTO, loginService_Main)
        {
            this._MainSystem_DTO = mainSystem_DTO;
            this._CompanyService_Main = companyService_Main;
            this._LogService_Main = logService_Main;
            this._LoginService_Main = loginService_Main;
            this._MyCheckHelper = myCheckHelper;
        }
        #endregion

        //--【方法】=================================================================================

        #region == 公司 ==
        [TypeFilter(typeof(IsLegal), Arguments = new object[] { E_Function.公司 })]
        public IActionResult Index_Company()
        {
            return View();
        }

        /// <summary>
        /// 【View】公司清單查詢畫面
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult FilterView_CompanyList()
        {
            ViewBag.Permission_DTO = this._MainSystem_DTO.Permission; // 權限DTO

            //var model = new Company_Filter { Company_Id = this._MainSystem_DTO.UserSession.Company_Id };
            var model = new Company_Filter();
            return PartialView("_Company_Filter", model);
        }

        /// <summary>
        /// 【View】公司清單查詢
        /// </summary>
        /// <param name="input">查詢條件</param>
        /// <returns></returns>
        [TypeFilter(typeof(MyResultFilter))]
        [TypeFilter(typeof(MyExceptionFilter))]
        [HttpPost]
        public IActionResult Filter_CompanyList(Company_Filter input)
        {
            #region == 參數 ==
            var methodParam = new MethodParameter(Guid.NewGuid()); // 方法的通用屬性參數
            this._MainSystem_DTO.MethodBase = System.Reflection.MethodBase.GetCurrentMethod(); // 方法Info
            this._MainSystem_DTO.BindKey_ByException = methodParam.BindKey;
            this._MainSystem_DTO.Set_LogConfig(E_DBTable.Main_Company, E_LogType.執行紀錄, E_Action.查詢);
            input.BindKey = methodParam.BindKey; // 綁定Key
            #endregion

            #region == 處理 ==
            // 添加執行Log
            this._LogService_Main.Add_LogActionRecord(this._MainSystem_DTO.MethodBase, methodParam.BindKey, methodParam.TodayFull);

            #region == 查詢值 ==
            var pageingDTO = new Pageing_DTO
            {
                IsEnable = input.PageNumber.HasValue ? true : false,
                PageNumber = input.PageNumber == 0 ? 1 : (input.PageNumber ?? 1),
                PageSize = input.PageSize ?? GlobalParameter.PageSize_ByList,
            };

            //紀錄查詢條件
            ViewBag.PageSize = input.PageSize;
            ViewBag.Keyword = input.Keyword;
            ViewBag.Id = input.Id;
            ViewBag.No = input.No;
            ViewBag.Name = input.Name;
            #endregion

            #region == 處理 ==
            // 取資料清單
            var result = this._CompanyService_Main.GetList_Company(input, pageingDTO);
            // [T：成功][F：失敗]
            if (result.IsSuccess)
            {
                #region == 判斷當前頁數是否大於總頁數  [如果超過總頁數，需重新取得Model] ==
                //判斷頁數，避免不是第一頁卻找不到資料的情況，也就是說，如果非第一頁卻無資料，則頁數需-1
                if (result.PageingDTO.IsEnable && result.PageingDTO.TotalCount > 0) //有啟用分頁 && 總數 > 0，才處理   [避免計算出來的總頁數是0]
                {
                    var overCheck = View_Tool.Check_NowPageOverTotalPage(result.PageingDTO);
                    if (overCheck.IsSuccess) //當前頁超過總頁數，重新取得分頁資料
                    {
                        pageingDTO.PageNumber = overCheck.Data;
                        result = this._CompanyService_Main.GetList_Company(input, pageingDTO); //重新取得Model
                    }
                }
                #endregion

                pageingDTO = result.PageingDTO;
                var model = new StaticPagedList<Company_List>(result.Data, pageingDTO.PageNumber, pageingDTO.PageSize, pageingDTO.TotalCount);
                return PartialView("_Company_List", model);
            }
            else
            {
                // 回傳訊息
                methodParam.ResultJsonDTO = new ResultJson_DTO(result.IsSuccess, result.Title, result.Message)
                {
                    logUrl = UrlTool.Get_LogSimpleUrl(_MainSystem_DTO.BindKey_ByAction),
                };
                return Json(methodParam.ResultJsonDTO);
            }
            #endregion
            #endregion
        }

        /// <summary>
        /// 【View】公司新增頁面
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateView_Company()
        {
            ViewBag.Permission_DTO = this._MainSystem_DTO.Permission; // 權限DTO

            var model = new Company_Model
            {
                CRUD = E_CRUD.C,
                //Company_Id = this._MainSystem_DTO.UserSession.Company_Id,
                CompanyLevelId = E_CompanyLevel.無級,
            };

            #region == 下拉選單-公司等級 ==
            var dropList_CompanyLevel = ConvertTool.EnumToList<E_CompanyLevel>(null, null, new int[] { (int)E_CompanyLevel.最高級 });
            ViewBag.DropList_CompanyLevel = dropList_CompanyLevel.ConvertModelList<SelectItemDTO, SelectListItem>();
            #endregion

            return PartialView("_Company_Create", model);
        }

        /// <summary>
        /// 【EXE】公司新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [TypeFilter(typeof(MyResultFilter))]
        [TypeFilter(typeof(MyExceptionFilter))]
        [TypeFilter(typeof(IsLegal), Arguments = new object[] { E_Function.公司 })]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create_Company(Company_Model input)
        {
            #region == 參數 ==
            var methodParam = new MethodParameter(Guid.NewGuid()); // 方法的通用屬性參數
            this._MainSystem_DTO.MethodBase = System.Reflection.MethodBase.GetCurrentMethod(); // 方法Info
            this._MainSystem_DTO.BindKey_ByException = methodParam.BindKey;
            this._MainSystem_DTO.Set_LogConfig(E_DBTable.Main_Company, E_LogType.執行紀錄, E_Action.新增);
            #endregion

            #region == 處理 ==
            // 添加執行Log
            this._LogService_Main.Add_LogActionRecord(this._MainSystem_DTO.MethodBase, methodParam.BindKey, methodParam.TodayFull);

            #region == 【檢查+補值】有無input (Error Return) ==
            // 檢查有無input
            methodParam.CheckResult = _MyCheckHelper.Check_IsNullInput<Company_Model>(ref methodParam, input);
            // [T：有值][F：無值]
            if (methodParam.CheckResult)
            {
                // 補值
                input.BindKey = methodParam.BindKey;
                // 設定Log的請求資料Json
                this._LogService_Main.Set_LogQueryJson<Company_Model>(methodParam.BindKey, input);
            }
            else
            {
                return Json(methodParam.ResultJsonDTO);
            }
            #endregion

            #region == 【Model驗證】避免前端驗證失效 (Error Return) ==
            // 欄位驗證
            methodParam.CheckResult = _MyCheckHelper.Check_IsValidModel<Company_Model>(ref methodParam, input, new List<string> { nameof(input.CompanyId) });
            // [T：拒絕]
            if (methodParam.CheckResult == false)
            {
                return Json(methodParam.ResultJsonDTO);
            }
            #endregion

            #region == 新增 ==
            var result = this._CompanyService_Main.Create_Company(input);
            #endregion

            // 回傳訊息
            methodParam.ResultJsonDTO = new ResultJson_DTO(result.IsSuccess, result.Title, result.Message)
            {
                logUrl = UrlTool.Get_LogSimpleUrl(_MainSystem_DTO.BindKey_ByAction),
            };
            return Json(methodParam.ResultJsonDTO);
            #endregion
        }

        /// <summary>
        /// 【View】公司修改頁面
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditView_Company(long key)
        {
            ViewBag.Permission_DTO = this._MainSystem_DTO.Permission; // 權限DTO

            var model = this._CompanyService_Main.GetEditModel_Company(key);
            if (model != null) //有資料
            {
                #region == 下拉選單-公司等級 ==
                ViewBag.DropList_CompanyLevel = new List<SelectListItem>()
                {
                   new SelectListItem()
                   {
                       Value = model.CompanyLevelId.EM_GetEnumInt().ToString(),
                       Text = model.CompanyLevelId.ToString(),
                       Selected = true,
                   }
                };
                #endregion

                return PartialView("_Company_Edit", model);
            }
            else //無資料
            {
                return Json(new { _isSuccess = false, _message = "查無資料" });
            }
        }

        /// <summary>
        /// 【EXE】公司修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [TypeFilter(typeof(MyResultFilter))]
        [TypeFilter(typeof(MyExceptionFilter))]
        [TypeFilter(typeof(IsLegal), Arguments = new object[] { E_Function.公司 })]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit_Company(Company_Model input)
        {
            #region == 參數 ==
            var methodParam = new MethodParameter(Guid.NewGuid()); // 方法的通用屬性參數
            this._MainSystem_DTO.MethodBase = System.Reflection.MethodBase.GetCurrentMethod(); // 方法Info
            this._MainSystem_DTO.BindKey_ByException = methodParam.BindKey;
            this._MainSystem_DTO.Set_LogConfig(E_DBTable.Main_Company, E_LogType.執行紀錄, E_Action.修改);
            #endregion

            #region == 處理 ==
            // 添加執行Log
            this._LogService_Main.Add_LogActionRecord(this._MainSystem_DTO.MethodBase, methodParam.BindKey, methodParam.TodayFull);

            #region == 【檢查+補值】有無input (Error Return) ==
            // 檢查有無input
            methodParam.CheckResult = _MyCheckHelper.Check_IsNullInput<Company_Model>(ref methodParam, input);
            // [T：有值][F：無值]
            if (methodParam.CheckResult)
            {
                // 補值
                input.BindKey = methodParam.BindKey;
                // 設定Log的請求資料Json
                this._LogService_Main.Set_LogQueryJson<Company_Model>(methodParam.BindKey, input);
            }
            else
            {
                return Json(methodParam.ResultJsonDTO);
            }
            #endregion

            #region == 【Model驗證】避免前端驗證失效 (Error Return) ==
            // 欄位驗證
            methodParam.CheckResult = _MyCheckHelper.Check_IsValidModel<Company_Model>(ref methodParam, input, new List<string> { nameof(input.CompanyId) });
            // [T：拒絕]
            if (methodParam.CheckResult == false)
            {
                return Json(methodParam.ResultJsonDTO);
            }
            #endregion

            #region == 修改 ==
            var result = this._CompanyService_Main.Edit_Company(input);
            #endregion

            // 回傳訊息
            methodParam.ResultJsonDTO = new ResultJson_DTO(result.IsSuccess, result.Title, result.Message)
            {
                logUrl = UrlTool.Get_LogSimpleUrl(_MainSystem_DTO.BindKey_ByAction),
            };
            return Json(methodParam.ResultJsonDTO);
            #endregion
        }

        /// <summary>
        /// 【EXE】公司刪除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [TypeFilter(typeof(MyResultFilter))]
        [TypeFilter(typeof(MyExceptionFilter))]
        [TypeFilter(typeof(IsLegal), Arguments = new object[] { E_Function.公司 })]
        [HttpPost]
        public IActionResult Delete_Company(long? key)
        {
            #region == 參數 ==
            var methodParam = new MethodParameter(Guid.NewGuid()); // 方法的通用屬性參數
            this._MainSystem_DTO.MethodBase = System.Reflection.MethodBase.GetCurrentMethod(); // 方法Info
            this._MainSystem_DTO.BindKey_ByException = methodParam.BindKey;
            this._MainSystem_DTO.Set_LogConfig(E_DBTable.Main_Company, E_LogType.執行紀錄, E_Action.刪除);
            Company_Model input = null;
            #endregion

            #region == 處理 ==
            // 添加執行Log
            this._LogService_Main.Add_LogActionRecord(this._MainSystem_DTO.MethodBase, methodParam.BindKey, methodParam.TodayFull);

            #region == 【Model】生成input ==
            // 檢查有無傳入值。 [T：有傳入，生成input]
            if (key.HasValue)
            {
                //生成input
                input = new Company_Model
                {
                    Id = key,
                    BindKey = methodParam.BindKey,
                };
            }
            #endregion

            #region == 【檢查】有無input (Error Return) ==
            // 檢查有無輸入input
            methodParam.CheckResult = _MyCheckHelper.Check_IsNullInput<Company_Model>(ref methodParam, input);
            // [T：有值][F：無值]
            if (methodParam.CheckResult)
            {
                // 設定Log的請求資料Json
                this._LogService_Main.Set_LogQueryJson<Company_Model>(methodParam.BindKey, input);
            }
            else
            {
                return Json(methodParam.ResultJsonDTO);
            }
            #endregion

            #region == 刪除 ==
            var result = this._CompanyService_Main.Delete_Company(input);
            #endregion

            // 回傳訊息
            methodParam.ResultJsonDTO = new ResultJson_DTO(result.IsSuccess, result.Title, result.Message)
            {
                logUrl = UrlTool.Get_LogSimpleUrl(_MainSystem_DTO.BindKey_ByAction),
            };
            return Json(methodParam.ResultJsonDTO);
            #endregion
        }
        #endregion
    }
}
