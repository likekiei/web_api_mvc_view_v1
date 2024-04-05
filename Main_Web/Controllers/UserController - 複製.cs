//using Main_Common.Model.Tool;
//using Main_Common.Model.Data.User;
//using Microsoft.AspNetCore.Mvc;
//using X.PagedList;
//using Main_Common.Enum.E_ProjectType;
//using Main_Common.Enum.E_StatusType;
//using Main_Service.Service.S_User;
//using Microsoft.AspNetCore.Authorization;
//using Main_Web.Filters;
//using Main_Common.Model.Main;
//using Main_Service.Service.S_Login;
//using Main_Common.Tool;
//using System.Reflection;
//using Main_Service.Service.S_Log;
//using Main_Common.Model.Result;
//using Main_Common.Model.Message;
//using Main_Web.Helper;

//namespace Main_Web.Controllers
//{
//    /// <summary>
//    /// 使用者相關
//    /// </summary>
//    [Authorize] //登入驗證
//    public class UserController : BaseWebController
//    {
//        #region == 【DI注入用宣告】 ==
//        /// <summary>
//        /// 【DTO】主系統資料
//        /// </summary>
//        public readonly MainSystem_DTO _MainSystem_DTO;
//        /// <summary>
//        /// 【Main Service】Log相關
//        /// </summary>
//        public readonly LogService_Main _LogService_Main;
//        /// <summary>
//        /// 【Main Service】登入相關
//        /// </summary>
//        public readonly LoginService_Main _LoginService_Main;
//        /// <summary>
//        /// 【Main Service】使用者相關
//        /// </summary>
//        public readonly UserService_Main _UserService_Main;
//        /// <summary>
//        /// 【Helper】通用檢查相關Helper
//        /// </summary>
//        public readonly MyCheckHelper _MyCheckHelper;
//        #endregion

//        #region == 【建構】 ==
//        /// <summary>
//        /// 建構
//        /// </summary>
//        /// <param name="httpContextAccessor">HttpContext</param>
//        /// <param name="mainSystem_DTO">主系統資料</param>
//        /// <param name="logService_Main">Log相關</param>
//        /// <param name="loginService_Main">登入相關</param>
//        /// <param name="userService_Main">使用者相關</param>
//        /// <param name="myCheckHelper">通用檢查相關Helper</param>
//        public UserController(IHttpContextAccessor httpContextAccessor,
//            MainSystem_DTO mainSystem_DTO,
//            LogService_Main logService_Main,
//            LoginService_Main loginService_Main,
//            UserService_Main userService_Main,
//            MyCheckHelper myCheckHelper)
//            : base(httpContextAccessor, mainSystem_DTO, loginService_Main)
//        {
//            this._MainSystem_DTO = mainSystem_DTO;
//            this._LogService_Main = logService_Main;
//            this._LoginService_Main = loginService_Main;
//            this._UserService_Main = userService_Main;
//            this._MyCheckHelper = myCheckHelper;
//        }
//        #endregion

//        //--【方法】=================================================================================

//        #region == 使用者 ==
//        [TypeFilter(typeof(IsLegal), Arguments = new object[] { E_Function.使用者 })]
//        public IActionResult Index_User()
//        {
//            return View();
//        }

//        /// <summary>
//        /// 【View】使用者清單查詢畫面
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost]
//        public IActionResult FilterView_UserList()
//        {
//            ViewBag.Permission_DTO = this._MainSystem_DTO.Permission; // 權限DTO

//            var model = new User_Filter { Company_ID = this._MainSystem_DTO.UserSession.Company_ID };
//            return PartialView("_User_Filter", model);
//        }

//        /// <summary>
//        /// 【View】使用者清單查詢
//        /// </summary>
//        /// <param name="input">查詢條件</param>
//        /// <returns></returns>
//        [HttpPost]
//        public IActionResult Filter_UserList(User_Filter input)
//        {
//            #region == 查詢值 ==
//            var pageingDTO = new Pageing_DTO
//            {
//                IsEnable = input.PageNumber.HasValue ? true : false,
//                PageNumber = input.PageNumber == 0 ? 1 : (input.PageNumber ?? 1),
//                PageSize = input.PageSize ?? 10,
//            };

//            //紀錄查詢條件
//            ViewBag.PageSize = input.PageSize;
//            ViewBag.Keyword = input.Keyword;
//            ViewBag.ID = input.ID;
//            ViewBag.No = input.No;
//            ViewBag.Name = input.Name;
//            ViewBag.Company_ID = input.Company_ID;
//            #endregion

