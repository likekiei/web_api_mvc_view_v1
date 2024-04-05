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
////using ERP_APP.Service.S_CUST;
//using Main_Common.Model.Data;
//using Main_Common.Model.Main;
//using Main_Common.Tool;
//using Main_EF.Interface;
//using Main_Service.Service.S_Log;
//using Main_EF.Table;
//using Main_Common.Enum.E_StatusType;
//using ERP_EF.Models;
//using Main_Common.Model.Tool;
//using ERP_APP.Service.S_Employee;
//using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json.Linq;
//using Main_Common.Model.ERP;
//using Main_Common.Model.DTO.Employee;
//using ERP_EF.Repository;

//namespace Web_API_APP.Service
//{
//    /// <summary>
//    /// 【Api】出入庫相關 (所有資料默認都要視情況過濾公司，例外狀況請特別處理)
//    /// </summary>
//    public class Employee_Service
//    {
//        #region == 【DI注入用宣告】 ==
//        /// <summary>
//        /// 資料庫工作單元
//        /// </summary>
//        public readonly IUnitOfWork _unitOfWork;
//        /// <summary>
//        /// 【DTO】主系統資料
//        /// </summary>
//        public readonly MainSystem_DTO _MainSystem_DTO;
//        /// <summary>
//        /// 【Main Service】Log相關
//        /// </summary>
//        public readonly LogService_Main _LogService_Main;
//        /// <summary>
//        /// 員工-ERP
//        /// </summary>
//        public readonly Employee_Service_Erp Employee_Service_ERP;
//        /// <summary>
//        /// 訊息處理
//        /// </summary>
//        //public readonly Message_Tool _Message_Tool;
//        /// <summary>
//        /// ERPDB
//        /// </summary>
//        //private readonly DB_T020Context _DB_T020Context;
//        /// <summary>
//        /// 採購受訂表頭檔
//        /// </summary>
//        //private readonly C_ERP_Repository<MF_POS> _MF_POS_Repository;
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
//        public Employee_Service(
//            IUnitOfWork unitOfWork,            
//            MainSystem_DTO mainSystem_DTO,
//            LogService_Main logService_Main,
//            Employee_Service_Erp _Employee_Service_ERP
//            //DB_T020Context db
//        //    //Message_Service _Message_Service,
//        //    Message_Tool messageTool
//            ) 
//        {
//            this._unitOfWork = unitOfWork;            
//            this._MainSystem_DTO = mainSystem_DTO;
//            this._LogService_Main = logService_Main;
//            this.Employee_Service_ERP = _Employee_Service_ERP;
//            //this._DB_T020Context = _DB_T020Context;
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
//        //private Employee_Service_Erp _Employee_Service;
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
//        //public Employee_Service(UserSession_Model input)
//        //{
//        //    //var db = new C_Main_DB();
//        //    //var db = new ERP_DB();
//        //    //_Companys_Repository = new C_Main_Repository<Companys>(db);
//        //    //_CUSTRepository = new C_ERP_Repository<CUST>(db);

//        //    _UserSessionModel = input; //保存
//        //}
//        #endregion

//        //--【方法】=================================================================================

//        #region == 員工相關 ==
//        /// <summary>
//        /// 取得全部員工資料
//        /// </summary>
//        /// <param name="input"></param>
//        /// <returns></returns>
//        public ResultOutput_Data<List<EmployeeDTO>> GetEmployees(Employee_Filter input)
//        {
//            var today = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString()); // 當前時間(不含毫秒)
//            #region == 參數 ==
//            var result = new ResultOutput_Data<List<EmployeeDTO>>(true, new List<EmployeeDTO>());
//            MessageInfo exceptionDTO = null; //用來紀錄錯誤訊息
//            #endregion

//            #region == 取得完整資料 ==  
//            var dbData = Employee_Service_ERP.GetUsers();
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
//            //if (!string.IsNullOrEmpty(input.No))
//            //{
//            //    result.Data = result.Data.Where(x => x == input.No).ToList();
//            //}
//            //// Name
//            //if (!string.IsNullOrEmpty(input.Name_Keyword))
//            //{
//            //    result.Data = result.Data.Where(x => x.NAME.Contains(input.Name_Keyword)).ToList();
//            //}
//            #endregion

