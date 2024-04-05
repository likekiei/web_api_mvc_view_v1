//using Main_Common.Enum;
//using Main_Common.Enum.E_StatusType;
//using Main_Common.Model.DTO;
//using Main_Common.Model.Result;
//using Main_Common.Model.ResultApi;
//using Main_Common.Model.Search;
//using Main_Common.Mothod.Message;
//using Main_Common.Mothod.Page;
//using Main_Common.Mothod.Validation;
//using Web_API.Filters;

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using Main_Common.Model.DTO.Cust;
//using Main_Common.Model.Data;
//using Main_Common.Model.Main;
//using Main_Service.Service.S_Company;
//using Main_Service.Service.S_Log;
//using Main_Service.Service.S_Login;
//using Web_API_APP.Service;
//using Main_Common.Model.Account;
//using Main_Common.ExtensionMethod;
//using Main_Common.GlobalSetting;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Http;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Mvc;
//using Jose;
//using System.Text;
//using Main_Service.Service.S_User;
//using Newtonsoft.Json;
//using Azure.Core;
//using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json.Linq;
//using Main_Common.Model.DTO.Employee;
//using Main_Common.Model.ERP;
//using System.Data;

//namespace Web_API.Controllers
//{
//    /// <summary>
//    /// 客戶相關作業
//    /// </summary>  
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    public class EmployeeController : ControllerBase // _APIController
//    {
//        #region == 【DI注入用宣告】 ==
//        /// <summary>
//        /// 【DTO】主系統資料
//        /// </summary>
//        public readonly MainSystem_DTO _MainSystem_DTO;
//        /// <summary>
//        /// 【API Service】客戶相關
//        /// </summary>
//        public readonly Employee_Service _Employee_Service;
//        /// <summary>
//        /// 【Main Service】Log相關
//        /// </summary>
//        public readonly LogService_Main _LogService_Main;
//        ///// <summary>
//        ///// 【Main Service】登入相關
//        ///// </summary>
//        public readonly LoginService_Main _Login_Service_Main;
//        ///// <summary>
//        ///// 【API APP】登入相關
//        ///// </summary>
//        public readonly Login_Service _Login_Service;
//        /// <summary>
//        /// 【Helper】通用檢查相關Helper
//        /// </summary>
//        //public readonly MyCheckHelper _MyCheckHelper;
//        #endregion

//        #region == 【全域宣告】 ==
//        /// <summary>
//        /// 登入者資訊
//        /// </summary>
//        //protected internal UserSession_Model _UserSession_Model { get; private set; }
//        #endregion

//        #region == 【建構】 ==
//        /// <summary>
//        /// 建構
//        /// </summary>
//        ///// <param name="httpContextAccessor">HttpContext</param>
//        ///// <param name="mainSystem_DTO">主系統資料</param>
//        /// <param name="logService_Main">Log相關</param>
//        /// <param name="loginService_Main">登入相關</param>
//        ///// <param name="companyService_Main">公司相關</param>
//        ///// <param name="myCheckHelper">通用檢查相關Helper</param>
//        public EmployeeController(
//            IHttpContextAccessor httpContextAccessor,
//            MainSystem_DTO mainSystem_DTO,
//            Employee_Service _employee_Service,
//            UserService_Main _user_Service,
//            //CompanyService_Main _company_Service,
//            //UserSession_Model _userSession_Model,

//            LogService_Main logService_Main,
//            LoginService_Main _login_Service_Main,
//            Login_Service _login_Service
//            //UserSession_Model _userSession_Model
//            )
//        //MyCheckHelper myCheckHelper)
//        //   : base(httpContextAccessor) // base(httpContextAccessor)
//        {
//            this._MainSystem_DTO = mainSystem_DTO;
//            this._Employee_Service = _employee_Service;

//            //this._CompanyService_Main = companyService_Main;
//            this._LogService_Main = logService_Main;
//            this._Login_Service_Main = _login_Service_Main;
//            this._Login_Service = _login_Service;
//            //this._UserSession_Model = _userSession_Model;
//            //this._MyCheckHelper = myCheckHelper;            
//        }
//        #endregion

