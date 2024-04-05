//using Main_Common.Enum;
//using ERP_EF;
//using Main_Common.Model.Account;
//using Main_Common.Model.DTO;
//using Main_Common.Model.Result;
//using Main_Common.Mothod.Message;
////using Main_Web_EF.Repositories;
////using Main_Web_EF.Table;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Main_Common.Model.ResultApi;
//using Main_Common.Mothod.Validation;
//using Main_Common.ExtensionMethod;
//using Main_Common.Model.DTO.Cust;
//using Main_Common.Model.Search;
//using ERP_APP.Service.S_CUST;
//using Main_Common.Model.Data;
//using Main_Common.Model.Main;
//using Main_Common.Tool;
//using Main_EF.Interface;
//using Main_Service.Service.S_Log;
//using Main_EF.Table;
//using Main_Common.Enum.E_StatusType;
//using ERP_EF.Models;
//using Main_Common.Model.Tool;

//namespace Web_API_APP.Service
//{
//    /// <summary>
//    /// 【Api】出入庫相關 (所有資料默認都要視情況過濾公司，例外狀況請特別處理)
//    /// </summary>
//    public class Cust_Service
//    {
//        #region == 【DI注入用宣告】 ==
//        /// <summary>
//        /// 資料庫工作單元
//        /// </summary>
//        //public readonly IUnitOfWork _unitOfWork;
//        /// <summary>
//        /// 【DTO】主系統資料
//        /// </summary>
//        public readonly MainSystem_DTO _MainSystem_DTO;
//        /// <summary>
//        /// 【Main Service】Log相關
//        /// </summary>
//        //public readonly LogService_Main _LogService_Main;
//        /// <summary>
//        /// 訊息處理
//        /// </summary>
//        public readonly CUST_Service_Erp CUST_Service_ERP;
//        /// <summary>
//        /// 訊息處理
//        /// </summary>
//        //public readonly Message_Tool _Message_Tool;
//        /// <summary>
//        /// ERPDB
//        /// </summary>
//        private readonly DB_T020Context _DB_T020Context;
//        #endregion

//        #region == 【全域宣告】 ==
//        // ...
//        #endregion

//        //--【建構】=================================================================================

//        #region == 建構 ==
//        /// <summary>
//        /// 建構
//        /// </summary>
//        ///// <param name="unitOfWork">資料庫工作單元</param>
//        /// <param name="mainSystem_DTO">主系統資料</param>
//        ///// <param name="logService_Main">Log相關</param>
//        ///// <param name="messageTool">訊息處理</param>
//        public Cust_Service(
//        //    IUnitOfWork unitOfWork,            
//            MainSystem_DTO mainSystem_DTO,
//        //    LogService_Main logService_Main,
//            CUST_Service_Erp _cust_Service_ERP,
//            DB_T020Context _DB_T020Context
//        //    //Message_Service _Message_Service,
//        //    Message_Tool messageTool
//            ) 
//        {
//        //    this._unitOfWork = unitOfWork;            
//            this._MainSystem_DTO = mainSystem_DTO;
//        //    this._LogService_Main = logService_Main;
//            this.CUST_Service_ERP = _cust_Service_ERP;
//            this._DB_T020Context = _DB_T020Context;
//        //    //this._Message_Service = Message_Service;

//        //    this._Message_Tool = messageTool;
//        }
//        #endregion

//        //--【方法】=================================================================================


//        #region == 【全域變數】DB、Service ==
//        /// <summary>
//        /// [Table]公司
//        /// </summary>
//        //private C_Main_Repository<Companys> _Companys_Repository;

//        /// <summary>
//        /// [Service]錯訊相關
//        /// </summary>
//        //private Message_Service _Message_Service;
//        /// <summary>
//        /// [Service]錯訊相關
//        /// </summary>
//        //private CUST_Service_Erp _CUST_Service;
//        #endregion