//            #region == 查詢值 ==
//            var pageDTO = new Pageing_DTO
//            {
//                IsEnable = input.PageNumber != 0 ? true : false,
//                PageNumber = input.PageNumber ?? 1,
//                PageSize = input.PageSize ?? 10,
//            };
//            #endregion

//            result.E_StatusCode = E_StatusCode.成功;
//            result.Title = "員工基本";
//            result.Message = "取得完整員工基本資料";
//            result.Data = dbData;

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
            
//            return result;    
//        }

//        /// <summary>
//        /// 取得員工資料
//        /// </summary>
//        /// <param name="input"></param>
//        /// <returns></returns>
//        public ResultOutput_Data<List<EmployeeDTO>> GetEmployee(Employee_Filter input)
//        {
//            var today = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString()); // 當前時間(不含毫秒)
//            #region == 參數 ==
//            var result = new ResultOutput_Data<List<EmployeeDTO>>(true, new List<EmployeeDTO>());
//            MessageInfo exceptionDTO = null; //用來紀錄錯誤訊息
//            #endregion

//            #region == 取得完整資料 ==  
//            var dbData = Employee_Service_ERP.GetUsers();
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

//            result.E_StatusCode = E_StatusCode.成功;
//            result.Title = "員工基本資料";
//            result.Message = "取得員工基本資料";
//            result.Data = dbData;

//            #region == 過濾 ==
//            if (!string.IsNullOrEmpty(input.No))
//            {
//                result.Data = result.Data.Where(x => x.no == input.No).ToList();
//            }
//            // Name
//            if (!string.IsNullOrEmpty(input.Name_Keyword))
//            {
//                result.Data = result.Data.Where(x => x.name.Contains(input.Name_Keyword)).ToList();
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

//            return result;
//        }

//        /// <summary>
//        /// 今日基本資料異動的員工
//        /// </summary>
//        /// <returns></returns>
//        public ResultOutput_Data<List<EmployeeDTO>> GetUpdateUser()
//        {
//            var today = Convert.ToDateTime(DateTime.UtcNow.AddHours(8));// ; ; // 當前時間(不含毫秒)
//            #region == 參數 ==
//            var result = new ResultOutput_Data<List<EmployeeDTO>>(true, new List<EmployeeDTO>());
//            MessageInfo exceptionDTO = null; //用來紀錄錯誤訊息
//            #endregion

//            #region == 取得完整資料 ==  
//            var dbData = Employee_Service_ERP.GetUsers();
//            #endregion

//            #region
//            dbData = dbData.Where(x => (x.updateDate.GetValueOrDefault().Date == today.Date
//                                    || x.clsDate.GetValueOrDefault().Date == today.Date)
//                                    && !x.retireDate.HasValue).ToList();
//            //dbData = dbData.Where(x => ((x.updateDate >= today.Date && x.updateDate < today.Date.AddDays(1)) 
//            //                         || (x.clsDate >= today.Date) && x.clsDate < today.Date.AddDays(1))
//            //                         && !x.retireDate.HasValue ).ToList();
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

//            result.E_StatusCode = E_StatusCode.成功;
//            result.Title = "員工基本資料";
//            result.Message = "今日基本資料異動的員工";
//            result.Data = dbData;

//            #region == 過濾 ==
//            //if (!string.IsNullOrEmpty(input.No))
//            //{
//            //    result.Data = result.Data.Where(x => x.no == input.No).ToList();
//            //}
//            //// Name
//            //if (!string.IsNullOrEmpty(input.Name_Keyword))
//            //{
//            //    result.Data = result.Data.Where(x => x.name.Contains(input.Name_Keyword)).ToList();
//            //}
//            #endregion

//            #region == 查詢值 ==
//            var pageDTO = new Pageing_DTO
//            {
//                IsEnable =  false,
//                PageNumber = 0,
//                PageSize = 0,
//            };
//            #endregion

//            #region == 分頁處理 ==
//            // 是否分頁。 [T：不分頁][F：分頁]
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

//            return result;
//        }

