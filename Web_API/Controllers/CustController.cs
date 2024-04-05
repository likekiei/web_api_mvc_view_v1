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
////using Main_Web_APP.Service.S_Company;
////using Main_Web_APP.Service.S_Log;
////using Main_Web_APP.Service.S_Login;
////using WebApi_APP.Service;
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

//namespace Web_API.Controllers
//{
//    /// <summary>
//    /// 客戶相關作業
//    /// </summary>  
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    public class CustController : ControllerBase // _APIController
//    {
//        #region == 【DI注入用宣告】 ==
//        /// <summary>
//        /// 【DTO】主系統資料
//        /// </summary>
//        public readonly MainSystem_DTO _MainSystem_DTO;
//        /// <summary>
//        /// 【API Service】客戶相關
//        /// </summary>
//        public readonly Cust_Service _Cust_Service;
//        ///// <summary>
//        ///// 【Main Service】Log相關
//        ///// </summary>
//        //public readonly LogService_Main _LogService_Main;
//        ///// <summary>
//        ///// 【Main Service】登入相關
//        ///// </summary>
//        public readonly LoginService_Main _Login_Service_Main;
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
//        ///// <param name="logService_Main">Log相關</param>
//        /// <param name="loginService_Main">登入相關</param>
//        ///// <param name="companyService_Main">公司相關</param>
//        ///// <param name="myCheckHelper">通用檢查相關Helper</param>
//        public CustController(
//            IHttpContextAccessor httpContextAccessor,
//            MainSystem_DTO mainSystem_DTO,
//            Cust_Service _cust_Service,
//            UserService_Main _user_Service,
//            //CompanyService_Main _company_Service,
//            //UserSession_Model _userSession_Model,

//            ////LogService_Main logService_Main,
//            LoginService_Main _login_Service_Main
//            //UserSession_Model _userSession_Model
//            )
//        //MyCheckHelper myCheckHelper)
//        //   : base(httpContextAccessor) // base(httpContextAccessor)
//        {
//            this._MainSystem_DTO = mainSystem_DTO;
//            this._Cust_Service = _cust_Service;

//            //this._CompanyService_Main = companyService_Main;
//            //this._LogService_Main = logService_Main;
//            this._Login_Service_Main = _login_Service_Main;
//            //this._UserSession_Model = _userSession_Model;
//            //this._MyCheckHelper = myCheckHelper;            
//        }
//        #endregion

//        //--【方法】=================================================================================

//        #region == 【全域變數】 ==
//        /// <summary>
//        /// 【Main】公司相關
//        /// </summary>
//        //private Company_Service_Main _Company_Service_Main;        
//        /// <summary>
//        /// 【Api】客戶相關
//        /// </summary>
//        //private Cust_Service _Cust_Service;
//        /// <summary>
//        /// 【Main】Log相關
//        /// </summary>
//        //private Log_Service_Main _Log_Service_Main;
//        /// <summary>
//        /// 【SYS】登入相關
//        /// </summary>
//        //private Login_Service_Main _Login_Service_Main;

//        /// <summary>
//        /// 【Common】分頁相關
//        /// </summary>
//        //private Page_Service _Page_Service_Common;
//        /// <summary>
//        /// [Service]錯訊相關
//        /// </summary>
//        //private Message_Service _Message_Service;
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
//        #region == 客戶相關 ==
//        /// <summary>
//        /// 取得客戶資料完整
//        /// </summary>        
//        [HttpPost]
//        [TypeFilter(typeof(Logs))]
//        public CustResult GetCusts(CUST_Filter input)
//        {
//            UserSession_Model _UserSession_Model = new UserSession_Model();
//            var result = new CustResult();

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
//                var datas = _Cust_Service.GetCUSTs(input);
//                // [T：成功][F：失敗]
//                if (datas.IsSuccess)
//                {
//                    return new CustResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message, datas.Pageing_DTO, datas.Data);
//                }
//                else
//                {
//                    return new CustResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message);
//                }
//                #endregion
//            }
//            return result;
//        }

//        /// <summary>
//        /// 取得客戶資料
//        /// </summary>    
//        [HttpPost]
//        public CustResult GetCust(CUST_Filter input)
//        {
//            UserSession_Model _UserSession_Model = new UserSession_Model();
//            var result = new CustResult();

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
//                var datas = _Cust_Service.GetCUST(input);
//                // [T：成功][F：失敗]
//                if (datas.IsSuccess)
//                {
//                    if (datas.Pageing_DTO.TotalCount == 0)
//                    {
//                        datas.Message = "查無資料。";
//                        return new CustResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message);
//                    }

//                    return new CustResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message, datas.Data[0]);
//                }
//                else
//                {
//                    return new CustResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message);
//                }
//                #endregion
//            }
//            return result;
//        }

        
//        /// <summary>
//        /// 建立客戶資料
//        /// </summary>    
//        [HttpPost]
//        public CustResult CreateCust(CUST_DTO input)
//        {
//            UserSession_Model _UserSession_Model = new UserSession_Model();

//            var result = new CustResult();
//            result.IsSuccess = true;

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

//                if (string.IsNullOrEmpty(input.CUS_NO))
//                {
//                    result = new CustResult("", false, E_StatusCode.失敗, "客戶代號不得為空");
//                }
//                if (string.IsNullOrEmpty(input.OBJ_ID))
//                {
//                    result = new CustResult("", false, E_StatusCode.失敗, "對象別不得為空");
//                }
//                if (string.IsNullOrEmpty(input.NAME))
//                {
//                    result = new CustResult("", false, E_StatusCode.失敗, "全稱不得為空");
//                }
//                if (!result.IsSuccess)
//                {
//                    return result;
//                }
//                #region == 處理 ==
//                //// 取得清單
//                //var datas = _Cust_Service.GetCUST(input);
//                //// [T：成功][F：失敗]
//                //if (datas.IsSuccess)
//                //{
//                //    if (datas.Pageing_DTO.TotalCount == 0)
//                //    {
//                //        datas.Message = "查無資料。";
//                //        return new CustResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message);
//                //    }

//                //    return new CustResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message, datas.Data[0]);
//                //}
//                //else
//                //{
//                //    return new CustResult(_UserSession_Model.Account, datas.IsSuccess, datas.E_StatusCode, datas.Message);
//                //}
//                #endregion
//            }
//            return result;
//        }
//        #endregion
//    }
//}