//        #region == 【全域變數】參數、屬性 ==
//        private UserSession_Model _UserSessionModel = null; //登入者資訊
//        private string Com_MainKey = null; //共用主要Key(使用前請先重置)
//        private string LogErrorMsg = null; //共用Log錯誤訊息(使用前請先重置)
//        private string ResultMsg_Finally = null; //共用最終回傳訊息(使用前請先重置)
//        private bool Com_Check = false; //共用檢查結果(使用前請先重置)
//        private bool Com_IsExist = false; //共用是否存在(使用前請先重置)
//        private bool Com_Result = false; //共用處理結果(使用前請先重置)
//        private ResultOutput Com_Result_DTO = null; //共用結果(使用前請先重置)
//        private List<string> Com_TextList = null; //共用文字清單(使用前請先重置)
//        private List<string> Com_Split = null; //共用Split結果(使用前請先重置)
//        private List<SelectItemDTO> DropList_DTO = null; //共用下拉清單(使用前請先重置)

//        //private List<Cust_DTO> _CUST_DTOs = new List<Cust_DTO>();
//        //private List<Product_DTO> _Product_DTOs = new List<Product_DTO>();
//        //private List<Warehouse_Area_DTO> _Warehouse_Area_DTOs = new List<Warehouse_Area_DTO>();
//        //private List<InBoundOrder_TF_DTO> _InBoundOrder_TF_DTOs = new List<InBoundOrder_TF_DTO>();
//        //private List<OutBoundOrder_TF_DTO> _OutBoundOrder_TF_DTOs = new List<OutBoundOrder_TF_DTO>();
//        #endregion

//        ////--【建構】=================================================================================

//        #region == 建構 ==
//        /// <summary>
//        /// 【建構】同源EntityContext
//        /// </summary>
//        /// <param name="input"></param>
//        //public Cust_Service(UserSession_Model input)
//        //{
//        //    //var db = new C_Main_DB();
//        //    //var db = new ERP_DB();
//        //    //_Companys_Repository = new C_Main_Repository<Companys>(db);
//        //    //_CUSTRepository = new C_ERP_Repository<CUST>(db);

//        //    _UserSessionModel = input; //保存
//        //}
//        #endregion

//        ////--【方法】=================================================================================

//        #region == 取得客戶資料 ==
//        /// <summary>
//        /// 取得客戶資料
//        /// </summary>
//        /// <returns></returns>
//        public ResultOutput_Data <List<CUST_DTO>> GetCUSTs(CUST_Filter input)
//        {
//            var today = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString()); // 當前時間(不含毫秒)
//            var result = new ResultOutput_Data<List<CUST_DTO>>(true, new List<CUST_DTO>());
//            //_CUST_Service = new CUST_Service_Erp(_UserSession_Model);                      
            


//            #region == 整理資料 ==  
//            result = CUST_Service_ERP.Get_CUST_Datas();
//            //var dbData = db.ConvertModelList<CUST, CUST_DTO>();

//            #endregion

//            #region == 檢查-權限 ==
//            // 檢查
//            //Com_Result_DTO = Validation_Tool.Is_AllowAction_ByCompanyLevelCheck(_UserSessionModel, 1);

//            //// [T：失敗]
//            //if (!Com_Result_DTO.IsSuccess)
//            //{
//            //    return new ResultOutput_Data<List<CUST_DTO>>(false, E_StatusCode.失敗, Com_Result_DTO.Message, null);
//            //}
//            #endregion                      

//            #region == 過濾 ==
//            if (!string.IsNullOrEmpty(input.No))
//            {
//                result.Data = result.Data.Where(x => x.CUS_NO == input.No).ToList();
//            }
//            // Name
//            if (!string.IsNullOrEmpty(input.Name_Keyword))
//            {
//                result.Data = result.Data.Where(x => x.NAME.Contains(input.Name_Keyword)).ToList();
//            }
//            #endregion

//            #region == 查詢值 ==
//            var pageDTO = new Pageing_DTO
//            {
//                IsEnable = input.PageNumber != 0 ? true : false,
//                PageNumber = input.PageNumber ?? 1,
//                PageSize = input.PageSize ?? 10,
//            };
//            #endregion

