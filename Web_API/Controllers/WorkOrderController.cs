using Main_Common.Enum;
using Main_Common.Enum.E_StatusType;
using Main_Common.Model.DTO;
using Main_Common.Model.Result;
using Main_Common.Model.ResultApi;
using Main_Common.Model.Search;
using Main_Common.Mothod.Message;
using Main_Common.Mothod.Page;
using Main_Common.Mothod.Validation;
using Web_API.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Main_Common.Model.DTO.Cust;
using Main_Common.Model.Data;
using Main_Common.Model.Main;
using Main_Service.Service.S_Company;
using Main_Service.Service.S_Log;
using Main_Service.Service.S_Login;
using Web_API_APP.Service;
using Main_Common.Model.Account;
using Main_Common.ExtensionMethod;
using Main_Common.GlobalSetting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Jose;
using System.Text;
using Main_Service.Service.S_User;
using Newtonsoft.Json;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Main_Common.Model.DTO.MO_WorkOrder;
using Main_Common.Model.ERP;
using System.Data;
using Main_Common.Model.ERP.DTO;
//using ERP_APP.Service.S_MF_POS;
//using ERP_APP.Service.S_CUST;
//using ERP_APP.Service.S_MY_WH;
//using ERP_APP.Service.S_DEPT;
//using ERP_APP.Service.S_MF_YG;
//using ERP_APP.Service.S_BIL_SPC;
using ERP_APP.Service.S_WORD_ORDER;
using ERP_EF.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Main_Common.Model.ResultApi.Order;
using System.Net.Sockets;
using static System.Collections.Specialized.BitVector32;
using Main_EF.Table;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_API.Controllers
{
    /// <summary>
    /// 製令單
    /// </summary>
    [SwaggerTag("製令單")]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrderController : ControllerBase
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
        /// 【API Service】Log相關
        /// </summary>
        public readonly Log_Service _Log_Service;
        ///// <summary>
        ///// 【Main Service】登入相關
        ///// </summary>
        public readonly LoginService_Main _Login_Service_Main;
        ///// <summary>
        ///// 【Main Service】登入相關
        ///// </summary>
        public readonly Login_Service _Login_Service;

        /// <summary>
        /// 【ERP】製令單相關
        /// </summary>
        public readonly Word_Order_Service_Erp _Word_Order_Service_Erp;

        #endregion

        #region == 【建構】 ==
        /// <summary>
        /// 建構
        /// </summary>
        ///// <param name="httpContextAccessor">HttpContext</param>
        ///// <param name="mainSystem_DTO">主系統資料</param>
        /// <param name="logService_Main">Log相關</param>
        /// <param name="loginService_Main">登入相關</param>
        ///// <param name="companyService_Main">公司相關</param>
        ///// <param name="myCheckHelper">通用檢查相關Helper</param>
        public WorkOrderController(
            IHttpContextAccessor httpContextAccessor,
            MainSystem_DTO mainSystem_DTO,
            UserService_Main _user_Service,
            LoginService_Main _login_Service_Main,
            Login_Service _login_Service,
            Word_Order_Service_Erp _Word_Order_Service_Erp,
            LogService_Main logService_Main,
            Log_Service log_Service
            )
        //MyCheckHelper myCheckHelper)
        //   : base(httpContextAccessor) // base(httpContextAccessor)
        {
            this._MainSystem_DTO = mainSystem_DTO;
            this._Login_Service_Main = _login_Service_Main;
            this._Login_Service = _login_Service;
            this._Word_Order_Service_Erp = _Word_Order_Service_Erp;
            this._LogService_Main = logService_Main;
            this._Log_Service = log_Service;
        }
        #endregion


        #region == 【全域變數】參數、屬性 ==
        private Guid? Com_Bind_Key = null; //共用綁定Key(使用前請先重置)
        private string Com_MainKey = null; //共用主要Key(使用前請先重置)
        private string LogErrorMsg = null; //共用Log錯誤訊息(使用前請先重置)
        private string ResultMsg_Finally = null; //共用最終回傳訊息(使用前請先重置)
        private bool Com_Result = false; //共用結果(使用前請先重置)
        private Message_Model Com_Message_DTO = null; //共用訊息結果(使用前請先重置)
        private ResultOutput Com_Result_DTO = null; //共用結果(使用前請先重置)
        private ResultOutput Com_Result_DTO_Log = null; //共用結果(使用前請先重置)
        private List<string> Com_TextList = null; //共用文字清單(使用前請先重置)
        private List<string> Com_Split = null; //共用Split結果(使用前請先重置)
        private List<SelectItemDTO> DropList_DTO = null; //共用下拉清單(使用前請先重置)
        #endregion






        //--【方法】=================================================================================      
        #region == 製令單相關 ==     

        /// <summary>
        /// 依日期取得製令單資料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(Logs))]
        public OrderResult GetWorkOrders(MoWorkOrder_Filter input)
        {
            UserSession_Model _UserSession_Model = new UserSession_Model();
            var result = new OrderResult();

            //取token檢查登入狀態
            var token = Request.Headers["Authorization"].ToString();
            var _check = _Login_Service_Main.Check_Login(token);

            if (!_check.IsSuccess)
            {
                result.IsSuccess = _check.IsSuccess;
                result.E_StatusCode = _check.E_StatusCode;
                result.Title = _check.Title;
                result.Message = _check.Message;
                return result;
            }
            else
            {
                _UserSession_Model = _check._UserSession_Model;

                #region == 處理 ==
                // 取得清單
                result = _Word_Order_Service_Erp.GetWorkOrders(input);
                //return result;
                // [T：成功][F：失敗]
                if (result.IsSuccess)
                {
                    return new OrderResult(_UserSession_Model.Account, result.IsSuccess, result.E_StatusCode, result.Message, result.Datas);
                }
                else
                {
                    return new OrderResult(_UserSession_Model.Account, result.IsSuccess, result.E_StatusCode, result.Message);
                }
                #endregion
            }

        }



        /// <summary>
        /// 依製令單號取得製令單資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}




        #endregion
    }
}