//        /// <summary>
//        /// 更新離職員工
//        /// </summary>
//        /// <returns></returns>
//        public ResultOutput_Data<List<EmployeeDTO>> UpdateLeaveEmployee()
//        {
//            var today = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString("d")); // 當前時間(不含毫秒)
//            #region == 參數 ==
//            var result = new ResultOutput_Data<List<EmployeeDTO>>(true, new List<EmployeeDTO>());
//            MessageInfo exceptionDTO = null; //用來紀錄錯誤訊息
//            #endregion

//            #region == 取得完整資料 ==  
//            var dbData = Employee_Service_ERP.GetUsers();
//            #endregion

//            #region
//            dbData = dbData.Where(x => x.updateDate.GetValueOrDefault().Date == today.Date
//                                    && x.retireDate.HasValue).ToList();
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

//            result.E_StatusCode = E_StatusCode.成功;
//            result.Title = "員工基本資料";
//            result.Message = "今日離職員工";
//            result.Data = dbData;

//            #region == 過濾 ==
//            //if (!string.IsNullOrEmpty(input.No))
//            //{
//            //    result.Data = result.Data.Where(x => x.no == input.No).ToList();
//            //}
//            //// Name
//            //if (!string.IsNullOrEmpty(input.Name_Keyword))
//            //{
//            //    result.Data = result.Data.Where(x => x.name.Contains(input.Name_Keyword)).ToList();
//            //}
//            #endregion

//            #region == 查詢值 ==
//            var pageDTO = new Pageing_DTO
//            {
//                IsEnable = false,
//                PageNumber = 0,
//                PageSize = 0,
//            };
//            #endregion

//            #region == 分頁處理 ==
//            // 是否分頁。 [T：不分頁][F：分頁]
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

//            return result;

//        }

//        /// <summary>
//        /// 更新請假資訊
//        /// </summary>
//        /// <param name="day"></param>
//        /// <returns></returns>
//        public ResultOutput_Data<List<LeaveDTO>> UpdateAttendanceRecord(DateTime day)
//        {
//            #region == 參數 ==
//            var result = new ResultOutput_Data<List<LeaveDTO>>(true, new List<LeaveDTO>());
//            MessageInfo exceptionDTO = null; //用來紀錄錯誤訊息
//            #endregion

//            #region == 取得完整資料 ==  
//            var dbData = Employee_Service_ERP.GetUserOnVocation(day);
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

//            result.E_StatusCode = E_StatusCode.成功;
//            result.Title = "員工基本資料";
//            result.Message = "請假資訊";
//            result.Data = dbData;

//            #region == 過濾 ==
//            //if (!string.IsNullOrEmpty(input.No))
//            //{
//            //    result.Data = result.Data.Where(x => x.no == input.No).ToList();
//            //}
//            //// Name
//            //if (!string.IsNullOrEmpty(input.Name_Keyword))
//            //{
//            //    result.Data = result.Data.Where(x => x.name.Contains(input.Name_Keyword)).ToList();
//            //}
//            #endregion

//            #region == 查詢值 ==
//            var pageDTO = new Pageing_DTO
//            {
//                IsEnable = false,
//                PageNumber = 0,
//                PageSize = 0,
//            };
//            #endregion

//            #region == 分頁處理 ==
//            // 是否分頁。 [T：不分頁][F：分頁]
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

//            return result;            
//        }

//        /// <summary>
//        /// 更新員工上班打卡紀錄
//        /// </summary>
//        public ResultOutput_Data<List<LeaveDTO>> UpdateClock(DateTime? day)
//        {
//            #region == 參數 ==
//            var result = new ResultOutput_Data<List<LeaveDTO>>(true, new List<LeaveDTO>());
//            MessageInfo exceptionDTO = null; //用來紀錄錯誤訊息
//            #endregion

//            #region == 取得打卡資料 ==  
//            //var dbData = Employee_Service_ERP.UpdateClock(day);
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

//            result.E_StatusCode = E_StatusCode.成功;
//            result.Title = "員工基本資料";
//            result.Message = "請假資訊";
//            //result.Data = dbData;