//            #region == 處理 ==
//            // 取資料清單
//            var result = this._UserService_Main.GetList_User(input, pageingDTO);
//            // [T：成功][F：失敗]
//            if (result.IsSuccess)
//            {
//                #region == 判斷當前頁數是否大於總頁數  [如果超過總頁數，需重新取得Model] ==
//                //判斷頁數，避免不是第一頁卻找不到資料的情況，也就是說，如果非第一頁卻無資料，則頁數需-1
//                if (result.Pageing_DTO.IsEnable && result.Pageing_DTO.TotalCount > 0) //有啟用分頁 && 總數 > 0，才處理   [避免計算出來的總頁數是0]
//                {
//                    var overCheck = View_Tool.Check_NowPageOverTotalPage(result.Pageing_DTO);
//                    if (overCheck.IsSuccess) //當前頁超過總頁數，重新取得分頁資料
//                    {
//                        pageingDTO.PageNumber = overCheck.Data;
//                        result = this._UserService_Main.GetList_User(input, pageingDTO); //重新取得Model
//                    }
//                }
//                #endregion

//                pageingDTO = result.Pageing_DTO;
//                var model = new StaticPagedList<User_List>(result.Data, pageingDTO.PageNumber, pageingDTO.PageSize, pageingDTO.TotalCount);
//                return PartialView("_User_List", model);
//            }
//            else
//            {
//                return Json(new { _isSuccess = false, _message = result.Title + result.Message });
//            }
//            #endregion
//        }

//        /// <summary>
//        /// 【View】使用者新增頁面
//        /// </summary>
//        /// <param name="input"></param>
//        /// <returns></returns>
//        [HttpPost]
//        public IActionResult CreateView_User()
//        {
//            ViewBag.Permission_DTO = this._MainSystem_DTO.Permission; // 權限DTO

//            var model = new User_Model
//            {
//                CRUD = E_CRUD.C,
//                Company_ID = this._MainSystem_DTO.UserSession.Company_ID,
//            };

//            return PartialView("_User_Create", model);
//        }

//        /// <summary>
//        /// 【EXE】使用者新增
//        /// </summary>
//        /// <param name="input"></param>
//        /// <returns></returns>
//        [TypeFilter(typeof(IsLegal), Arguments = new object[] { E_Function.使用者 })]
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create_User(User_Model input) 
//        {
//            #region == 參數 ==
//            var todayFull = DateTime.UtcNow.AddHours(8); // 當前時間(含毫秒)
//            var today = Convert.ToDateTime(todayFull.ToString()); // 當前時間(不含毫秒)
//            #endregion

//            #region == 處理 ==
//            var result = this._UserService_Main.Create_User(input);
//            #endregion

//            result.Message += string.IsNullOrEmpty(result.Message_Other) ? "" : $"\r\n【其他】\r\n{result.Message_Other}";
//            return Json(new { _isSuccess = result.IsSuccess, _message = result.Title + result.Message });
//        }

//        /// <summary>
//        /// 【View】使用者修改頁面
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        [HttpPost]
//        public IActionResult EditView_User(long key)
//        {
//            ViewBag.Permission_DTO = this._MainSystem_DTO.Permission; // 權限DTO

//            var model = this._UserService_Main.GetEditModel_User(key);
//            if (model != null) //有資料
//            {
//                return PartialView("_User_Edit", model);
//            }
//            else //無資料
//            {
//                return Json(new { _isSuccess = false, _message = "查無資料" });
//            }
//        }

//        ///// <summary>
//        ///// 【EXE】使用者修改
//        ///// </summary>
//        ///// <param name="input"></param>
//        ///// <returns></returns>
//        //[TypeFilter(typeof(IsLegal), Arguments = new object[] { E_Function.使用者 })]
//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //public IActionResult Edit_User(User_Model input)
//        //{
//        //    #region == 參數 ==
//        //    var todayFull = DateTime.UtcNow.AddHours(8); // 當前時間(含毫秒)
//        //    var today = Convert.ToDateTime(todayFull.ToString()); // 當前時間(不含毫秒)
//        //    #endregion

//        //    #region == 處理 ==
//        //    var result = this._UserService_Main.Edit_User(input);
//        //    #endregion

//        //    result.Message += string.IsNullOrEmpty(result.Message_Other) ? "" : $"\r\n【其他】\r\n{result.Message_Other}";
//        //    return Json(new { _isSuccess = result.IsSuccess, _message = result.Title + result.Message });
//        //}

