using Main_Common.Enum;
using ERP_EF;
using Main_Common.Model.Account;
using Main_Common.Model.DTO;
using Main_Common.Model.Result;
using Main_Common.Mothod.Message;
//using Main_Web_EF.Repositories;
//using Main_Web_EF.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using Main_Common.Model.ResultApi;
using Main_Common.Mothod.Validation;
using Main_Common.ExtensionMethod;
using Main_Common.Model.DTO.Cust;
using Main_Common.Model.Search;
//using ERP_APP.Service.S_CUST;
using Main_Common.Model.Data;
using Main_Common.Model.Main;
using Main_Common.Tool;
using Main_EF.Interface;
using Main_Service.Service.S_Log;
using Main_EF.Table;
using Main_Common.Enum.E_StatusType;
using ERP_EF.Models;
using Main_Common.Model.Tool;
//using ERP_APP.Service.S_Order;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Main_Common.Model.ERP;
using Main_Common.Model.DTO.Order;
//using ERP_APP.Service.S_Order;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Main_Common.Model.ResultApi.Order;

namespace Web_API_APP.Service
{
    /// <summary>
    /// 【Api】訂單相關 
    /// </summary>
    public class Order_Service
    {
        #region == 【DI注入用宣告】 ==
        /// <summary>
        /// 資料庫工作單元
        /// </summary>
        public readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// 【DTO】主系統資料
        /// </summary>
        public readonly MainSystem_DTO _MainSystem_DTO;
        /// <summary>
        /// 【Main Service】Log相關
        /// </summary>
        public readonly LogService_Main _LogService_Main;
        /// <summary>
        /// 訂單-ERP
        /// </summary>
        //public readonly Order_Service_Erp Order_Service_ERP;
        /// <summary>
        /// 訊息處理
        /// </summary>
        //public readonly Message_Tool _Message_Tool;
        /// <summary>
        /// ERPDB
        /// </summary>
        private readonly DB_T014Context _DB_T014Context;
        #endregion

        #region == 【全域宣告】 ==
        // ...
        #endregion

        //--【建構】=================================================================================

        #region == 建構 ==
        /// <summary>
        /// 建構
        /// </summary>
        ///// <param name="unitOfWork">資料庫工作單元</param>
        /// <param name="mainSystem_DTO">主系統資料</param>
        ///// <param name="logService_Main">Log相關</param>
        ///// <param name="messageTool">訊息處理</param>
        public Order_Service(
            IUnitOfWork unitOfWork,            
            MainSystem_DTO mainSystem_DTO,
            LogService_Main logService_Main,
            //Order_Service_Erp _Order_Service_ERP,
            DB_T014Context _DB_T014Context
        //    //Message_Service _Message_Service,
        //    Message_Tool messageTool
            ) 
        {
            this._unitOfWork = unitOfWork;            
            this._MainSystem_DTO = mainSystem_DTO;
            this._LogService_Main = logService_Main;
            //this.Order_Service_ERP = _Order_Service_ERP;
            this._DB_T014Context = _DB_T014Context;
        //    //this._Message_Service = Message_Service;

        //    this._Message_Tool = messageTool;
        }
        #endregion

        //--【方法】=================================================================================

        #region == 【全域變數】參數、屬性 ==
        private UserSession_Model _UserSessionModel = null; //登入者資訊
        private string Com_MainKey = null; //共用主要Key(使用前請先重置)
        private string LogErrorMsg = null; //共用Log錯誤訊息(使用前請先重置)
        private string ResultMsg_Finally = null; //共用最終回傳訊息(使用前請先重置)
        private bool Com_Check = false; //共用檢查結果(使用前請先重置)
        private bool Com_IsExist = false; //共用是否存在(使用前請先重置)
        private bool Com_Result = false; //共用處理結果(使用前請先重置)
        private ResultOutput Com_Result_DTO = null; //共用結果(使用前請先重置)
        private List<string> Com_TextList = null; //共用文字清單(使用前請先重置)
        private List<string> Com_Split = null; //共用Split結果(使用前請先重置)
        private List<SelectItemDTO> DropList_DTO = null; //共用下拉清單(使用前請先重置)