//            #region == 分頁處理 ==
//            //// 是否分頁。 [T：不分頁][F：分頁]
//            if (pageDTO == null || pageDTO.IsEnable == false)
//            {
//                result.Pageing_DTO.TotalCount = result.Data.Count();
//            }
//            else
//            {
//                pageDTO.TotalCount = result.Data.Count();
//                result.Pageing_DTO = pageDTO; //給result值
//                result.Data = result.Data.Skip((pageDTO.PageNumber - 1) * pageDTO.PageSize).Take(pageDTO.PageSize).ToList();
//            }
//            #endregion
//            result.E_StatusCode = E_StatusCode.成功;
//            //result.Title = "客戶資料";
//            result.Message = "取得完整客戶資料";
//            //result.Data = dbData;
//            return result;
//        }

//        /// <summary>
//        /// 取得客戶資料
//        /// </summary>
//        /// <returns></returns>
//        public ResultOutput_Data<List<CUST_DTO>> GetCUST(CUST_Filter input)
//        {
//            var today = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString()); // 當前時間(不含毫秒)
//            var result = new ResultOutput_Data<List<CUST_DTO>>(true, new List<CUST_DTO>());
//            //_CUST_Service = new CUST_Service_Erp(_UserSessionModel);

//            #region == 前置檢查 ==
//            //[T：無值][]
//            if (input == null)
//            {
//                //result.Message_Exception = new MessageInfo(false, $"");
//                result.E_StatusCode = E_StatusCode.檢查異常;
//                result.Message = $"【檢查異常】傳入參數存在null值";

//                result.IsSuccess = false;
//                //result.Message_Exception = exceptionDTO;
//                //result.Data = null;
//                return result;
//            }
//            #endregion


//            #region == 整理資料 ==  
//            result = CUST_Service_ERP.Get_CUST_Datas();
//            //var dbData = db.ConvertModelList<CUST, CUST_DTO>();
//            //var Data = new Employee_DTO();
//            //result.Data = dbData.to;
//            #endregion

//            #region == 檢查-權限 ==
//            //// 檢查
//            //Com_Result_DTO = Validation_Tool.Is_AllowAction_ByCompanyLevelCheck(_UserSessionModel, 1);

//            //// [T：失敗]
//            //if (!Com_Result_DTO.IsSuccess)
//            //{
//            //    return new ResultOutput_Data<List<CUST_DTO>>(false, E_StatusCode.失敗, Com_Result_DTO.Message, null);
//            //}
//            #endregion

//            #region == 過濾 ==           

//            // No
//            if (!string.IsNullOrEmpty(input.No))
//            {
//                result.Data = result.Data.Where(x => x.CUS_NO == input.No).ToList();
//            }
//            // Name
//            if (!string.IsNullOrEmpty(input.Name_Keyword))
//            {
//                result.Data = result.Data.Where(x => x.NAME == input.Name_Keyword).ToList();
//            }          
//            #endregion

//            #region == 查詢值 ==
//            //input.PageNumber
//            var pageDTO = new Pageing_DTO
//            {
//                IsEnable = input.PageNumber.HasValue ? true : false,
//                PageNumber = input.PageNumber ?? 1,
//                PageSize = input.PageSize ?? 10,
//            };
//            #endregion

//            #region == 分頁處理 ==
//            //// 是否分頁。 [T：不分頁][F：分頁]
//            if (pageDTO == null || pageDTO.IsEnable == false)
//            {
//                result.Pageing_DTO.TotalCount = result.Data.Count();
//            }
//            else
//            {
//                pageDTO.TotalCount = result.Data.Count();
//                result.Pageing_DTO = pageDTO; //給result值
//                //dbData = dbData.OrderByDescending(o => o.Doc_Date).ThenByDescending(o => o.No).Skip((pageDTO.PageNumber - 1) * pageDTO.PageSize).Take(pageDTO.PageSize);
//            }
//            #endregion
//            result.E_StatusCode = E_StatusCode.成功;
//            //result.Title = "客戶資料";
//            result.Message = "取得客戶資料";
//            //result.Data = dbData;
//            return result;
//        }
//        #endregion
//    }
//}