//        //--【方法】=================================================================================
//        #region == 【全域變數】參數、屬性 ==
//        private Guid? Com_Bind_Key = null; //共用綁定Key(使用前請先重置)
//        private string Com_MainKey = null; //共用主要Key(使用前請先重置)
//        private string LogErrorMsg = null; //共用Log錯誤訊息(使用前請先重置)
//        private string ResultMsg_Finally = null; //共用最終回傳訊息(使用前請先重置)
//        private bool Com_Result = false; //共用結果(使用前請先重置)
//        private Message_Model Com_Message_DTO = null; //共用訊息結果(使用前請先重置)
//        private ResultOutput Com_Result_DTO = null; //共用結果(使用前請先重置)
//        private ResultOutput Com_Result_DTO_Log = null; //共用結果(使用前請先重置)
//        private List<string> Com_TextList = null; //共用文字清單(使用前請先重置)
//        private List<string> Com_Split = null; //共用Split結果(使用前請先重置)
//        private List<SelectItemDTO> DropList_DTO = null; //共用下拉清單(使用前請先重置)
//        #endregion

//        //--【方法】=================================================================================      
//        #region == 員工相關 ==
//        /// <summary>
//        /// 取得全部員工基本資料
//        /// </summary>
//        [HttpPost]
//        [TypeFilter(typeof(Logs))]
//        public EmployeeResult GetEmployees(Employee_Filter input)
//        {
//            UserSession_Model _UserSession_Model = new UserSession_Model();
//            var result = new EmployeeResult();

//            //取得遠端Token
//            var _token = _Login_Service.GetToken();

//            //取token檢查登入狀態
//            var token = Request.Headers["Authorization"].ToString();
//            var _check = _Login_Service_Main.Check_Login(token);

//            if (!_check.IsSuccess)
//            {
//                result.IsSuccess = _check.IsSuccess;
//                result.E_StatusCode = _check.E_StatusCode;
//                result.Title = _check.Title;
//                result.Message = _check.Message;
//                return result;
//            }
//            else
//            {
//                _UserSession_Model = _check._UserSession_Model;

//                #region == 處理 ==
//                // 取得清單
//                var datas = _Employee_Service.GetEmployees(input);
//                // [T：成功][F：失敗]
//                if (datas.IsSuccess)
//                {
//                    return new EmployeeResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message, datas.Pageing_DTO, datas.Data);
//                }
//                else
//                {
//                    return new EmployeeResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message);
//                }
//                #endregion
//            }
//        }

//        /// <summary>
//        /// 依查詢取得員工基本資料
//        /// </summary>
//        /// <param name="input"></param>
//        /// <returns></returns>
//        [HttpPost]
//        [TypeFilter(typeof(Logs))]
//        public EmployeeResult GetEmployee(Employee_Filter input)
//        {
//            UserSession_Model _UserSession_Model = new UserSession_Model();
//            var result = new EmployeeResult();

//            //取token檢查登入狀態
//            var token = Request.Headers["Authorization"].ToString();
//            var _check = _Login_Service_Main.Check_Login(token);

//            if (!_check.IsSuccess)
//            {
//                result.IsSuccess = _check.IsSuccess;
//                result.E_StatusCode = _check.E_StatusCode;
//                result.Title = _check.Title;
//                result.Message = _check.Message;
//                return result;
//            }
//            else
//            {
//                _UserSession_Model = _check._UserSession_Model;

//                #region == 處理 ==
//                // 取得清單
//                var datas = _Employee_Service.GetEmployee(input);
//                // [T：成功][F：失敗]
//                if (datas.IsSuccess)
//                {
//                    return new EmployeeResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message, datas.Pageing_DTO, datas.Data);
//                }
//                else
//                {
//                    return new EmployeeResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message);
//                }
//                #endregion
//            }
//        }