//        /// <summary>
//        /// 【EXE】使用者修改
//        /// </summary>
//        /// <param name="input"></param>
//        /// <returns></returns>
//        [TypeFilter(typeof(IsLegal), Arguments = new object[] { E_Function.使用者 })]
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit_User(User_Model input)
//        {
//            #region == 【參數】 ==
//            bool check = false; // 共用判斷
//            MethodParameter methodParam = new MethodParameter(Guid.NewGuid()); // 方法的通用屬性參數
//            this._MainSystem_DTO.MethodBase = System.Reflection.MethodBase.GetCurrentMethod(); // 方法Info
//            this._MainSystem_DTO.BindKey_ByException = methodParam.BindKey;
//            this._MainSystem_DTO.Set_LogConfig(E_DBTable.Main_Company, E_LogType.執行紀錄, E_Action.修改);
//            #endregion

//            #region == 【處理】 ==
//            // 不論執行過程是否異常，最後都要執行Log整理與Log寫入
//            try
//            {
//                // 添加執行Log
//                this._LogService_Main.Add_LogActionRecord(this._MainSystem_DTO.MethodBase, methodParam.BindKey, methodParam.TodayFull);

//                #region == 【檢查】有無input值 (Error Return) ==
//                // [有無input][T：有，補值][F：無值]
//                if (input != null)
//                {
//                    input.Bind_Key = methodParam.BindKey;
//                }

//                // 檢查是否有無輸入input
//                check = _MyCheckHelper.Check_IsNullInput<User_Model>(ref methodParam, input);
//                // [T：拒絕]
//                if(check == false)
//                {
//                    return Json(methodParam.ResultJsonDTO);
//                }
//                #endregion

//                #region == 【Model驗證】避免前端驗證失效 (Error Return) ==
//                // 檢查是否有無輸入input
//                check = _MyCheckHelper.Check_IsValidModel<User_Model>(ref methodParam, input, new List<string> { /*nameof(input.No)*/ });
//                // [T：拒絕]
//                if (check == false)
//                {
//                    return Json(methodParam.ResultJsonDTO);
//                }
//                #endregion

//                #region == 【處理】 ==
//                var result = this._UserService_Main.Edit_User(input);
//                #endregion

//                // 回傳訊息
//                methodParam.ResultJsonDTO = new ResultJson_DTO(result.IsSuccess, result.Title, result.Message)
//                {
//                    logUrl = UrlTool.Get_LogSimpleUrl(_MainSystem_DTO.BindKey_ByAction),
//                };
//                return Json(methodParam.ResultJsonDTO);
//            }
//            catch (Exception)
//            {
//                // 添加Log訊息
//                methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.處理異常, "");
//                methodParam.MessageDTO.Message = $"執行過程發生例外錯誤，建議聯絡相關人員";
//                this._LogService_Main.Add_LogResultMessage(input.Bind_Key, methodParam.MessageDTO);

//                return Json(new { _isSuccess = false, _message = methodParam.MessageDTO.Message });
//            }
//            finally
//            {
//                this._LogService_Main.Refresh_LogInfo();
//                this._LogService_Main.Create_Log();
//            }
//            #endregion
//        }

//        ///// <summary>
//        ///// 【EXE】使用者修改
//        ///// </summary>
//        ///// <param name="input"></param>
//        ///// <returns></returns>
//        //[TypeFilter(typeof(IsLegal), Arguments = new object[] { E_Function.使用者 })]
//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //public IActionResult Edit_User(User_Model input)
//        //{
//        //    ResultJson_DTO resultJsonDTO = null;

//        //    try
//        //    {
//        //        #region == 參數 ==
//        //        var todayFull = DateTime.UtcNow.AddHours(8); // 當前時間(含毫秒)
//        //        var today = Convert.ToDateTime(todayFull.ToString()); // 當前時間(不含毫秒)
//        //        #endregion

//        //        #region == TEST ==
//        //        var methodInfo = System.Reflection.MethodBase.GetCurrentMethod();
//        //        //var tt = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
//        //        //var ttA = System.Reflection.MethodBase.GetCurrentMethod().Name;
//        //        //var ttB = $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}";

//        //        var t = this._MainSystem_DTO;

//        //        // 模擬批量處理

//        //        // 傳入的查詢值
//        //        var filter = new User_Filter()
//        //        {
//        //            Keyword = "A",
//        //            Bind_Key = Guid.NewGuid(),
//        //        };

//        //        // 添加執行Log
//        //        this._LogService_Main.Add_LogActionRecord(methodInfo, filter.Bind_Key, todayFull, E_DBTable.Main_User, E_LogType.執行紀錄, E_Action.修改);
//        //        // 設定Log的請求資料Json
//        //        this._LogService_Main.Set_LogQueryJson<User_Filter>(filter.Bind_Key, filter);

//        //        var inputs = new List<User_Model>
//        //        {
//        //            _UserService_Main.GetModel_User_Edit(1),
//        //            _UserService_Main.GetModel_User_Edit(2),
//        //        };