        private HttpClient _HttpClient = new HttpClient();
        private string URL = "";
        private string route = "";

        //private List<Cust_DTO> _CUST_DTOs = new List<Cust_DTO>();
        //private List<Product_DTO> _Product_DTOs = new List<Product_DTO>();
        //private List<Warehouse_Area_DTO> _Warehouse_Area_DTOs = new List<Warehouse_Area_DTO>();
        //private List<InBoundOrder_TF_DTO> _InBoundOrder_TF_DTOs = new List<InBoundOrder_TF_DTO>();
        //private List<OutBoundOrder_TF_DTO> _OutBoundOrder_TF_DTOs = new List<OutBoundOrder_TF_DTO>();
        #endregion

        ////--【建構】=================================================================================

        #region == 建構 ==
        /// <summary>
        /// 【建構】同源EntityContext
        /// </summary>
        /// <param name="input"></param>
        //public Order_Service(UserSession_Model input)
        //{
        //    //var db = new C_Main_DB();
        //    //var db = new ERP_DB();
        //    //_Companys_Repository = new C_Main_Repository<Companys>(db);
        //    //_CUSTRepository = new C_ERP_Repository<CUST>(db);

        //    _UserSessionModel = input; //保存
        //}
        #endregion

        //--【方法】=================================================================================

        #region == 訂單相關 ==
        /// <summary>
        /// 取得全部訂單資料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public List<OrderMFDTO> GetOrders(Order_Filter input)
        //{
        //    var today = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString()); // 當前時間(不含毫秒)
        //    #region == 參數 ==
        //    //var result = new ResultOutput_Data<List<OrderMFDTO>>(); // (true, new List<OrderMFDTO>());
        //    MessageInfo exceptionDTO = null; //用來紀錄錯誤訊息
        //    #endregion

        //    #region == 取得完整資料 ==  
        //    var result  = Order_Service_ERP.GetOrders();
        //    return result;
        //    #endregion

        //    #region == 檢查-權限 ==
        //    // 檢查
        //    //Com_Result_DTO = Validation_Tool.Is_AllowAction_ByCompanyLevelCheck(_UserSessionModel, 1);

        //    //// [T：失敗]
        //    //if (!Com_Result_DTO.IsSuccess)
        //    //{
        //    //    return new ResultOutput_Data<List<CUST_DTO>>(false, E_StatusCode.失敗, Com_Result_DTO.Message, null);
        //    //}
        //    #endregion

        //    #region == 過濾 ==
        //    //if (!string.IsNullOrEmpty(input.No))
        //    //{
        //    //    result.Data = result.Data.Where(x => x == input.No).ToList();
        //    //}
        //    //// Name
        //    //if (!string.IsNullOrEmpty(input.Name_Keyword))
        //    //{
        //    //    result.Data = result.Data.Where(x => x.NAME.Contains(input.Name_Keyword)).ToList();
        //    //}
        //    #endregion

        //    //#region == 查詢值 ==
        //    //var pageDTO = new Pageing_DTO
        //    //{
        //    //    IsEnable = input.PageNumber != 0 ? true : false,
        //    //    PageNumber = input.PageNumber ?? 1,
        //    //    PageSize = input.PageSize ?? 10,
        //    //};
        //    //#endregion

        //    //result.E_StatusCode = E_StatusCode.成功;
        //    //result.Title = "訂單基本";
        //    //result.Message = "取得完整訂單基本資料";
        //    //result.Data = dbData;

        //    //#region == 分頁處理 ==
        //    ////// 是否分頁。 [T：不分頁][F：分頁]
        //    //if (pageDTO == null || pageDTO.IsEnable == false)
        //    //{
        //    //    result.Pageing_DTO.TotalCount = result.Data.Count();
        //    //}
        //    //else
        //    //{
        //    //    pageDTO.TotalCount = result.Data.Count();
        //    //    result.Pageing_DTO = pageDTO; //給result值
        //    //    result.Data = result.Data.Skip((pageDTO.PageNumber - 1) * pageDTO.PageSize).Take(pageDTO.PageSize).ToList();
        //    //}
        //    //#endregion