//        /// <summary>
//        /// 取得今日有更新之員工資料
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost]
//        [TypeFilter(typeof(Logs))]
//        public EmployeeResult GetUpdateUser()
//        {
//            UserSession_Model _UserSession_Model = new UserSession_Model();
//            var result = new EmployeeResult();

//            //取token檢查登入狀態
//            var token = Request.Headers["Authorization"].ToString();
//            var _check = _Login_Service_Main.Check_Login(token);

//            if (!_check.IsSuccess)
//            {
//                result.IsSuccess = _check.IsSuccess;
//                result.E_StatusCode = _check.E_StatusCode;
//                result.Title = _check.Title;
//                result.Message = _check.Message;
//                return result;
//            }
//            else
//            {
//                _UserSession_Model = _check._UserSession_Model;

//                #region == 處理 ==
//                // 取得清單
//                var datas = _Employee_Service.GetUpdateUser();
//                // [T：成功][F：失敗]
//                if (datas.IsSuccess)
//                {
//                    return new EmployeeResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message, datas.Pageing_DTO, datas.Data);
//                }
//                else
//                {
//                    return new EmployeeResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message);
//                }
//                #endregion
//            }
//        }

//        /// <summary>
//        /// 更新離職員工
//        /// </summary>
//        [HttpPost]
//        [TypeFilter(typeof(Logs))]
//        public EmployeeResult UpdateLeaveEmployee()
//        {
//            UserSession_Model _UserSession_Model = new UserSession_Model();
//            var result = new EmployeeResult();

//            //取token檢查登入狀態
//            var token = Request.Headers["Authorization"].ToString();
//            var _check = _Login_Service_Main.Check_Login(token);

//            if (!_check.IsSuccess)
//            {
//                result.IsSuccess = _check.IsSuccess;
//                result.E_StatusCode = _check.E_StatusCode;
//                result.Title = _check.Title;
//                result.Message = _check.Message;
//                return result;
//            }
//            else
//            {
//                _UserSession_Model = _check._UserSession_Model;

//                #region == 處理 ==
//                // 取得清單
//                var datas = _Employee_Service.UpdateLeaveEmployee();
//                // [T：成功][F：失敗]
//                if (datas.IsSuccess)
//                {
//                    return new EmployeeResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message, datas.Pageing_DTO, datas.Data);
//                }
//                else
//                {
//                    return new EmployeeResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message);
//                }
//                #endregion
//            }
//        }

//        /// <summary>
//        /// 更新請假資訊
//        /// </summary>
//        [HttpPost]
//        [TypeFilter(typeof(Logs))]
//        public LeaveResult UpdateAttendanceRecord(DateTime? day)
//        {
//            UserSession_Model _UserSession_Model = new UserSession_Model();
//            var result = new LeaveResult();

//            var today = DateTime.UtcNow.AddHours(8).AddDays(-1).Date;
//            if (day.HasValue)
//            {
//                today = day.Value;
//            }

//            //取token檢查登入狀態
//            var token = Request.Headers["Authorization"].ToString();
//            var _check = _Login_Service_Main.Check_Login(token);

//            if (!_check.IsSuccess)
//            {
//                result.IsSuccess = _check.IsSuccess;
//                result.E_StatusCode = _check.E_StatusCode;
//                result.Title = _check.Title;
//                result.Message = _check.Message;
//                return result;
//            }
//            else
//            {
//                _UserSession_Model = _check._UserSession_Model;

//                #region == 處理 ==
//                // 取得清單
//                var datas = _Employee_Service.UpdateAttendanceRecord(today);
//                // [T：成功][F：失敗]
//                if (datas.IsSuccess)
//                {
//                    return new LeaveResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message, datas.Pageing_DTO, datas.Data);
//                }
//                else
//                {
//                    return new LeaveResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message);
//                }
//                #endregion
//            }
//        }