//            #region == 過濾 ==
//            //if (!string.IsNullOrEmpty(input.No))
//            //{
//            //    result.Data = result.Data.Where(x => x.no == input.No).ToList();
//            //}
//            //// Name
//            //if (!string.IsNullOrEmpty(input.Name_Keyword))
//            //{
//            //    result.Data = result.Data.Where(x => x.name.Contains(input.Name_Keyword)).ToList();
//            //}
//            #endregion

//            #region == 查詢值 ==
//            var pageDTO = new Pageing_DTO
//            {
//                IsEnable = false,
//                PageNumber = 0,
//                PageSize = 0,
//            };
//            #endregion

//            #region == 分頁處理 ==
//            // 是否分頁。 [T：不分頁][F：分頁]
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

//            return result;
//        }

//        /// <summary>
//        /// 【最新】更新員工遲到紀錄
//        /// </summary>
//        public ResultOutput_Data<List<LeaveDTO>> UpdateLateRecord(DateTime? day)
//        {
//            #region == 參數 ==
//            var result = new ResultOutput_Data<List<LeaveDTO>>(true, new List<LeaveDTO>());
//            MessageInfo exceptionDTO = null; //用來紀錄錯誤訊息
//            #endregion

//            #region == 取得打卡資料 ==  
//            //var dbData = Employee_Service_ERP.UpdateClock(day);
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

//            result.E_StatusCode = E_StatusCode.成功;
//            result.Title = "員工基本資料";
//            result.Message = "請假資訊";
//            //result.Data = dbData;

//            #region == 過濾 ==
//            //if (!string.IsNullOrEmpty(input.No))
//            //{
//            //    result.Data = result.Data.Where(x => x.no == input.No).ToList();
//            //}
//            //// Name
//            //if (!string.IsNullOrEmpty(input.Name_Keyword))
//            //{
//            //    result.Data = result.Data.Where(x => x.name.Contains(input.Name_Keyword)).ToList();
//            //}
//            #endregion

//            #region == 查詢值 ==
//            var pageDTO = new Pageing_DTO
//            {
//                IsEnable = false,
//                PageNumber = 0,
//                PageSize = 0,
//            };
//            #endregion

//            #region == 分頁處理 ==
//            // 是否分頁。 [T：不分頁][F：分頁]
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

//            return result;

//            //var _YGService = new YGService(dbSun);

//            ////取得昨天日期
//            //var today = DateTime.UtcNow.AddHours(8).AddDays(-1).Date;
//            //if (day.HasValue)
//            //{
//            //    today = day.Value;
//            //}

//            //#region 1.取得搬家平台提供之目前打卡資訊
//            //var _empService = new A_EmployeeService(Token);
//            //var data = _empService.AttendanceRecord(today);
//            //#endregion

//            //#region 2-1.遲到資料寫入ERP
//            //var dataLate = data.Where(x => Int64.Parse(x.delayTime) != 0).ToList();
//            ////將其所屬部門代號，其開頭非為K的工務部門帶入
//            //dataLate = dataLate.Where(x => !_YGService.GetNoAndDepAndType(x.idNumber, today).Dep.StartsWith("K")).ToList();

//            //var items = dataLate.Select(x => new TF_KQTZ
//            //{
//            //    YG_NO = _YGService.GetNO(x.idNumber, today),
//            //    SZ_NO = "C01",
//            //    TRS_DD = today,
//            //    SZ_YM = new DateTime(today.Year, today.Month, 1),
//            //    UNIT = "4",
//            //    QTY = decimal.Parse(x.delayTime),
//            //    CALC_ID = "F",
//            //    BAN_NO = "01",
//            //    ADD_ID = ""
//            //}).ToList();

//            //if (dataLate != null)
//            //{
//            //    _KQTZService = new KQTZService(dbSun);
//            //    _KQTZService.Update(items);
//            //}
//            //#endregion

