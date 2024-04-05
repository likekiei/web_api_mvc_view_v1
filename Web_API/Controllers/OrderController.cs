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
//using Main_Common.Model.DTO.Order;
//using Main_Common.Model.ERP;
//using System.Data;
//using Main_Common.Model.ERP.DTO;
////using ERP_APP.Service.S_MF_POS;
////using ERP_APP.Service.S_CUST;
////using ERP_APP.Service.S_MY_WH;
////using ERP_APP.Service.S_DEPT;
////using ERP_APP.Service.S_MF_YG;
////using ERP_APP.Service.S_BIL_SPC;
//using ERP_EF.Models;

//using Microsoft.AspNetCore.Cors.Infrastructure;

//using Main_Common.Model.ResultApi.Order;
//using ERP_APP.Service.S_Order;
//using ERP_APP.Service.S_INV_NO;
//using System.Net.Sockets;
//using static System.Collections.Specialized.BitVector32;
//using Main_EF.Table;

//namespace Web_API.Controllers
//{
//    /// <summary>
//    /// 訂單相關作業
//    /// </summary>  
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    public class OrderController : ControllerBase // _APIController
//    {
//        #region == 【DI注入用宣告】 ==
//        /// <summary>
//        /// 【DTO】主系統資料
//        /// </summary>
//        public readonly MainSystem_DTO _MainSystem_DTO;
//        /// <summary>
//        /// 【API Service】訂單相關
//        /// </summary>
//        public readonly Order_Service _Order_Service;
//        /// <summary>
//        /// 【Main Service】Log相關
//        /// </summary>
//        public readonly LogService_Main _LogService_Main;
//        /// <summary>
//        /// 【API Service】Log相關
//        /// </summary>
//        public readonly Log_Service _Log_Service;
//        ///// <summary>
//        ///// 【Main Service】登入相關
//        ///// </summary>
//        public readonly LoginService_Main _Login_Service_Main;
//        ///// <summary>
//        ///// 【Main Service】登入相關
//        ///// </summary>
//        public readonly Login_Service _Login_Service;
//        ///// <summary>
//        ///// 【ERP】客戶相關
//        ///// </summary>
//        //public readonly CUST_Service_Erp _CUST_Service_Erp;
//        ///// <summary>
//        ///// 【ERP】部門相關
//        ///// </summary>
//        //public readonly DEPT_Service_Erp _DEPT_Service_Erp;
//        ///// <summary>
//        ///// 【ERP】受訂單相關
//        ///// </summary>
//        //public readonly MF_POS_Service_Erp _MF_POS_Service_Erp;
//        ///// <summary>
//        ///// 【ERP】人事薪資相關
//        ///// </summary>
//        //public readonly MF_YG_Service_Erp _MF_YG_Service_Erp;
//        /// <summary>
//        /// 【ERP】倉庫相關
//        /// </summary>
//        //public readonly MY_WH_Service_Erp _MY_WH_Service_Erp;
//        /// <summary>
//        /// 【ERP】倉庫相關
//        /// </summary>
//        //public readonly BIL_SPC_Service_Erp _BIL_SPC_Service_Erp;
//        /// <summary>
//        /// 【ERP】銷貨單相關
//        /// </summary>
//        public readonly Order_Service_Erp _Order_Service_Erp;
//        /// <summary>
//        /// 【ERP】銷貨單相關
//        /// </summary>
//        public readonly INV_NO_Service_Erp _INV_NO_Service_Erp;