        //    //return result;    
        //}

        /// <summary>
        /// 取得訂單資料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public ResultOutput_Data<List<OrderMFDTO>> GetOrder(Order_Filter input)
        //{
        //    var today = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString()); // 當前時間(不含毫秒)
        //    #region == 參數 ==
        //    var result = new ResultOutput_Data<List<OrderMFDTO>>(true, new List<OrderMFDTO>());
        //    MessageInfo exceptionDTO = null; //用來紀錄錯誤訊息
        //    #endregion

        //    #region == 取得完整資料 ==  
        //    var dbData = Order_Service_ERP.GetOrders();
        //    #endregion

        //    #region == 檢查-權限 ==
        //    // 檢查
        //    //Com_Result_DTO = Validation_Tool.Is_AllowAction_ByCompanyLevelCheck(_UserSessionModel, 1);

        //    //// [T：失敗]
        //    //if (!Com_Result_DTO.IsSuccess)
        //    //{
        //    //    return new ResultOutput_Data<List<CUST_DTO>>(false, E_StatusCode.失敗, Com_Result_DTO.Message, null);
        //    //}
        //    #endregion

        //    result.E_StatusCode = E_StatusCode.成功;
        //    result.Title = "訂單基本資料";
        //    result.Message = "取得訂單基本資料";
        //    result.Data = dbData;

        //    #region == 過濾 ==
        //    //if (!string.IsNullOrEmpty(input.No))
        //    //{
        //    //    result.Data = result.Data.Where(x => x.no == input.No).ToList();
        //    //}
        //    //// Name
        //    //if (!string.IsNullOrEmpty(input.Name_Keyword))
        //    //{
        //    //    result.Data = result.Data.Where(x => x.name.Contains(input.Name_Keyword)).ToList();
        //    //}
        //    #endregion

        //    #region == 查詢值 ==
        //    var pageDTO = new Pageing_DTO
        //    {
        //        IsEnable = input.PageNumber != 0 ? true : false,
        //        PageNumber = input.PageNumber ?? 1,
        //        PageSize = input.PageSize ?? 10,
        //    };
        //    #endregion

            

        //    #region == 分頁處理 ==
        //    //// 是否分頁。 [T：不分頁][F：分頁]
        //    if (pageDTO == null || pageDTO.IsEnable == false)
        //    {
        //        result.Pageing_DTO.TotalCount = result.Data.Count();
        //    }
        //    else
        //    {
        //        pageDTO.TotalCount = result.Data.Count();
        //        result.Pageing_DTO = pageDTO; //給result值
        //        result.Data = result.Data.Skip((pageDTO.PageNumber - 1) * pageDTO.PageSize).Take(pageDTO.PageSize).ToList();
        //    }
        //    #endregion

        //    return result;
        //}

        /// <summary>
        /// 依Keyin日，取得新增訂單
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        public List<MovingOrderSumaryApiDto> Setup(DateTime today,string token)
        {
            var input = new { date = today };
            try
            {
                route = "MovingOrders/";
                string con = String.Format(route + "Setup?date={0}", today.ToString("yyyy/MM/dd"));
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44303/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync(con).Result; // route + "Setup", input).Result;
                    if(response.IsSuccessStatusCode)
                    {
                        var data = response.Content.ReadAsStringAsync().Result;

                        //反序列化
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };
                        var j = JsonSerializer.Deserialize<List<MovingOrderSumaryApiDto>>(data,options);
                        return j;
                    }
                    
                }
            }
            catch
            {

            }
            return null;        

        }

        /// <summary>
        /// 依Keyin日，取得修改訂單
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        public List<MovingOrderSumaryApiDto> Changed(DateTime today, string token)
        {
            string con = String.Format(route + "Changed?date={0}", today.ToString("yyyy/MM/dd"));
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44303/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync(con).Result; // route + "Setup", input).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;

                    //反序列化
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    var j = JsonSerializer.Deserialize<List<MovingOrderSumaryApiDto>>(data, options);
                    return j;
                }

            }

            return null;

        }


        #endregion
    }
}