//            #region 2-2. 打卡記錄回寫
//            //var source = new List<MF_CLOCK>();
//            //foreach (var x in data)
//            //{
//            //    if (!string.IsNullOrEmpty(x.checkin))
//            //    {
//            //        var container = x.checkin.Split(':');
//            //        var clockHour = Int32.Parse(container[0]);
//            //        var clockMin= Int32.Parse(container[1]);
//            //        var itemIn = new MF_CLOCK
//            //        {
//            //            CLOCK_ID = _YGService.GetNO(x.idNumber),
//            //            YG_NO = _YGService.GetNO(x.idNumber),
//            //            CLOCK_TIME = new DateTime(x.shouldCheckIn.Year,x.shouldCheckIn.Month,x.shouldCheckIn.Day,clockHour,clockMin,0)
//            //        };
//            //        source.Add(itemIn);

//            //    }
//            //    if (!string.IsNullOrEmpty(x.checkout))
//            //    {
//            //        var container = x.checkout.Split(':');
//            //        var clockHour = Int32.Parse(container[0]);
//            //        var clockMin = Int32.Parse(container[1]);

//            //        var itemOut = new MF_CLOCK
//            //        {
//            //            CLOCK_ID = _YGService.GetNO(x.idNumber),
//            //            YG_NO = _YGService.GetNO(x.idNumber),
//            //            CLOCK_TIME = new DateTime(x.shouldCheckIn.Year, x.shouldCheckIn.Month, x.shouldCheckIn.Day, clockHour, clockMin, 0)
//            //        };
//            //        source.Add(itemOut);
//            //    }

//            //}

//            //if (source != null)
//            //{
//            //    _ClockService = new ClockService(dbSun);
//            //    _ClockService.Update(source);
//            //}

//            #endregion

//        }
//        #endregion

//        //#region == 取得客戶資料 ==
//        ///// <summary>
//        ///// 取得客戶資料
//        ///// </summary>
//        ///// <returns></returns>
//        //public ResultOutput_Data <List<CUST_DTO>> GetCUSTs(CUST_Filter input)
//        //{
//        //    var today = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString()); // 當前時間(不含毫秒)
//        //    var result = new ResultOutput_Data<List<CUST_DTO>>(true, new List<CUST_DTO>());
//        //    //_Employee_Service = new Employee_Service_Erp(_UserSession_Model);                      



//        //    #region == 整理資料 ==  
//        //    //result = Employee_Service_ERP.Get_CUST_Datas();
//        //    //var dbData = db.ConvertModelList<CUST, CUST_DTO>();

//        //    #endregion

//        //    #region == 檢查-權限 ==
//        //    // 檢查
//        //    //Com_Result_DTO = Validation_Tool.Is_AllowAction_ByCompanyLevelCheck(_UserSessionModel, 1);

//        //    //// [T：失敗]
//        //    //if (!Com_Result_DTO.IsSuccess)
//        //    //{
//        //    //    return new ResultOutput_Data<List<CUST_DTO>>(false, E_StatusCode.失敗, Com_Result_DTO.Message, null);
//        //    //}
//        //    #endregion                      

//        //    #region == 過濾 ==
//        //    if (!string.IsNullOrEmpty(input.No))
//        //    {
//        //        result.Data = result.Data.Where(x => x.CUS_NO == input.No).ToList();
//        //    }
//        //    // Name
//        //    if (!string.IsNullOrEmpty(input.Name_Keyword))
//        //    {
//        //        result.Data = result.Data.Where(x => x.NAME.Contains(input.Name_Keyword)).ToList();
//        //    }
//        //    #endregion

//        //    #region == 查詢值 ==
//        //    var pageDTO = new Pageing_DTO
//        //    {
//        //        IsEnable = input.PageNumber != 0 ? true : false,
//        //        PageNumber = input.PageNumber ?? 1,
//        //        PageSize = input.PageSize ?? 10,
//        //    };
//        //    #endregion

//        //    #region == 分頁處理 ==
//        //    //// 是否分頁。 [T：不分頁][F：分頁]
//        //    if (pageDTO == null || pageDTO.IsEnable == false)
//        //    {
//        //        result.Pageing_DTO.TotalCount = result.Data.Count();
//        //    }
//        //    else
//        //    {
//        //        pageDTO.TotalCount = result.Data.Count();
//        //        result.Pageing_DTO = pageDTO; //給result值
//        //        result.Data = result.Data.Skip((pageDTO.PageNumber - 1) * pageDTO.PageSize).Take(pageDTO.PageSize).ToList();
//        //    }
//        //    #endregion
//        //    result.E_StatusCode = E_StatusCode.成功;
//        //    //result.Title = "客戶資料";
//        //    result.Message = "取得完整客戶資料";
//        //    //result.Data = dbData;
//        //    return result;
//        //}