//        /// <summary>
//        /// 更新員工上班打卡紀錄
//        /// </summary>
//        //public void UpdateClock(DateTime? day)
//        //{
//        //    UserSession_Model _UserSession_Model = new UserSession_Model();
//        //    var result = new LeaveResult();

//        //    var today = DateTime.UtcNow.AddHours(8).AddDays(-1).Date;
//        //    if (day.HasValue)
//        //    {
//        //        today = day.Value;
//        //    }

//        //    //取token檢查登入狀態
//        //    var token = Request.Headers["Authorization"].ToString();
//        //    var _check = _Login_Service_Main.Check_Login(token);

//        //    if (!_check.IsSuccess)
//        //    {
//        //        result.IsSuccess = _check.IsSuccess;
//        //        result.E_StatusCode = _check.E_StatusCode;
//        //        result.Title = _check.Title;
//        //        result.Message = _check.Message;
//        //        //return result;
//        //    }
//        //    else
//        //    {
//        //        _UserSession_Model = _check._UserSession_Model;

//        //        #region == 處理 ==
//        //        // 取得清單
//        //        var datas = _Employee_Service.UpdateAttendanceRecord(today);
//        //        // [T：成功][F：失敗]
//        //        if (datas.IsSuccess)
//        //        {
//        //            //return new LeaveResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message, datas.Pageing_DTO, datas.Data);
//        //        }
//        //        else
//        //        {
//        //            //return new LeaveResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message);
//        //        }
//        //        #endregion
//        //    }


//        //    ////取得昨天日期
//        //    //var today = DateTime.UtcNow.AddHours(8).AddDays(-1).Date;
//        //    //if (day.HasValue)
//        //    //{
//        //    //    today = day.Value;
//        //    //}

//        //    //#region 1.取得搬家平台提供之目前打卡資訊
//        //    //var _empService = new A_EmployeeService(Token);
//        //    //var data = _empService.PunchRecords(today);

//        //    //var _YGService = new YGService(dbSun);
//        //    //var items = data.Select(x => new MF_CLOCK
//        //    //{
//        //    //    CLOCK_ID = _YGService.GetNO(x.idNumber, x.recordedDateTime),
//        //    //    YG_NO = _YGService.GetNO(x.idNumber, x.recordedDateTime),
//        //    //    CLOCK_TIME = x.recordedDateTime
//        //    //}).ToList();

//        //    //items = items.Where(x => !string.IsNullOrEmpty(x.YG_NO)).ToList();
//        //    //#endregion

//        //    //#region 2.資料寫入ERP
//        //    //if (data != null)
//        //    //{
//        //    //    _ClockService = new ClockService(dbSun);
//        //    //    _ClockService.Update(items);
//        //    //}
//        //    //#endregion

//        //}

//        /// <summary>
//        /// 【最新】更新員工遲到紀錄
//        /// </summary>
//        //public void UpdateLateRecord(DateTime? day)
//        //{
//        //    UserSession_Model _UserSession_Model = new UserSession_Model();
//        //    var result = new LeaveResult();

//        //    var today = DateTime.UtcNow.AddHours(8).AddDays(-1).Date;
//        //    if (day.HasValue)
//        //    {
//        //        today = day.Value;
//        //    }

//        //    //取token檢查登入狀態
//        //    var token = Request.Headers["Authorization"].ToString();
//        //    var _check = _Login_Service_Main.Check_Login(token);

//        //    if (!_check.IsSuccess)
//        //    {
//        //        result.IsSuccess = _check.IsSuccess;
//        //        result.E_StatusCode = _check.E_StatusCode;
//        //        result.Title = _check.Title;
//        //        result.Message = _check.Message;
//        //        //return result;
//        //    }
//        //    else
//        //    {
//        //        _UserSession_Model = _check._UserSession_Model;

//        //        #region == 處理 ==
//        //        // 取得清單
//        //        var datas = _Employee_Service.UpdateLateRecord(today);
//        //        // [T：成功][F：失敗]
//        //        if (datas.IsSuccess)
//        //        {
//        //            //return new LeaveResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message, datas.Pageing_DTO, datas.Data);
//        //        }
//        //        else
//        //        {
//        //            //return new LeaveResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message);
//        //        }
//        //        #endregion
//        //    }