//        /// <summary>
//        /// 【Helper】通用檢查相關Helper
//        /// </summary>
//        //public readonly MyCheckHelper _MyCheckHelper;
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
//        public OrderController(
//            IHttpContextAccessor httpContextAccessor,
//            MainSystem_DTO mainSystem_DTO,
//            Order_Service _Order_Service,
//            UserService_Main _user_Service,
//            //CompanyService_Main _company_Service,
//            //UserSession_Model _userSession_Model,
//            //CUST_Service_Erp _CUST_Service_Erp,
//            //DEPT_Service_Erp _DEPT_Service_Erp,
//            LoginService_Main _login_Service_Main,
//            Login_Service _login_Service,
//            //MF_POS_Service_Erp _MF_POS_Service_Erp,
//            //MF_YG_Service_Erp _MF_YG_Service_Erp,
//            //MY_WH_Service_Erp _MY_WH_Service_Erp,
//            //BIL_SPC_Service_Erp _BIL_SPC_Service_Erp,
//            Order_Service_Erp _Order_Service_Erp,
//            INV_NO_Service_Erp _INV_NO_Service_Erp,
//            //UserSession_Model _userSession_Model
//            LogService_Main logService_Main,
//            Log_Service log_Service
//            )
//        //MyCheckHelper myCheckHelper)
//        //   : base(httpContextAccessor) // base(httpContextAccessor)
//        {
//            this._MainSystem_DTO = mainSystem_DTO;
//            this._Order_Service = _Order_Service;
//            this._Login_Service_Main = _login_Service_Main;
//            this._Login_Service = _login_Service;
//            //this._CUST_Service_Erp = _CUST_Service_Erp;
//            //this._DEPT_Service_Erp = _DEPT_Service_Erp;
//            //this._MF_POS_Service_Erp = _MF_POS_Service_Erp;
//            //this._MF_YG_Service_Erp = _MF_YG_Service_Erp;
//            //this._MY_WH_Service_Erp = _MY_WH_Service_Erp;
//            //this._BIL_SPC_Service_Erp = _BIL_SPC_Service_Erp;
//            this._Order_Service_Erp = _Order_Service_Erp;
//            this._INV_NO_Service_Erp = _INV_NO_Service_Erp;
//            //this._UserSession_Model = _userSession_Model;
//            //this._MyCheckHelper = myCheckHelper;
//            this._LogService_Main = logService_Main;
//            this._Log_Service = log_Service;
//        }
//        #endregion

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
//        #region == 訂單相關 ==     
//        /// <summary>
//        /// 取得全部訂單基本資料
//        /// </summary>
//        /// <param name="input"></param>
//        /// <returns></returns>
//        [HttpPost]
//        [TypeFilter(typeof(Logs))]
//        public OrderResult GetSalesOrders(Order_Filter input)
//        {
//            UserSession_Model _UserSession_Model = new UserSession_Model();
//            var result = new OrderResult();

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
//                result = _Order_Service_Erp.GetSalesOrders(input);
//                //return result;
//                // [T：成功][F：失敗]
//                if (result.IsSuccess)
//                {
//                    return new OrderResult(_UserSession_Model.Account, result.IsSuccess, result.E_StatusCode, result.Message, result.PageDTO, result.Datas);
//                }
//                else
//                {
//                    return new OrderResult(_UserSession_Model.Account, result.IsSuccess, result.E_StatusCode, result.Message);
//                }
//                #endregion
//            }
//        }

//        /// <summary>
//        /// 建立單筆銷貨單
//        /// </summary>
//        /// <param name="input"></param>
//        /// <returns></returns>
//        [HttpPost]
//        [TypeFilter(typeof(Logs))]
//        public OrderResult CreateSalesOrder(SalesOrderDTO input)
//        {
//            UserSession_Model _UserSession_Model = new UserSession_Model();
//            var result = new OrderResult();

//            //取token檢查登入狀態
//            var token = Request.Headers["Authorization"].ToString();
//            var _check = _Login_Service_Main.Check_Login(token);

//            if (!_check.IsSuccess)
//            {
//                #region == Log & return Result ==
//                var status = _check.Title;
//                var message = _check.Message;
//                var slog = new SLog
//                {
//                    EventLevel = "Error",
//                    ActionName = $"{ControllerContext.RouteData.Values["controller"]}/{ControllerContext.RouteData.Values["action"]}",
//                    User = _UserSession_Model.Account,
//                    Status = status,
//                    Message = message,
//                    Data = ConvertExtension.EM_ModelToJson<SalesOrderDTO>(input)
//                };
//                _Log_Service.Create_Log(slog);