//        ///// <summary>
//        ///// 取得客戶資料
//        ///// </summary>
//        ///// <returns></returns>
//        //public ResultOutput_Data<List<CUST_DTO>> GetCUST(CUST_Filter input)
//        //{
//        //    var today = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString()); // 當前時間(不含毫秒)
//        //    var result = new ResultOutput_Data<List<CUST_DTO>>(true, new List<CUST_DTO>());
//        //    //_Employee_Service = new Employee_Service_Erp(_UserSessionModel);

//        //    #region == 前置檢查 ==
//        //    //[T：無值][]
//        //    if (input == null)
//        //    {
//        //        //result.Message_Exception = new MessageInfo(false, $"");
//        //        result.E_StatusCode = E_StatusCode.檢查異常;
//        //        result.Message = $"【檢查異常】傳入參數存在null值";

//        //        result.IsSuccess = false;
//        //        //result.Message_Exception = exceptionDTO;
//        //        //result.Data = null;
//        //        return result;
//        //    }
//        //    #endregion


//        //    #region == 整理資料 ==  
//        //    //result = Employee_Service_ERP.Get_CUST_Datas();
//        //    //var dbData = db.ConvertModelList<CUST, CUST_DTO>();
//        //    //var Data = new Employee_DTO();
//        //    //result.Data = dbData.to;
//        //    #endregion

//        //    #region == 檢查-權限 ==
//        //    //// 檢查
//        //    //Com_Result_DTO = Validation_Tool.Is_AllowAction_ByCompanyLevelCheck(_UserSessionModel, 1);

//        //    //// [T：失敗]
//        //    //if (!Com_Result_DTO.IsSuccess)
//        //    //{
//        //    //    return new ResultOutput_Data<List<CUST_DTO>>(false, E_StatusCode.失敗, Com_Result_DTO.Message, null);
//        //    //}
//        //    #endregion

//        //    #region == 過濾 ==           

//        //    // No
//        //    if (!string.IsNullOrEmpty(input.No))
//        //    {
//        //        result.Data = result.Data.Where(x => x.CUS_NO == input.No).ToList();
//        //    }
//        //    // Name
//        //    if (!string.IsNullOrEmpty(input.Name_Keyword))
//        //    {
//        //        result.Data = result.Data.Where(x => x.NAME == input.Name_Keyword).ToList();
//        //    }          
//        //    #endregion

//        //    #region == 查詢值 ==
//        //    //input.PageNumber
//        //    var pageDTO = new Pageing_DTO
//        //    {
//        //        IsEnable = input.PageNumber.HasValue ? true : false,
//        //        PageNumber = input.PageNumber ?? 1,
//        //        PageSize = input.PageSize ?? 10,
//        //    };
//        //    #endregion

//        //    #region == 分頁處理 ==
//        //    //// 是否分頁。 [T：不分頁][F：分頁]
//        //    if (pageDTO == null || pageDTO.IsEnable == false)
//        //    {
//        //        result.Pageing_DTO.TotalCount = result.Data.Count();
//        //    }
//        //    else
//        //    {
//        //        pageDTO.TotalCount = result.Data.Count();
//        //        result.Pageing_DTO = pageDTO; //給result值
//        //        //dbData = dbData.OrderByDescending(o => o.Doc_Date).ThenByDescending(o => o.No).Skip((pageDTO.PageNumber - 1) * pageDTO.PageSize).Take(pageDTO.PageSize);
//        //    }
//        //    #endregion
//        //    result.E_StatusCode = E_StatusCode.成功;
//        //    //result.Title = "客戶資料";
//        //    result.Message = "取得客戶資料";
//        //    //result.Data = dbData;
//        //    return result;
//        //}
//        //#endregion
//    }
//}