//        //    //var _YGService = new YGService(dbSun);

//        //    //取得昨天日期
//        //    //var today = DateTime.UtcNow.AddHours(8).AddDays(-1).Date;
//        //    //if (day.HasValue)
//        //    //{
//        //    //    today = day.Value;
//        //    //}

//        //    //#region 1.取得搬家平台提供之目前打卡資訊
//        //    //var _empService = new A_EmployeeService(Token);
//        //    //var data = _empService.AttendanceRecord(today);
//        //    //#endregion

//        //    //#region 2-1.遲到資料寫入ERP
//        //    //var dataLate = data.Where(x => Int64.Parse(x.delayTime) != 0).ToList();
//        //    //將其所屬部門代號，其開頭非為K的工務部門帶入
//        //    //dataLate = dataLate.Where(x => !_YGService.GetNoAndDepAndType(x.idNumber, today).Dep.StartsWith("K")).ToList();

//        //    //var items = dataLate.Select(x => new TF_KQTZ
//        //    //{
//        //    //    YG_NO = _YGService.GetNO(x.idNumber, today),
//        //    //    SZ_NO = "C01",
//        //    //    TRS_DD = today,
//        //    //    SZ_YM = new DateTime(today.Year, today.Month, 1),
//        //    //    UNIT = "4",
//        //    //    QTY = decimal.Parse(x.delayTime),
//        //    //    CALC_ID = "F",
//        //    //    BAN_NO = "01",
//        //    //    ADD_ID = ""
//        //    //}).ToList();

//        //    //if (dataLate != null)
//        //    //{
//        //    //    _KQTZService = new KQTZService(dbSun);
//        //    //    _KQTZService.Update(items);
//        //    //}
//        //    //#endregion

//        //    //#region 2-2. 打卡記錄回寫
//        //    //var source = new List<MF_CLOCK>();
//        //    //foreach (var x in data)
//        //    //{
//        //    //    if (!string.IsNullOrEmpty(x.checkin))
//        //    //    {
//        //    //        var container = x.checkin.Split(':');
//        //    //        var clockHour = Int32.Parse(container[0]);
//        //    //        var clockMin= Int32.Parse(container[1]);
//        //    //        var itemIn = new MF_CLOCK
//        //    //        {
//        //    //            CLOCK_ID = _YGService.GetNO(x.idNumber),
//        //    //            YG_NO = _YGService.GetNO(x.idNumber),
//        //    //            CLOCK_TIME = new DateTime(x.shouldCheckIn.Year,x.shouldCheckIn.Month,x.shouldCheckIn.Day,clockHour,clockMin,0)
//        //    //        };
//        //    //        source.Add(itemIn);

//        //    //    }
//        //    //    if (!string.IsNullOrEmpty(x.checkout))
//        //    //    {
//        //    //        var container = x.checkout.Split(':');
//        //    //        var clockHour = Int32.Parse(container[0]);
//        //    //        var clockMin = Int32.Parse(container[1]);

//        //    //        var itemOut = new MF_CLOCK
//        //    //        {
//        //    //            CLOCK_ID = _YGService.GetNO(x.idNumber),
//        //    //            YG_NO = _YGService.GetNO(x.idNumber),
//        //    //            CLOCK_TIME = new DateTime(x.shouldCheckIn.Year, x.shouldCheckIn.Month, x.shouldCheckIn.Day, clockHour, clockMin, 0)
//        //    //        };
//        //    //        source.Add(itemOut);
//        //    //    }

//        //    //}

//        //    //if (source != null)
//        //    //{
//        //    //    _ClockService = new ClockService(dbSun);
//        //    //    _ClockService.Update(source);
//        //    //}

//        //    //#endregion

//        //}

//        #endregion
//    }
//}
