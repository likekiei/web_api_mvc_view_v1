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
using ERP_APP.Service.S_Product_Inspection;
using ERP_EF.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
//using Main_Common.Model.ResultApi.Order;
using System.Net.Sockets;
using static System.Collections.Specialized.BitVector32;
using Main_EF.Table;
using Swashbuckle.AspNetCore.Annotations;
using Main_Common.Model.DTO.TI_ProductInspection;
using Main_Common.Model.ResultApi.ProductInspection;
using Main_Common.Model.ResultApi.Order;


namespace Web_API.Controllers
{
    /// <summary>
    /// 製成品送檢單
    /// </summary>
    [SwaggerTag("製成品送檢單")]
    [Route("api/[controller]")]
    [ApiController]
    public class TiProductInspectionController : ControllerBase
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
        /// 【ERP】製成品送檢單相關
        /// </summary>
        public readonly Product_Inspection_Serveice_Erp _Product_Inspection_Serveice_Erp;

        /// <summary>
        /// 【ERP】製成品送檢單 自訂義欄位檢查
        /// </summary>
        public readonly MF_TI_Z_Service_Erp _MF_TI_Z_Service_Erp;

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
        public TiProductInspectionController(
            IHttpContextAccessor httpContextAccessor,
            MainSystem_DTO mainSystem_DTO,
            UserService_Main _user_Service,
            LoginService_Main _login_Service_Main,
            Login_Service _login_Service,
            Product_Inspection_Serveice_Erp _product_Inspection_Serveice_Erp,
            MF_TI_Z_Service_Erp _MF_TI_Z_Service_Erp,
            LogService_Main logService_Main,
            Log_Service log_Service
            )
        //MyCheckHelper myCheckHelper)
        //   : base(httpContextAccessor) // base(httpContextAccessor)
        {
            this._MainSystem_DTO = mainSystem_DTO;
            this._Login_Service_Main = _login_Service_Main;
            this._Login_Service = _login_Service;
            this._Product_Inspection_Serveice_Erp = _product_Inspection_Serveice_Erp;
            this._MF_TI_Z_Service_Erp = _MF_TI_Z_Service_Erp;
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
        #region == 送檢單相關 ==     

        /// <summary>
        /// 建立單筆製成品送檢單
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(Logs))]
        public ProductInspectionResult CreateProductInspection(MfTiProductInspection_DTO input)
        {
            UserSession_Model _UserSession_Model = new UserSession_Model();
            var result = new ProductInspectionResult();

            //取token檢查登入狀態
            var token = Request.Headers["Authorization"].ToString();
            var _check = _Login_Service_Main.Check_Login(token);

            if (!_check.IsSuccess)
            {
                #region == Log & return Result ==
                var status = _check.Title;
                var message = _check.Message;
                var slog = new SLog
                {
                    EventLevel = "Error",
                    ActionName = $"{ControllerContext.RouteData.Values["controller"]}/{ControllerContext.RouteData.Values["action"]}",
                    User = _UserSession_Model.Account,
                    Status = status,
                    Message = message,
                    Data = ConvertExtension.EM_ModelToJson<MfTiProductInspection_DTO>(input)
                };
                _Log_Service.Create_Log(slog);

                result.IsSuccess = _check.IsSuccess;
                result.E_StatusCode = _check.E_StatusCode;
                result.Title = _check.Title;
                result.Message = _check.Message;
                return result;
                #endregion
            }
            else
            {
                _UserSession_Model = _check._UserSession_Model;
                //檢查日威寫回送檢單號是否存在
                if (input.bbnum != null)
                {
                    if (_MF_TI_Z_Service_Erp.Check_IsExist_MF_TI_Z(input.bbnum))
                    {
                        #region == Log & return Result ==
                        var status = $"日威送檢單號:{input.bbnum}新增失敗。";
                        var message = "日威送檢單號已存在";
                        var slog = new SLog
                        {
                            EventLevel = "Error",
                            ActionName = $"{ControllerContext.RouteData.Values["controller"]}/{ControllerContext.RouteData.Values["action"]}",
                            User = _UserSession_Model.Account,
                            Status = status,
                            Message = message,
                            Data = ConvertExtension.EM_ModelToJson<MfTiProductInspection_DTO>(input)
                        };
                        _Log_Service.Create_Log(slog);

                        result.IsSuccess = false;
                        result.E_StatusCode = E_StatusCode.存在相同資料;
                        result.Title = status;
                        result.Message = message;
                        return result;
                        #endregion
                    }
                }

                #region == 處理 ==
                // 取得清單
                result = _Product_Inspection_Serveice_Erp.CreateProductInspection(input);
                //return result;
                // [T：成功][F：失敗]
                if (result.IsSuccess)
                {
                    #region == Log  ==
                    var status = result.E_StatusCode.ToString();
                    var message = result.Message;
                    var slog = new SLog
                    {
                        EventLevel = "Error",
                        ActionName = $"{ControllerContext.RouteData.Values["controller"]}/{ControllerContext.RouteData.Values["action"]}",
                        User = _UserSession_Model.Account,
                        Status = status,
                        Message = message,
                        Data = ConvertExtension.EM_ModelToJson<List<MfTiProductInspection_DTO>>(result.Datas)
                    };
                    _Log_Service.Create_Log(slog);
                    #endregion
                    return new ProductInspectionResult(_UserSession_Model.Account, result.IsSuccess, result.E_StatusCode, result.Message, result.Datas);
                }
                else
                {
                    #region == Log  ==
                    var status = result.E_StatusCode.ToString();
                    var message = $"{result.Message},{result.Message_Exception},{result.Message_Other}";
                    //string user = "";
                    //if (_UserSession_Model == null)
                    //    user = "sys";
                    //else  
                    //    user = _UserSession_Model.Account;
                    var slog = new SLog
                    {
                        EventLevel = "Error",
                        ActionName = $"{ControllerContext.RouteData.Values["controller"]}/{ControllerContext.RouteData.Values["action"]}",
                        User = _UserSession_Model.Account,
                        Status = status,
                        Message = message,

                        Data = ConvertExtension.EM_ModelToJson<MfTiProductInspection_DTO>(input)
                    };
                    _Log_Service.Create_Log(slog);
                    #endregion
                    return new ProductInspectionResult(_UserSession_Model.Account, result.IsSuccess, result.E_StatusCode, message);
                }
                #endregion
            }
        }



        #endregion


    }
}