//                result.IsSuccess = _check.IsSuccess;
//                result.E_StatusCode = _check.E_StatusCode;
//                result.Title = _check.Title;
//                result.Message = _check.Message;
//                return result;
//                #endregion
//            }
//            else
//            {
//                _UserSession_Model = _check._UserSession_Model;
//                //檢查發票是否存在
//                if (input.invoice_number != null)
//                {
//                    if (_INV_NO_Service_Erp.Check_IsExist_INV_NO(input.invoice_number))
//                    {
//                        #region == Log & return Result ==
//                        var status = $"發票單號:{input.invoice_number}新增失敗。";
//                        var message = "發票單號已存在";
//                        var slog = new SLog
//                        {
//                            EventLevel = "Error",
//                            ActionName = $"{ControllerContext.RouteData.Values["controller"]}/{ControllerContext.RouteData.Values["action"]}",
//                            User = _UserSession_Model.Account,
//                            Status = status,
//                            Message = message,
//                            Data = ConvertExtension.EM_ModelToJson<SalesOrderDTO>(input)
//                        };
//                        _Log_Service.Create_Log(slog);

//                        result.IsSuccess = false;
//                        result.E_StatusCode = E_StatusCode.存在相同資料;
//                        result.Title = status;
//                        result.Message = message;
//                        return result;
//                        #endregion
//                    }
//                }

//                #region == 處理 ==
//                // 取得清單
//                result = _Order_Service_Erp.CreateSalesOrder(input);
//                //return result;
//                // [T：成功][F：失敗]
//                if (result.IsSuccess)
//                {
//                    #region == Log  ==
//                    var status = result.E_StatusCode.ToString();
//                    var message = result.Message;
//                    var slog = new SLog
//                    {
//                        EventLevel = "Error",
//                        ActionName = $"{ControllerContext.RouteData.Values["controller"]}/{ControllerContext.RouteData.Values["action"]}",
//                        User = _UserSession_Model.Account,
//                        Status = status,
//                        Message = message,
//                        Data = ConvertExtension.EM_ModelToJson<List<OrderMFDTO>>(result.Datas)
//                    };
//                    _Log_Service.Create_Log(slog);
//                    #endregion
//                    return new OrderResult(_UserSession_Model.Account, result.IsSuccess, result.E_StatusCode, result.Message, result.PageDTO, result.Datas);
//                }
//                else
//                {
//                    #region == Log  ==
//                    var status = result.E_StatusCode.ToString();
//                    var message = $"{result.Message},{result.Message_Exception},{result.Message_Other}";
//                    //string user = "";
//                    //if (_UserSession_Model == null)
//                    //    user = "sys";
//                    //else
//                    //    user = _UserSession_Model.Account;
//                    var slog = new SLog
//                    {
//                        EventLevel = "Error",
//                        ActionName = $"{ControllerContext.RouteData.Values["controller"]}/{ControllerContext.RouteData.Values["action"]}",
//                        User = _UserSession_Model.Account,
//                        Status = status,
//                        Message = message,

//                        Data = ConvertExtension.EM_ModelToJson<SalesOrderDTO>(input)
//                    };
//                    _Log_Service.Create_Log(slog);
//                    #endregion
//                    return new OrderResult(_UserSession_Model.Account, result.IsSuccess, result.E_StatusCode, message);
//                }
//                #endregion
//            }
//        }

//        /// <summary>
//        /// 建立多筆銷貨單
//        /// </summary>
//        /// <param name="input"></param>
//        /// <returns></returns>
//        [HttpPost]
//        //[TypeFilter(typeof(Logs))]
//        public List<OrderResult> CreateSalesOrders(List<SalesOrderDTO> inputs)
//        {
//            UserSession_Model _UserSession_Model = new UserSession_Model();
//            var results = new List<OrderResult>();
//            var result = new OrderResult();

//            //取token檢查登入狀態
//            var token = Request.Headers["Authorization"].ToString();
//            var _check = _Login_Service_Main.Check_Login(token);

