using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Main_Common.Model.Result;
using Main_Common.Model.ResultApi;
using Web_API.Filters;
using Main_Common.Model.Data;
using Main_Common.Model.Main;
using Main_Service.Service.S_Log;
using Main_Service.Service.S_Login;
using Web_API_APP.Service;
using Main_Common.Model.Account;
using Main_Service.Service.S_User;
using Main_Common.Model.DTO.MO_WorkOrder;
using ERP_APP.Service.S_MF_TY;
using ERP_APP.Service.S_Product_Inspection;
using ERP_APP.Service.S_WORD_ORDER;
using Main_Common.Model.DTO.TI_ProductInspection;
using Main_Common.Model.ResultApi.ProductInspection;
using Main_Common.Model.DTO.TY_ProductAcceptance;
using Main_Common.Model.ResultApi.ProductAcceptance;
using Main_Common.Enum.E_StatusType;
using Main_Common.ExtensionMethod;


namespace Web_API.Controllers
{

    /// <summary>
    /// 生產繳庫驗收單
    /// </summary>
    [SwaggerTag("生產繳庫驗收單")]
    [Route("api/[controller]")]
    [ApiController]
    public class TyProductAcceptanceController : Controller
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
        /// 【ERP】生產繳庫驗收單相關
        /// </summary>
        public readonly MF_TY_Service_Erp _MF_TY_Service_Erp;

        public readonly MF_TY_Z_Service_Erp _MF_TY_Z_Service_Erp;


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
        public TyProductAcceptanceController(
            IHttpContextAccessor httpContextAccessor,
            MainSystem_DTO mainSystem_DTO,
            UserService_Main _user_Service,
            LoginService_Main _login_Service_Main,
            Login_Service _login_Service,
            MF_TY_Service_Erp _MF_TY_Service_Erp,
            MF_TY_Z_Service_Erp _MF_TY_Z_Service_Erp,
            LogService_Main logService_Main,
            Log_Service log_Service
            )
        //MyCheckHelper myCheckHelper)
        //   : base(httpContextAccessor) // base(httpContextAccessor)
        {
            this._MainSystem_DTO = mainSystem_DTO;
            this._Login_Service_Main = _login_Service_Main;
            this._Login_Service = _login_Service;
            this._MF_TY_Service_Erp = _MF_TY_Service_Erp;
            this._MF_TY_Z_Service_Erp = _MF_TY_Z_Service_Erp;
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
        #region == 生產繳庫驗收單相關 ==  

        /// <summary>
        /// 建立單筆繳庫驗收單
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(Logs))]
        public ProductAcceptanceResult CreateProductAcceptance(MfTyProductAcceptance_DTO input)
        {
            UserSession_Model _UserSession_Model = new UserSession_Model();
            var result = new ProductAcceptanceResult();

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
                    Data = ConvertExtension.EM_ModelToJson<MfTyProductAcceptance_DTO>(input)
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
                //檢查日威寫回生產繳庫驗收單號是否存在
                if (input.bbnum != null)
                {
                    if (_MF_TY_Z_Service_Erp.Check_IsExist_MF_TI_Z(input.bbnum))
                    {
                        #region == Log & return Result ==
                        var status = $"日威送生產繳庫驗收單號:{input.bbnum}新增失敗。";
                        var message = "日威送生產繳庫驗收單號已存在";
                        var slog = new SLog
                        {
                            EventLevel = "Error",
                            ActionName = $"{ControllerContext.RouteData.Values["controller"]}/{ControllerContext.RouteData.Values["action"]}",
                            User = _UserSession_Model.Account,
                            Status = status,
                            Message = message,
                            Data = ConvertExtension.EM_ModelToJson<MfTyProductAcceptance_DTO>(input)
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
                result = _MF_TY_Service_Erp.CreateProductAcceptance(input);
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
                        Data = ConvertExtension.EM_ModelToJson<List<MfTyProductAcceptance_DTO>>(result.Datas)
                    };
                    _Log_Service.Create_Log(slog);
                    #endregion
                    return new ProductAcceptanceResult(_UserSession_Model.Account, result.IsSuccess, result.E_StatusCode, result.Message, result.Datas);
                }
                else
                {
                    #region == Log  ==
                    var status = result.E_StatusCode.ToString();
                    var message = $"{result.Message},{result.Message_Exception},{result.Message_Other}";             
                    var slog = new SLog
                    {
                        EventLevel = "Error",
                        ActionName = $"{ControllerContext.RouteData.Values["controller"]}/{ControllerContext.RouteData.Values["action"]}",
                        User = _UserSession_Model.Account,
                        Status = status,
                        Message = message,

                        Data = ConvertExtension.EM_ModelToJson<MfTyProductAcceptance_DTO>(input)
                    };
                    _Log_Service.Create_Log(slog);
                    #endregion
                    return new ProductAcceptanceResult(_UserSession_Model.Account, result.IsSuccess, result.E_StatusCode, message);
                }
                #endregion
            }


        }



            #endregion

        }
}