//        //        inputs[0].Rem = "A";
//        //        inputs[0].Bind_Key = Guid.NewGuid();

//        //        inputs[1].Rem = "B";
//        //        inputs[1].Bind_Key = Guid.NewGuid();

//        //        // 添加執行Log
//        //        foreach (var item in inputs)
//        //        {
//        //            // 添加執行Log
//        //            this._LogService_Main.Add_LogActionRecord(methodInfo, item.Bind_Key, todayFull, E_DBTable.Main_User, E_LogType.處理紀錄, E_Action.修改);
//        //            // 設定Log的請求資料Json
//        //            this._LogService_Main.Set_LogQueryJson<User_Model>(item.Bind_Key, item);
//        //        }
//        //        #endregion

//        //        #region == 處理 ==
//        //        var m1 = this._MainSystem_DTO;

//        //        var result = this._UserService_Main.Edits_User(inputs);

//        //        var m2 = this._MainSystem_DTO;
//        //        #endregion

//        //        resultJsonDTO = new ResultJson_DTO(result.IsSuccess, result.Title, result.Message) 
//        //        {
//        //            logUrl = UrlTool.Get_LogSimpleUrl(_MainSystem_DTO.BindKey_ByAction),
//        //        };
//        //        return Json(resultJsonDTO);
//        //    }
//        //    catch (Exception)
//        //    {
//        //        return Json(new { _isSuccess = false, _message = "未知錯誤，請聯繫相關人員" });
//        //    }
//        //    finally
//        //    {
//        //        this._LogService_Main.Refresh_LogInfo();
//        //        this._LogService_Main.Create_Log();
//        //    }
//        //}

//        /// <summary>
//        /// 添加群組Log紀錄
//        /// </summary>
//        /// <param name="methodBase"></param>
//        /// <param name="guids"></param>
//        /// <param name="todayFull"></param>
//        /// <param name="eDBTable"></param>
//        /// <param name="eAction"></param>
//        //public void Add_Log_ByGroupRecord(MethodBase methodBase, List<Guid> guids, DateTime todayFull, E_DBTable eDBTable, E_Action eAction)
//        //{
//        //    var functionPath = $"{methodBase.DeclaringType.FullName}.{methodBase.Name}";

//        //    _MainSystem_DTO.LogList = guids.Select(x => new Log_Model
//        //    {
//        //        IsSuccess = false, // 還沒處理過，先默認失敗
//        //        DBTableID = eDBTable,
//        //        ActionID = eAction,
//        //        StatusCodeID = E_StatusCode.Default,
//        //        Message = "【執行紀錄】",
//        //        FunctionPath = functionPath,
//        //        //Function_Path = "Main_Web/InBoundOrderController/Create_InBoundOrder_MF",
//        //        CompanyID = this._MainSystem_DTO.UserSession.Company_ID,
//        //        CompanyName = this._MainSystem_DTO.UserSession.Company_Name,
//        //        UserID = this._MainSystem_DTO.UserSession.User_ID,
//        //        UserName = this._MainSystem_DTO.UserSession.User_Name,
//        //        LogDate = todayFull,
//        //        ActionDate = todayFull,
//        //        BindKey = x,
//        //        BindKey_ByAction = _MainSystem_DTO.BindKey_ByAction,
//        //        BindKey_ByGroup = x,
//        //    }).ToList();


//        //    //methodBase.DeclaringType.FullName;

//        //    //var methodInfo = System.Reflection.MethodBase.GetCurrentMethod();
//        //    //var tt = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
//        //    //var ttA = System.Reflection.MethodBase.GetCurrentMethod().Name;
//        //    //var ttB = $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}";
//        //}



//        /// <summary>
//        /// 【EXE】使用者刪除
//        /// </summary>
//        /// <param name="input"></param>
//        /// <returns></returns>
//        [TypeFilter(typeof(IsLegal), Arguments = new object[] { E_Function.使用者 })]
//        [HttpPost]
//        public IActionResult Delete_User(long? key)
//        {
//            #region == 參數 ==
//            var todayFull = DateTime.UtcNow.AddHours(8); // 當前時間(含毫秒)
//            var today = Convert.ToDateTime(todayFull.ToString()); // 當前時間(不含毫秒)
//            #endregion

//            #region == 處理 ==
//            var input = new User_Model { ID = key };
//            var result = this._UserService_Main.Delete_User(input);
//            #endregion

//            result.Message += string.IsNullOrEmpty(result.Message_Other) ? "" : $"\r\n【其他】\r\n{result.Message_Other}";
//            return Json(new { _isSuccess = result.IsSuccess, _message = result.Title + result.Message });
//        }
//        #endregion
//    }
//}
