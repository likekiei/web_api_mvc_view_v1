using Main_Common.Model.Tool;
using Main_Common.Model.Data.User;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Main_Common.Enum.E_ProjectType;
using Main_Common.Enum.E_StatusType;
using Main_Service.Service.S_User;
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
using Main_Common.ExtensionMethod;
using Main_Common.GlobalSetting;
using Main_Common.Model.Data.User;
using Main_Common.Model.Data;
using Main_Service.Service.S_User;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Main_Web.Controllers
{
    /// <summary>
    /// 使用者相關
    /// </summary>
    [Authorize] //登入驗證
    public class UserController : BaseWebController
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
        /// <summary>
        /// 【Main Service】使用者相關
        /// </summary>
        public readonly UserService_Main _UserService_Main;
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
        /// <param name="userService_Main">使用者相關</param>
        /// <param name="myCheckHelper">通用檢查相關Helper</param>
        public UserController(IHttpContextAccessor httpContextAccessor,
            MainSystem_DTO mainSystem_DTO,
            LogService_Main logService_Main,
            LoginService_Main loginService_Main,
            UserService_Main userService_Main,
            MyCheckHelper myCheckHelper)
            : base(httpContextAccessor, mainSystem_DTO, loginService_Main)
        {
            this._MainSystem_DTO = mainSystem_DTO;
            this._LogService_Main = logService_Main;
            this._LoginService_Main = loginService_Main;
            this._UserService_Main = userService_Main;
            this._MyCheckHelper = myCheckHelper;
        }
        #endregion

        //--【方法】=================================================================================

        #region == 使用者 ==
        [TypeFilter(typeof(IsLegal), Arguments = new object[] { E_Function.使用者 })]
        public IActionResult Index_User()
        {
            return View();
        }

        /// <summary>
        /// 【View】使用者清單查詢畫面
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult FilterView_UserList()
        {
            ViewBag.Permission_DTO = this._MainSystem_DTO.Permission; // 權限DTO

            //var model = new User_Filter { User_Id = this._MainSystem_DTO.UserSession.User_Id };
            var model = new User_Filter
            {
                CompanyId = this._MainSystem_DTO.UserSession.CompanyId,
            };
            return PartialView("_User_Filter", model);
        }

        /// <summary>
        /// 【View】使用者清單查詢
        /// </summary>
        /// <param name="input">查詢條件</param>
        /// <returns></returns>
        [TypeFilter(typeof(MyResultFilter))]
        [TypeFilter(typeof(MyExceptionFilter))]
        [HttpPost]
        public IActionResult Filter_UserList(User_Filter input)
        {
            #region == 參數 ==
            var methodParam = new MethodParameter(Guid.NewGuid()); // 方法的通用屬性參數
            this._MainSystem_DTO.MethodBase = System.Reflection.MethodBase.GetCurrentMethod(); // 方法Info
            this._MainSystem_DTO.BindKey_ByException = methodParam.BindKey;
            this._MainSystem_DTO.Set_LogConfig(E_DBTable.Main_User, E_LogType.執行紀錄, E_Action.查詢);
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
            ViewBag.CompanyId = input.CompanyId;
            ViewBag.Id = input.Id;
            ViewBag.No = input.No;
            ViewBag.Name = input.Name;
            #endregion

            #region == 處理 ==
            // 取資料清單
            var result = this._UserService_Main.GetList_User(input, pageingDTO);
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
                        result = this._UserService_Main.GetList_User(input, pageingDTO); //重新取得Model
                    }
                }
                #endregion

                pageingDTO = result.PageingDTO;
                var model = new StaticPagedList<User_List>(result.Data, pageingDTO.PageNumber, pageingDTO.PageSize, pageingDTO.TotalCount);
                return PartialView("_User_List", model);
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
        /// 【View】使用者新增頁面
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateView_User()
        {
            ViewBag.Permission_DTO = this._MainSystem_DTO.Permission; // 權限DTO

            var model = new User_Model
            {
                CRUD = E_CRUD.C,
                CompanyId = this._MainSystem_DTO.UserSession.CompanyId,
            };

            #region == 下拉選單-角色 ==
            ViewBag.DropList_Role = new List<SelectListItem>();
            #endregion

            return PartialView("_User_Create", model);
        }

        /// <summary>
        /// 【EXE】使用者新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [TypeFilter(typeof(MyResultFilter))]
        [TypeFilter(typeof(MyExceptionFilter))]
        [TypeFilter(typeof(IsLegal), Arguments = new object[] { E_Function.使用者 })]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create_User(User_Model input)
        {
            #region == 參數 ==
            var methodParam = new MethodParameter(Guid.NewGuid()); // 方法的通用屬性參數
            this._MainSystem_DTO.MethodBase = System.Reflection.MethodBase.GetCurrentMethod(); // 方法Info
            this._MainSystem_DTO.BindKey_ByException = methodParam.BindKey;
            this._MainSystem_DTO.Set_LogConfig(E_DBTable.Main_User, E_LogType.執行紀錄, E_Action.新增);
            #endregion

            #region == 處理 ==
            // 添加執行Log
            this._LogService_Main.Add_LogActionRecord(this._MainSystem_DTO.MethodBase, methodParam.BindKey, methodParam.TodayFull);

            #region == 【檢查+補值】有無input (Error Return) ==
            // 檢查有無input
            methodParam.CheckResult = _MyCheckHelper.Check_IsNullInput<User_Model>(ref methodParam, input);
            // [T：有值][F：無值]
            if (methodParam.CheckResult)
            {
                // 補值
                input.BindKey = methodParam.BindKey;
                // 設定Log的請求資料Json
                this._LogService_Main.Set_LogQueryJson<User_Model>(methodParam.BindKey, input);
            }
            else
            {
                return Json(methodParam.ResultJsonDTO);
            }
            #endregion

            #region == 【Model驗證】避免前端驗證失效 (Error Return) ==
            // 欄位驗證
            methodParam.CheckResult = _MyCheckHelper.Check_IsValidModel<User_Model>(ref methodParam, input, new List<string> { nameof(input.Id) });
            // [T：拒絕]
            if (methodParam.CheckResult == false)
            {
                return Json(methodParam.ResultJsonDTO);
            }
            #endregion

            #region == 新增 ==
            var result = this._UserService_Main.Create_User(input);
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
        /// 【View】使用者修改頁面
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditView_User(long key)
        {
            ViewBag.Permission_DTO = this._MainSystem_DTO.Permission; // 權限DTO

            var model = this._UserService_Main.GetEditModel_User(key);
            if (model != null) //有資料
            {
                #region == 下拉選單-使用者等級 ==
                ViewBag.DropList_Role = new List<SelectListItem>()
                {
                   new SelectListItem()
                   {
                       Value = model.RoleId.Value.ToString(),
                       Text = model.RoleName,
                       Selected = true,
                   }
                };
                #endregion

                return PartialView("_User_Edit", model);
            }
            else //無資料
            {
                return Json(new { _isSuccess = false, _message = "查無資料" });
            }
        }

        /// <summary>
        /// 【EXE】使用者修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [TypeFilter(typeof(MyResultFilter))]
        [TypeFilter(typeof(MyExceptionFilter))]
        [TypeFilter(typeof(IsLegal), Arguments = new object[] { E_Function.使用者 })]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit_User(User_Model input)
        {
            #region == 參數 ==
            var methodParam = new MethodParameter(Guid.NewGuid()); // 方法的通用屬性參數
            this._MainSystem_DTO.MethodBase = System.Reflection.MethodBase.GetCurrentMethod(); // 方法Info
            this._MainSystem_DTO.BindKey_ByException = methodParam.BindKey;
            this._MainSystem_DTO.Set_LogConfig(E_DBTable.Main_User, E_LogType.執行紀錄, E_Action.修改);
            #endregion

            #region == 處理 ==
            // 添加執行Log
            this._LogService_Main.Add_LogActionRecord(this._MainSystem_DTO.MethodBase, methodParam.BindKey, methodParam.TodayFull);

            #region == 【檢查+補值】有無input (Error Return) ==
            // 檢查有無input
            methodParam.CheckResult = _MyCheckHelper.Check_IsNullInput<User_Model>(ref methodParam, input);
            // [T：有值][F：無值]
            if (methodParam.CheckResult)
            {
                // 補值
                input.BindKey = methodParam.BindKey;
                // 設定Log的請求資料Json
                this._LogService_Main.Set_LogQueryJson<User_Model>(methodParam.BindKey, input);
            }
            else
            {
                return Json(methodParam.ResultJsonDTO);
            }
            #endregion

            #region == 【Model驗證】避免前端驗證失效 (Error Return) ==
            // 欄位驗證
            methodParam.CheckResult = _MyCheckHelper.Check_IsValidModel<User_Model>(ref methodParam, input, new List<string> { /*nameof(input.User_Id)*/ });
            // [T：拒絕]
            if (methodParam.CheckResult == false)
            {
                return Json(methodParam.ResultJsonDTO);
            }
            #endregion

            #region == 修改 ==
            var result = this._UserService_Main.Edit_User(input);
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
        /// 【EXE】使用者刪除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [TypeFilter(typeof(MyResultFilter))]
        [TypeFilter(typeof(MyExceptionFilter))]
        [TypeFilter(typeof(IsLegal), Arguments = new object[] { E_Function.使用者 })]
        [HttpPost]
        public IActionResult Delete_User(long? key)
        {
            #region == 參數 ==
            var methodParam = new MethodParameter(Guid.NewGuid()); // 方法的通用屬性參數
            this._MainSystem_DTO.MethodBase = System.Reflection.MethodBase.GetCurrentMethod(); // 方法Info
            this._MainSystem_DTO.BindKey_ByException = methodParam.BindKey;
            this._MainSystem_DTO.Set_LogConfig(E_DBTable.Main_User, E_LogType.執行紀錄, E_Action.刪除);
            User_Model input = null;
            #endregion

            #region == 處理 ==
            // 添加執行Log
            this._LogService_Main.Add_LogActionRecord(this._MainSystem_DTO.MethodBase, methodParam.BindKey, methodParam.TodayFull);

            #region == 【Model】生成input ==
            // 檢查有無傳入值。 [T：有傳入，生成input]
            if (key.HasValue)
            {
                //生成input
                input = new User_Model
                {
                    Id = key,
                    BindKey = methodParam.BindKey,
                };
            }
            #endregion

            #region == 【檢查】有無input (Error Return) ==
            // 檢查有無輸入input
            methodParam.CheckResult = _MyCheckHelper.Check_IsNullInput<User_Model>(ref methodParam, input);
            // [T：有值][F：無值]
            if (methodParam.CheckResult)
            {
                // 設定Log的請求資料Json
                this._LogService_Main.Set_LogQueryJson<User_Model>(methodParam.BindKey, input);
            }
            else
            {
                return Json(methodParam.ResultJsonDTO);
            }
            #endregion

            #region == 刪除 ==
            var result = this._UserService_Main.Delete_User(input);
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