//            if (!_check.IsSuccess)
//            {
//                #region == Log & return Result ==
//                var status = _check.Title;
//                var message = _check.Message;
//                var slog = new SLog
//                {
//                    EventLevel = "Error",
//                    ActionName = $"{ControllerContext.RouteData.Values["controller"]}/{ControllerContext.RouteData.Values["action"]}",
//                    User = _UserSession_Model.Account,
//                    Status = status,
//                    Message = message,
//                    Data = ConvertExtension.EM_ModelToJson<List<SalesOrderDTO>>(inputs)
//                };
//                _Log_Service.Create_Log(slog);

//                result.IsSuccess = _check.IsSuccess;
//                result.E_StatusCode = _check.E_StatusCode;
//                result.Title = _check.Title;
//                result.Message = _check.Message;
//                results.Add(result);
//                return results;
//                #endregion
//            }
//            else
//            {
//                _UserSession_Model = _check._UserSession_Model;

//                #region == 處理 ==
//                // 取得清單
//                int i = 1;
//                foreach (var input in inputs)
//                {
//                    if (input.invoice_number != null)
//                    {
//                        if (_INV_NO_Service_Erp.Check_IsExist_INV_NO(input.invoice_number))
//                        {
//                            #region == Log & return Result ==
//                            var status = $"發票單號:{input.invoice_number}新增失敗。";
//                            var message = "發票單號已存在";
//                            var slog = new SLog
//                            {
//                                ID = i,
//                                EventLevel = "Error",
//                                ActionName = $"{ControllerContext.RouteData.Values["controller"]}/{ControllerContext.RouteData.Values["action"]}",
//                                User = _UserSession_Model.Account,
//                                Status = status,
//                                Message = message,
//                                Data = ConvertExtension.EM_ModelToJson<SalesOrderDTO>(input)
//                            };
//                            _Log_Service.Create_Log(slog);

//                            result.item = i;
//                            result.IsSuccess = false;
//                            result.E_StatusCode = E_StatusCode.存在相同資料;
//                            result.Title = status;
//                            result.Message = message;
//                            results.Add(result);
//                            #endregion
//                        }
//                        else
//                        {
//                            result = _Order_Service_Erp.CreateSalesOrder(input);
//                            result.item = i;
//                            results.Add(result);
//                        }
//                    }
//                    else
//                    {
//                        result = _Order_Service_Erp.CreateSalesOrder(input);
//                        result.item = i;
//                        results.Add(result);
//                    }
//                    i++;
//                }


//                foreach (var res in results)
//                {
//                    #region == Log  ==
//                    string st = "Info";
//                    var status = res.E_StatusCode.ToString();
//                    var message = res.Message;
//                    if (!res.IsSuccess)
//                    {
//                        st = "Error";
//                    }
//                    var slog = new SLog
//                    {
//                        ID = res.item,
//                        EventLevel = st,
//                        ActionName = $"{ControllerContext.RouteData.Values["controller"]}/{ControllerContext.RouteData.Values["action"]}",
//                        User = _UserSession_Model.Account,
//                        Status = status,
//                        Message = message,
//                        //Data = ConvertExtension.EM_ModelToJson<OrderMFDTO>(res.Datas)
//                    };
//                    _Log_Service.Create_Log(slog);
//                    #endregion
//                }

//                return results;
//                // [T：成功][F：失敗]
//                //return results;
//                //if (result.IsSuccess)
//                //{
//                //    results.Add(new OrderResult(_UserSession_Model.Account, result.IsSuccess, result.E_StatusCode, result.Message, result.PageDTO, result.Datas));
//                //    return new OrderResult(_UserSession_Model.Account, result.IsSuccess, result.E_StatusCode, result.Message, result.PageDTO, result.Datas);
//                //}
//                //else
//                //{
//                //    return new OrderResult(_UserSession_Model.Account, result.IsSuccess, result.E_StatusCode, result.Message);
//                //}
//                #endregion
//            }
//        }

//        #endregion
//    }

//}
