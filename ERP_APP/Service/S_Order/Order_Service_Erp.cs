//using ERP_APP.Service.S_INV_NO;
//using ERP_APP.Service.S_MF_ARP;
//using ERP_APP.Service.S_MF_PSS;
//using ERP_APP.Service.S_TF_PSS;
//using ERP_EF.Models;
//using ERP_EF.Repository;
//using Main_Common.Enum;
//using Main_Common.Enum.E_StatusType;
//using Main_Common.ExtensionMethod;
//using Main_Common.Model.Account;
//using Main_Common.Model.Data;
//using Main_Common.Model.DTO.Cust;
//using Main_Common.Model.DTO.Order;
//using Main_Common.Model.ERP;

//using Main_Common.Model.Main;
//using Main_Common.Model.Result;
//using Main_Common.Model.ResultApi;
//using Main_Common.Model.ResultApi.Order;
//using Main_Common.Model.Tool;
//using Main_Common.Mothod.Message;
//using Main_Common.Tool;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Transactions;
//using System.Xml.Schema;

//namespace ERP_APP.Service.S_Order
//{
//    /// <summary>
//    /// 受訂單相關
//    /// </summary>
//    public class Order_Service_Erp
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
//        /// 【Tool】訊息處理
//        /// </summary>
//        public readonly Message_Tool _Message_Tool;
//        /// <summary>
//        /// ERPDB
//        /// </summary>
//        private readonly DB_T020Context _DB_T020Context;
//        /// <summary>
//        /// 進銷貨表頭檔
//        /// </summary>
//        private readonly C_ERP_Repository<MF_PSS> _MF_PSS_Repository;
//        /// <summary>
//        /// 進銷貨表身檔
//        /// </summary>
//        //private readonly C_ERP_Repository<MF_PSS_Z> _MF_PSS_Z_Repository;
//        /// <summary>
//        /// 採購受訂表頭檔
//        /// </summary>
//        private readonly C_ERP_Repository<TF_PSS> _TF_PSS_Repository;
//        /// <summary>
//        /// 採購受訂表頭檔
//        /// </summary>
//        private readonly C_ERP_Repository<TF_PSS_RCV> _TF_PSS_RCV_Repository;
//        /// <summary>
//        /// 發票庫
//        /// </summary>
//        private readonly C_ERP_Repository<INV_NO> _INV_NO_Repository;
//        /// <summary>
//        /// 購買發票庫
//        /// </summary>
//        private readonly C_ERP_Repository<INV_ID> _INV_ID_Repository;
//        /// <summary>
//        /// 進銷貨表頭檔
//        /// </summary>
//        private readonly C_ERP_Repository<MF_ARP> _MF_ARP_Repository;
//        /// <summary>
//        /// 【ERP】發票庫相關
//        /// </summary>
//        public readonly INV_NO_Service_Erp _INV_NO_Service_Erp;
//        /// <summary>
//        /// 【ERP】銷貨單相關
//        /// </summary>
//        public readonly MF_PSS_Service_Erp _MF_PSS_Service_Erp;
//        /// <summary>
//        /// 【ERP】應收付款主庫相關
//        /// </summary>
//        public readonly MF_ARP_Service_Erp _MF_ARP_Service_Erp;
//        /// <summary>
//        /// 【ERP】銷貨單相關
//        /// </summary>
//        public readonly TF_PSS_Service_Erp _TF_PSS_Service_Erp;
//        #endregion

//        #region == 【全域宣告】 ==
//        /// <summary>
//        /// 【DTO】全部資料的DTO
//        /// </summary>
//        public readonly AllDataDTO _AllDataDTO = new AllDataDTO();
//        #endregion

//        //--【建構】=================================================================================

//        #region == 建構 ==
//        /// <summary>
//        /// 建構
//        /// </summary>
//        /// <param name="_DB_T020Context">ERP資料庫</param>
//        /// <param name="mainSystem_DTO">主系統資料</param>
//        /// <param name="logService_Main">Log相關</param>
//        /// <param name="messageTool">訊息處理</param>
//        public Order_Service_Erp(
//            DB_T020Context db,
//            MainSystem_DTO mainSystem_DTO,
//            //LogService_Main logService_Main,
//            INV_NO_Service_Erp _INV_NO_Service_Erp,
//            MF_PSS_Service_Erp _MF_PSS_Service_Erp,
//            MF_ARP_Service_Erp _MF_ARP_Service_Erp,
//            TF_PSS_Service_Erp _TF_PSS_Service_Erp,
//            Message_Tool messageTool)
//        {

//            this._MainSystem_DTO = mainSystem_DTO;
//            //this._DB_T020Context = _DB_T020Context;       
//            //this._LogService_Main = logService_Main;
//            _MF_PSS_Repository = new C_ERP_Repository<MF_PSS>(db);
//            //_MF_PSS_Z_Repository = new C_ERP_Repository<MF_PSS_Z>(db);
//            _TF_PSS_Repository = new C_ERP_Repository<TF_PSS>(db);
//            _TF_PSS_RCV_Repository = new C_ERP_Repository<TF_PSS_RCV>(db);
//            _INV_NO_Repository = new C_ERP_Repository<INV_NO>(db);
//            _INV_ID_Repository = new C_ERP_Repository<INV_ID>(db);
//            _MF_ARP_Repository = new C_ERP_Repository<MF_ARP>(db);

//            this._INV_NO_Service_Erp = _INV_NO_Service_Erp;
//            this._MF_PSS_Service_Erp = _MF_PSS_Service_Erp;
//            this._MF_ARP_Service_Erp = _MF_ARP_Service_Erp;
//            this._TF_PSS_Service_Erp = _TF_PSS_Service_Erp;
//            this._Message_Tool = messageTool;
//        }
//        #endregion

//        //--【方法】=================================================================================
//        #region == 【全域變數】DB、Service ==       
//        #endregion

//        #region == 【全域變數】參數、屬性 ==
//        private UserSession_Model _UserSession_Model = null; //登入者資訊
//        private string Com_MainKey = null; //共用主要Key(使用前請先重置)
//        private string LogErrorMsg = null; //共用Log錯誤訊息(使用前請先重置)
//        private string ResultMsg_Finally = null; //共用最終回傳訊息(使用前請先重置)
//        private bool Com_Check = false; //共用檢查結果(使用前請先重置)
//        private bool Com_IsExist = false; //共用是否存在(使用前請先重置)
//        private bool Com_Result = false; //共用處理結果(使用前請先重置)
//        private ResultOutput Com_Result_DTO = null; //共用結果(使用前請先重置)
//        private List<string> Com_OtherMsg_List = null; //共用其他訊息清單(使用前請先重置)
//        private List<string> Com_TextList = null; //共用文字清單(使用前請先重置)
//        private List<string> Com_Split = null; //共用Split結果(使用前請先重置)
//        private List<SelectItemDTO> DropList_DTO = null; //共用下拉清單(使用前請先重置)

//        //private List<Product_DTO> _Product_DTOs = new List<Product_DTO>();
//        //private List<Order_DTO> _Order_DTOs = new List<Order_DTO>();
//        #endregion

//        //--【方法】=================================================================================
//        /// <summary>
//        /// 取得所有員工資料
//        /// </summary>
//        /// <returns></returns>
//        //public List<OrderDTO> GetUsers()
//        //{
//        //    var result = _DB_T020Context.MF_YG.Select(x => new OrderDTO
//        //    {
//        //        no = x.YG_NO,
//        //        idNumber = x.ID_NO,
//        //        mobilePhone = x.CNT_TEL2,
//        //        name = x.NAME,
//        //        pointNumber = x.DEP,
//        //        hireDate = x.IN_DAY,
//        //        retireDate = x.OUT_DAY,
//        //        clsDate = x.CLS_DATE,
//        //        updateDate = x.MODIFY_DD
//        //    }).ToList();

//        //    return result;
//        //}

//        /// <summary>
//        /// 取得全部訂單基本資料
//        /// </summary>
//        /// <returns></returns>
//        public OrderResult GetSalesOrders(Order_Filter input)
//        {
//            //var result = new List<OrderMFDTO>();
//            var result = new OrderResult();
//            var Datas = new List<OrderMFDTO>();
//            var check = true;
//            MessageInfo exceptionDTO = null; //用來紀錄錯誤訊息
//            #region == DB資料 ==
//            IQueryable<MF_PSS> dbMF_PSS = null; // new IQueryable<MF_PSS>(); //  _MF_PSS_Repository.GetAll();// s(x => x.PS_ID == "SO");
//            //IQueryable<MF_PSS_Z> dbMF_PSS_Z = null; // (); //_MF_PSS_Z_Repository.GetAll();// s(x => x.PS_ID == "SO");
//            IQueryable<TF_PSS> dbTF_PSS = null; // _TF_PSS_Repository.GetAll();// s(x => x.PS_ID == "SO");
//            IQueryable<INV_NO> dbINV_NO = null; // _INV_NO_Repository.GetAll();
//            IQueryable<INV_ID> dbINV_ID = null; // _INV_ID_Repository.GetAll();
//            #endregion
//            try
//            {
//                dbMF_PSS = _MF_PSS_Repository.GetAll();
//                dbTF_PSS = _TF_PSS_Repository.GetAll();
//                #region == 前置檢查 ==
//                if (!string.IsNullOrEmpty(input.Order_Number)) //有傳入資料才處理
//                {
//                    dbMF_PSS = dbMF_PSS.Where(x => x.PS_NO == input.Order_Number);
//                    dbTF_PSS = dbTF_PSS.Where(x => x.PS_NO == input.Order_Number);
//                    if (dbMF_PSS.Count() == 0)
//                    {
//                        //result.PageDTO = pageDTO;
//                        result.IsSuccess = true;
//                        result.Message = $"查無 {input.Order_Number} 此筆銷貨單。";
//                        result.Datas = Datas;
//                        return result;
//                    }
//                    //check = false;

//                }
//                if (input.Query_Date_STA != null && input.Query_Date_END != null)
//                {
//                    dbMF_PSS = dbMF_PSS.Where(x => x.PS_DD >= input.Query_Date_STA && x.PS_DD <= input.Query_Date_END);
//                    dbTF_PSS = dbTF_PSS.Where(x => x.PS_DD >= input.Query_Date_STA && x.PS_DD <= input.Query_Date_END);
//                    if (dbMF_PSS.Count() == 0)
//                    {
//                        //result.PageDTO = pageDTO;
//                        result.IsSuccess = true;
//                        result.Message = $"查無 {input.Query_Date_STA}-{input.Query_Date_END} 間銷貨單。";
//                        result.Datas = Datas;
//                        return result;
//                    }
//                    //check = false;
//                }
//                //if (check)
//                //{
//                //    #region == DB資料 ==
//                //    dbMF_PSS = _MF_PSS_Repository.GetAll();//.ToList();// s(x => x.PS_ID == "SO");
//                //    dbTF_PSS = _TF_PSS_Repository.GetAll();// s(x => x.PS_ID == "SO");
//                //    #endregion
//                //}
//                dbINV_NO = _INV_NO_Repository.GetAll();
//                dbINV_ID = _INV_ID_Repository.GetAll();
//                #endregion

//                #region == Join資料 ==
//                var join_header = from a in dbMF_PSS
//                                  join b in dbINV_NO on a.INV_NO equals b.INV_NO1 into ab
//                                  from b in ab.DefaultIfEmpty()
//                                  select new
//                                  {
//                                      PS_DD = a.PS_DD,
//                                      PS_NO = a.PS_NO,
//                                      CUS_NO = a.CUS_NO,
//                                      DEP = a.DEP,
//                                      TAX_ID = a.TAX_ID,
//                                      ZHANG_ID = a.ZHANG_ID,
//                                      OS_NO = a.OS_NO,
//                                      REM = a.REM,
//                                      VOH_ID = a.VOH_ID,
//                                      INV_NO = a.INV_NO,
//                                      INV_ID = b.INV_ID,
//                                      INV_DD = b.INV_DD,
//                                      INV_YM = b.INV_YM,
//                                      DEP1 = b.DEP,
//                                      UNI_NO_BUY = b.UNI_NO_BUY,
//                                      TITLE_BUY = b.TITLE_BUY,
//                                      UNI_NO_PAY = b.UNI_NO_PAY,
//                                      TITLE_PAY = b.TITLE_PAY,
//                                      AMT = b.AMT,
//                                      TAX_ID1 = b.TAX_ID1,
//                                      TAX = b.TAX,
//                                      TAX_ID2 = b.TAX_ID2,
//                                      CHK_CODE = b.CHK_CODE,
//                                      //Customer = b.Customer,
//                                      //InvoiceAdr = b.InvoiceAdr,
//                                  };
//                var j1 = join_header.ToList();

//                var dbData = join_header.Join(dbTF_PSS, a => a.PS_NO, b => b.PS_NO, (a, b) => new
//                {
//                    PS_DD = a.PS_DD,
//                    PS_NO = a.PS_NO,
//                    CUS_NO = a.CUS_NO,
//                    DEP = a.DEP,
//                    TAX_ID = a.TAX_ID,
//                    ZHANG_ID = a.ZHANG_ID,
//                    OS_NO = a.OS_NO,
//                    REM = a.REM,
//                    VOH_ID = a.VOH_ID,
//                    INV_NO = a.INV_NO,
//                    //Customer = a.Customer,
//                    //InvoiceAdr = a.InvoiceAdr,
//                    INV_ID = a.INV_ID,
//                    INV_DD = a.INV_DD,
//                    INV_YM = a.INV_YM,
//                    DEP1 = a.DEP,
//                    UNI_NO_BUY = a.UNI_NO_BUY,
//                    TITLE_BUY = a.TITLE_BUY,
//                    UNI_NO_PAY = a.UNI_NO_PAY,
//                    TITLE_PAY = a.TITLE_PAY,
//                    AMT = a.AMT,
//                    TAX_ID1 = a.TAX_ID1,
//                    TAX = a.TAX,
//                    TAX_ID2 = a.TAX_ID2,
//                    CHK_CODE = a.CHK_CODE,
//                    //INV_BOOK = a.INV_BOOK,
//                    PRD_NAME = b.PRD_NAME,
//                    WH = b.WH,
//                    QTY = b.QTY,
//                    UP = b.UP,
//                    AMT1 = b.AMT,
//                    UNIT = b.UNIT,
//                    DIS_CNT = b.DIS_CNT,
//                    ITM = b.ITM,
//                }).GroupBy(g => new { g.PS_NO });

//                #endregion

//                #region == 整理資料 ==
//                var dataPSS = dbData.ToList();

//                foreach (var item in dataPSS)
//                {
//                    //表頭
//                    var mf = new OrderMFDTO
//                    {
//                        sale_date = item.FirstOrDefault().PS_DD.Value,
//                        order_number = item.FirstOrDefault().PS_NO,
//                        sales_customer = item.FirstOrDefault().CUS_NO,
//                        department = item.FirstOrDefault().DEP,
//                        tax_category = item.FirstOrDefault().TAX_ID,
//                        set_up_account = item.FirstOrDefault().ZHANG_ID,
//                        customer_order = item.FirstOrDefault().OS_NO,
//                        remark = item.FirstOrDefault().REM,
//                        subpoena_template = item.FirstOrDefault().VOH_ID,
//                        category = item.FirstOrDefault().INV_ID,
//                        invoice_number = item.FirstOrDefault().INV_NO,
//                        invoice_date = item.FirstOrDefault().INV_DD.GetValueOrDefault(),
//                        reporting_period = item.FirstOrDefault().INV_YM.GetValueOrDefault(),
//                        invoice_department = item.FirstOrDefault().DEP1,
//                        buyer_unified_number = item.FirstOrDefault().UNI_NO_BUY,
//                        buyer = item.FirstOrDefault().TITLE_BUY,
//                        //buyer_invoice_address = item.FirstOrDefault().InvoiceAdr,
//                        unified_business_number = item.FirstOrDefault().UNI_NO_PAY,
//                        seller = item.FirstOrDefault().TITLE_PAY,
//                        sales_amount = item.FirstOrDefault().AMT1.GetValueOrDefault(),
//                        tax_category1 = item.FirstOrDefault().TAX_ID1,
//                        tax_price = item.FirstOrDefault().TAX.GetValueOrDefault(),
//                        total_price = item.FirstOrDefault().AMT1.GetValueOrDefault() + item.FirstOrDefault().TAX.GetValueOrDefault(),
//                        tax_id2 = item.FirstOrDefault().TAX_ID2,
//                        checksum = item.FirstOrDefault().CHK_CODE

//                    };
//                    //var tf = new OrderTFDTO

//                    mf.TFs = item.Select(x => new OrderTFDTO
//                    {
//                        itm = x.ITM,
//                        order_number = x.OS_NO,
//                        product_name = x.PRD_NAME,
//                        storehouse = x.WH,
//                        quantity = x.QTY.GetValueOrDefault(),
//                        unit_price = x.UP.GetValueOrDefault(),
//                        price = x.AMT.GetValueOrDefault(),
//                        unit = x.UNIT,
//                        discount = x.DIS_CNT.GetValueOrDefault(),

//                    }).ToList();

//                    //表身
//                    Datas.Add(mf);
//                    //result.Datas.Add(mf);
//                }
//                //Datas = Datas.ToList();
//                //取得發票冊序 ******************** 費時 *******************
//                foreach (var item in Datas)
//                {
//                    if (item.invoice_number != null && item.invoice_number.Length == 10)
//                    {
//                        var n1 = item.invoice_number.Substring(0, 2);
//                        var n2 = Int32.Parse(item.invoice_number.Substring(2, 8));
//                        item.book_order = dbINV_ID.ToList().Where(x => x.TRACK_ID == n1 && Int32.Parse(x.F_SEQ_NO) <= n2 && Int32.Parse(x.E_SEQ_NO) >= n2).Select(x => x.INV_BOOK).FirstOrDefault();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                //exceptionDTO = new MessageInfo(false, $"");
//                //exceptionDTO.E_StatusCode = E_StatusCode.失敗;
//                //exceptionDTO.Message = $"【失敗】內部錯誤。";
//                //exceptionDTO.Message_Exception = $"【InnerException】{Exception_Service.ToInnerExceptionMessage(ex)}";
//                var m = ex.Message;
//                result.IsSuccess = false;
//                result.Title = "【新增失敗】";
//                result.Message = $"內部錯誤。";
//                result.Datas = null;
//                return result;
//            }
//            #endregion

//            #region == 查詢值 ==
//            var pageDTO = new Pageing_DTO
//            {
//                IsEnable = input.PageNumber != 0 ? true : false,
//                PageNumber = input.PageNumber ?? 1,
//                PageSize = input.PageSize ?? 10,
//                // = Datas.Count,
//            };


//            #endregion

//            #region == 分頁處理 ==
//            if (pageDTO == null || pageDTO.IsEnable == false) // 不分頁
//            {
//                pageDTO.TotalCount = Datas.Count();
//            }
//            else // 分頁處理
//            {
//                //input.IdSelected = null; // 避免要分頁的時候處理到(補已選資料)
//                pageDTO.TotalCount = Datas.Count();
//                Datas = Datas.OrderBy(o => o.invoice_date).ThenBy(t => t.order_number).Skip((pageDTO.PageNumber - 1) * pageDTO.PageSize).Take(pageDTO.PageSize).ToList();
//            }
//            #endregion

//            result.PageDTO = pageDTO;
//            result.IsSuccess = true;
//            result.Message = $"【成功】取得銷貨單。";
//            result.Datas = Datas;
//            return result;
//        }


//        public OrderResult CreateSalesOrder(SalesOrderDTO input)
//        {
//            var result = new OrderResult();
//            var today = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString());

//            //var addMF_PSSs_Final = new List<MF_PSS>();
//            //var addTF_PSSs_Final = new List<TF_PSS>();
//            //var addMF_ARPs_Final = new List<MF_ARP>();

//            //foreach (var input in inputs)
//            {
//                var psdd = new DateTime(input.sales_date.Year, input.sales_date.Month, input.sales_date.Day);
//                var paydd = psdd;
//                var chkdd = psdd;
//                if (paydd.Day > 1)
//                {
//                    paydd = paydd.AddMonths(1);
//                    paydd = new DateTime(paydd.Year, paydd.Month, 1);
//                    chkdd = chkdd.AddMonths(2);
//                    chkdd = new DateTime(chkdd.Year, chkdd.Month, 1);
//                }
//                else
//                {
//                    paydd = new DateTime(paydd.Year, paydd.Month, 1);
//                    chkdd = chkdd.AddMonths(1);
//                    chkdd = new DateTime(chkdd.Year, chkdd.Month, 1);
//                }
//                var payrem = $"結帳期:{paydd.ToString("yyyy-MM-dd")}; 票據到期日:{chkdd.ToString("yyyy-MM-dd")}";
//                //var psno = _MF_PSS_Service_Erp.GetSalesOrderNo(input.sales_date);
//                #region == 銷貨單-表頭 [MF_PSS] ==
//                var addMF_PSS = new MF_PSS
//                {
//                    PS_ID = "SA", //銷貨識別碼                    
//                    PS_NO = _MF_PSS_Service_Erp.GetPSNo(input.sales_date), // input.sales_number, //銷貨單號(ERP)  GetPS_NO()                  
//                    PS_DD = psdd, //單據日期
//                    PAY_DD = paydd, //付款日期 
//                    CHK_DD = chkdd, // 票據日期
//                    CUS_NO = input.customer_id, //客戶代號
//                    VOH_ID = "02",
//                    DEP = input.department_id, //部門代號
//                    INV_NO = input.invoice_number,  //發票號碼

//                    TAX_ID = "2", //扣稅類別
//                    SEND_MTH = "1", //交貨方式
//                    ZHANG_ID = "1", //立帳方式
//                    EXC_RTO = 1, //匯率
//                    ARP_NO = _MF_ARP_Service_Erp.GetARPNo(input.sales_date), //立帳單號(ERP) GetARPNO()
//                    REM = "TK_APP", //備註
//                    PAY_MTH = "1", //交易方式
//                    PAY_DAYS = 1, //付款天數
//                    CHK_DAYS = 30, //票據天數
//                    INT_DAYS = 30, //間隔天數
//                    PAY_REM = payrem, //交易備註
//                    USR = "1110601", //輸入員
//                    CHK_MAN = "1110601", //審核人
//                    PRT_SW = "N", //列印註記
//                    CLS_DATE = psdd, //終審日期
//                    CUS_OS_NO = input.customer_order, //銷貨單號(前端)
//                    CK_CLS_ID = "F", //出庫結案標誌
//                    LZ_CLS_ID = "F", //立帳結案標誌
//                    YD_ID = "T", //出庫補開發票標記
//                    KP_ID = "T",
//                    SYS_DATE = today, //輸單日期
//                    //ARP_DD = "2023/01/01", //立帳日期
//                    INV_BIL_ID = "SA",
//                    //INV_BIL_NO = PS_NO,
//                    AMT_POI = 0,
//                    APP_NAME_DATA = "Sunlike2004",

//                    SAL_NO = "",
//                    ARP_DD = psdd,
//                    //OS_ID = "", //來源單據識別碼
//                    //OS_NO = "", //來源單據單號
//                    //SAL_NO = posInfo.MF_POS.SAL_NO, //員工代號
//                    //SEND_WH = "", //交貨倉(分倉代號)
//                    //ADR = "", //地址 //[待補]
//                    //MODIFY_DD = today, //最近修改日期
//                    //MODIFY_MAN = "TK_APP", //最近修改人

//                };
//                #endregion

//                #region == 銷貨單-表身 [TF_PSS] ==
//                var addTF_PSSs = new List<TF_PSS>();
//                var indexTF = 1;
//                foreach (var pssTF in input.TFs)
//                {
//                    var addTF_PSS = new TF_PSS
//                    {
//                        ITM = indexTF, //流水項次
//                        EST_ITM = indexTF, //追蹤已交數量項次(流水項次)
//                        //PRE_ITM = subItem.ITM_Key, //追蹤退回數量項次(唯一項次)
//                        PRE_ITM = indexTF, //追蹤退回數量項次(唯一項次)
//                        OTH_ITM = indexTF, //出入庫單項次(流水項次)

//                        PS_ID = addMF_PSS.PS_ID, //單據識別碼
//                        PS_NO = addMF_PSS.PS_NO, //單號
//                        PS_DD = addMF_PSS.PS_DD, //日期                        
//                        //EST_DD = pssTF.est_dd, //預交日
//                        //OS_ID = pssTF.OS_ID, //來源單據識別碼
//                        //OS_NO = pssTF.OS_NO, //來源單據單號
//                        //WH = subItem.WH_No, //倉庫代號
//                        WH = pssTF.storehouse, //倉庫代號
//                        //BAT_NO = pssTF.BAT_NO, //批號
//                        PRD_NO = pssTF.product_id, //貨品代號
//                        PRD_NAME = pssTF.product_name, //貨品名稱
//                        //PRD_MARK = pssTF.PRD_MARK,
//                        UNIT = pssTF.unit, //數量單位
//                        QTY = pssTF.quantity, //數量

//                        CSTN_SAL = 0, //成本
//                        UP = pssTF.unit_price, //單價
//                        //QTY_PS = pssTF.QTY, //擷取受訂數量
//                        AMTN_NET = pssTF.price - pssTF.tax, //未稅本位幣
//                        AMT = pssTF.price, //外幣金額                        
//                        TAX = pssTF.tax, //稅額
//                        AMTN_EP = 0,
//                        TAX_RTO = 5, //稅率
//                        CST_STD = 0,
//                        CUS_OS_NO = addMF_PSS.CUS_OS_NO,
//                        FREE_ID_DEF = "F", //是否定價政策搭贈品
//                        UP_STD = 0,
//                        CSTN_SAL_IFRS = 0,
//                        AMTN_EP_IFRS = 0,
//                        UP_MAIN_IFRS = 0,
//                        //SC_DD = null,   //生產日期

//                        //SEND_WH = "02", //送貨倉
//                        DIS_CNT = pssTF.discount,

//                        //REM = "TK_APP", //備註
//                    };
//                    addTF_PSSs.Add(addTF_PSS);
//                    indexTF++;
//                }
//                #endregion

//                #region == 立帳單 [MF_ARP] ==
//                var addMF_ARP = new MF_ARP
//                {
//                    ARP_ID = "1", //收付註記
//                    OPN_ID = "2", //開帳標誌
//                    ARP_NO = addMF_PSS.ARP_NO, //立帳單號
//                                               //BIL_ITM = addTF_PSS.KEY_ITM, //單據表身項次
//                    BIL_NO = addMF_PSS.PS_NO, //來源單號
//                    BIL_DD = addMF_PSS.PS_DD, //交易日
//                    CUS_NO = addMF_PSS.CUS_NO, //客戶代號
//                    PAY_DD = paydd, //結帳期
//                    INV_NO = addMF_PSS.INV_NO, //發票號碼
//                    DEP = addMF_PSS.DEP, //部門代號
//                    AMTN = addTF_PSSs.Sum(sum => sum.AMT), //應收本位幣金額
//                    AMTN_NET = addTF_PSSs.Sum(sum => sum.AMTN_NET), //未稅本位幣
//                    TAX = addTF_PSSs.Sum(sum => sum.TAX), //稅額
//                    AMT = 0,
//                    //AMTN_RCV = addTF_PSSs.Sum(sum => sum.AMT),
//                    EXC_RTO = 1, //匯率
//                    USR_NO = addMF_PSS.USR, //操作員
//                    //CLOSE_ID = "T", //結案
//                    REM = addMF_PSS.REM, //摘要 REM = addMF_PSS.REM, //摘要
//                    BIL_ID = addMF_PSS.PS_ID, //來源單識別碼
//                    PAY_REM = payrem, //交易方式
//                    CHK_DD = chkdd, //票據到期日
//                    //RP_DD = psdd,
//                    SYS_DATE = today, //輸單日期
//                    ZHANG_ID = "1",
//                    BAT_NO = "",
//                    SAL_NO = "",
//                    CUR_ID = "",
//                    BIL_TYPE = "",
//                    CONTRACT = ""
//                    //BIL_TYPE = addMF_PSS.BIL_TYPE, //單據類別
//                    //CHECK_MAN = "", //審核人  
//                };
//                #endregion

//                #region == 發票 [INV_NO] ==
//                var addINV_NO = new INV_NO
//                {
//                    INV_NO1 = addMF_PSS.INV_NO,
//                    BIL_ID = addMF_PSS.PS_ID,
//                    BIL_NO = addMF_PSS.PS_NO,
//                    INV_ID = input.category,
//                    INV_DD = input.invoice_date,
//                    INV_YM = input.reporting_period,
//                    CUS_NO = addMF_PSS.CUS_NO,
//                    UNI_NO_BUY = input.buyer_unified_number,
//                    TITLE_BUY = input.buyer,
//                    UNI_NO_PAY = input.unified_business_number,
//                    TITLE_PAY = input.seller,
//                    AMT = input.sales_amount,
//                    TAX = input.tax_price,
//                    TAX_ID1 = input.tax_category,
//                    TAX_ID2 = "1",
//                    CHK_CODE = input.checksum,
//                    METH_ID = "01",
//                    USR = "1110601",
//                    DEP = addMF_PSS.DEP,
//                    SYS_DATE = today,
//                    OUT_MTH = "0",
//                    B2BTYPE = "F",
//                    SDCHK = "F",
//                    DEP_COMP = "########",
//                    JORCV_TYPE = "3",
//                };
//                #endregion

//                #region == [DB]寫入 ==
//                try
//                {
//                    //MF_PSS
//                    try
//                    {
//                        _MF_PSS_Repository.Create(addMF_PSS);
//                    }
//                    catch (Exception ex)
//                    {
//                        //刪除新增
//                        DelNew(addMF_PSS.PS_NO, addMF_PSS.INV_NO, addMF_ARP.ARP_NO);

//                        result.IsSuccess = false;
//                        result.E_StatusCode = E_StatusCode.資料存取異常;
//                        result.Message = $"【失敗】資料庫存取異常-訂單轉銷貨[{addMF_PSS.PS_NO}]-寫入MF_PSS異常。";
//                        result.Message_Exception = ex.Message;
//                        result.Message_Other = ex.InnerException.ToString();
//                        return result;
//                    }
//                    //TF_PSS
//                    try
//                    {
//                        foreach (var tf in addTF_PSSs)
//                        {
//                            _TF_PSS_Repository.Create(tf);
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        //刪除新增
//                        DelNew(addMF_PSS.PS_NO, addMF_PSS.INV_NO, addMF_ARP.ARP_NO);

//                        result.IsSuccess = false;
//                        result.E_StatusCode = E_StatusCode.資料存取異常;
//                        result.Message = $"【失敗】資料庫存取異常-訂單轉銷貨[{addMF_PSS.PS_NO}]-寫入TF_PSS異常。";
//                        result.Message_Exception = ex.Message;
//                        result.Message_Other = ex.InnerException.ToString();
//                        return result;
//                    }

//                    //MF_ARP
//                    try
//                    {
//                        _MF_ARP_Repository.Create(addMF_ARP);
//                    }
//                    catch (Exception ex)
//                    {
//                        //刪除新增
//                        DelNew(addMF_PSS.PS_NO, addMF_PSS.INV_NO, addMF_ARP.ARP_NO);

//                        result.IsSuccess = false;
//                        result.E_StatusCode = E_StatusCode.資料存取異常;
//                        result.Message = $"【失敗】資料庫存取異常-訂單轉銷貨[{addMF_PSS.PS_NO}]-寫入MF_ARP異常。";
//                        result.Message_Exception = ex.Message;
//                        result.Message_Other = ex.InnerException.ToString();
//                    }
//                    try
//                    {
//                        _INV_NO_Repository.Create(addINV_NO);
//                    }
//                    catch (Exception ex)
//                    {
//                        //刪除新增
//                        DelNew(addMF_PSS.PS_NO, addMF_PSS.INV_NO, addMF_ARP.ARP_NO);

//                        result.IsSuccess = false;
//                        result.E_StatusCode = E_StatusCode.資料存取異常;
//                        result.Message = $"【失敗】資料庫存取異常-訂單轉銷貨[{addMF_PSS.PS_NO}]-寫入INV_NO異常。";
//                        result.Message_Exception = ex.Message;
//                        result.Message_Other = ex.InnerException.ToString();
//                    }


//                    result.IsSuccess = true;
//                    result.E_StatusCode = E_StatusCode.成功;
//                    result.Message = $"【成功】銷貨單[{addMF_PSS.PS_NO}]-{input.buyer}。";
//                    //result.Nos.AddRange(addMF_PSSs_Final.Select(x => x.PS_NO));
//                }
//                catch (Exception ex)
//                {
//                    result.IsSuccess = false;
//                    result.E_StatusCode = E_StatusCode.資料存取異常;
//                    result.Message = $"【失敗】資料庫存取異常-訂單轉銷貨[{addMF_PSS.PS_NO}]-{input.buyer}。";
//                    result.Message_Exception = ex.Message;
//                    //result.Message_Other = ex.InnerException.ToString();
//                }
//                #endregion                
//            }
//            return result;
//        }

//        //刪除新增
//        public void DelNew(string psno, string invno, string arpno)
//        {
//            //del MF_PSS
//            if (_TF_PSS_Service_Erp.Check_IsExist_PS_NO(psno))
//            {
//                var dels = _TF_PSS_Repository.GetAlls(x => x.PS_NO == psno);
//                foreach (var del in dels)
//                {
//                    try
//                    {
//                        var db1 = new DB_T020Context();
//                        db1.TF_PSS.Remove(del);
//                        db1.SaveChanges();
//                    }
//                    catch { }
//                }

//            }
//            //del MF_PSS
//            if (_MF_PSS_Service_Erp.Check_IsExist_PS_NO(psno))
//            {
//                var del = _MF_PSS_Repository.Get(x => x.PS_NO == psno);
//                try
//                {
//                    var db1 = new DB_T020Context();
//                    db1.MF_PSS.Remove(del);
//                    db1.SaveChanges();
//                }
//                catch { }
//            }
//            //del INV_NO
//            if (_INV_NO_Service_Erp.Check_IsExist_INV_NO(invno))
//            {
//                var del = _INV_NO_Repository.Get(x => x.INV_NO1 == invno);
//                try
//                {
//                    var db1 = new DB_T020Context();
//                    db1.INV_NO.Remove(del);
//                    db1.SaveChanges();
//                }
//                catch { }
//            }
//            //del MF_ARP
//            if (_MF_ARP_Service_Erp.Check_IsExist_ARP_NO(arpno))
//            {
//                var del = _MF_ARP_Repository.Get(x => x.ARP_NO == arpno);
//                try
//                {
//                    var db1 = new DB_T020Context();
//                    db1.MF_ARP.Remove(del);
//                    db1.SaveChanges();
//                }
//                catch { }
//            }
//        }

//        public List<OrderResult> CreateSalesOrders(List<SalesOrderDTO> inputs)
//        {
//            var results = new List<OrderResult>();
//            var result = new OrderResult();
//            var today = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString());

//            var addMF_PSSs_Final = new List<MF_PSS>();
//            var addTF_PSSs_Final = new List<TF_PSS>();
//            var addMF_ARPs_Final = new List<MF_ARP>();

//            foreach (var input in inputs)
//            {
//                var psdd = new DateTime(input.sales_date.Year, input.sales_date.Month, input.sales_date.Day);
//                var paydd = psdd;
//                var chkdd = psdd;
//                if (paydd.Day > 1)
//                {
//                    paydd = paydd.AddMonths(1);
//                    paydd = new DateTime(paydd.Year, paydd.Month, 1);
//                    chkdd = chkdd.AddMonths(2);
//                    chkdd = new DateTime(chkdd.Year, chkdd.Month, 1);
//                }
//                else
//                {
//                    paydd = new DateTime(paydd.Year, paydd.Month, 1);
//                    chkdd = chkdd.AddMonths(1);
//                    chkdd = new DateTime(chkdd.Year, chkdd.Month, 1);
//                }
//                var payrem = $"結帳期:{paydd.ToString("yyyy-MM-dd")}; 票據到期日:{chkdd.ToString("yyyy-MM-dd")}";
//                //var psno = _MF_PSS_Service_Erp.GetSalesOrderNo(input.sales_date);
//                #region == 銷貨單-表頭 [MF_PSS] ==
//                var addMF_PSS = new MF_PSS
//                {
//                    PS_ID = "SA", //銷貨識別碼                    
//                    PS_NO = _MF_PSS_Service_Erp.GetPSNo(input.sales_date), // input.sales_number, //銷貨單號(ERP)  GetPS_NO()                  
//                    PS_DD = psdd, //單據日期
//                    PAY_DD = paydd, //付款日期 
//                    CHK_DD = chkdd, // 票據日期
//                    CUS_NO = input.customer_id, //客戶代號
//                    VOH_ID = "02",
//                    DEP = input.department_id, //部門代號
//                    INV_NO = input.invoice_number,  //發票號碼
//                    TAX_ID = "2", //扣稅類別
//                    SEND_MTH = "1", //交貨方式
//                    ZHANG_ID = "1", //立帳方式
//                    EXC_RTO = 1, //匯率
//                    ARP_NO = _MF_ARP_Service_Erp.GetARPNo(input.sales_date), //立帳單號(ERP) GetARPNO()
//                    REM = "TK_APP", //備註
//                    PAY_MTH = "1", //交易方式
//                    PAY_DAYS = 1, //付款天數
//                    CHK_DAYS = 30, //票據天數
//                    INT_DAYS = 30, //間隔天數
//                    PAY_REM = payrem, //交易備註
//                    USR = "1110601", //輸入員
//                    CHK_MAN = "1110601", //審核人
//                    PRT_SW = "N", //列印註記
//                    CLS_DATE = psdd, //終審日期
//                    CUS_OS_NO = input.customer_order, //銷貨單號(前端)
//                    CK_CLS_ID = "F", //出庫結案標誌
//                    LZ_CLS_ID = "F", //立帳結案標誌
//                    YD_ID = "T", //出庫補開發票標記
//                    KP_ID = "T",
//                    SYS_DATE = today, //輸單日期
//                    //ARP_DD = "2023/01/01", //立帳日期
//                    INV_BIL_ID = "SA",
//                    //INV_BIL_NO = PS_NO,
//                    AMT_POI = 0,
//                    APP_NAME_DATA = "Sunlike2004",

//                    SAL_NO = "",
//                    ARP_DD = psdd,
//                    //OS_ID = "", //來源單據識別碼
//                    //OS_NO = "", //來源單據單號
//                    //SAL_NO = posInfo.MF_POS.SAL_NO, //員工代號
//                    //SEND_WH = "", //交貨倉(分倉代號)
//                    //ADR = "", //地址 //[待補]
//                    //MODIFY_DD = today, //最近修改日期
//                    //MODIFY_MAN = "TK_APP", //最近修改人

//                };
//                #endregion

//                #region == 銷貨單-表身 [TF_PSS] ==
//                var addTF_PSSs = new List<TF_PSS>();
//                var indexTF = 1;
//                foreach (var pssTF in input.TFs)
//                {
//                    var addTF_PSS = new TF_PSS
//                    {
//                        ITM = indexTF, //流水項次
//                        EST_ITM = indexTF, //追蹤已交數量項次(流水項次)
//                        //                         //PRE_ITM = subItem.ITM_Key, //追蹤退回數量項次(唯一項次)
//                        PRE_ITM = indexTF, //追蹤退回數量項次(唯一項次)
//                        OTH_ITM = indexTF, //出入庫單項次(流水項次)

//                        PS_ID = addMF_PSS.PS_ID, //單據識別碼
//                        PS_NO = addMF_PSS.PS_NO, //單號
//                        PS_DD = addMF_PSS.PS_DD, //日期                        
//                                                 //EST_DD = pssTF.est_dd, //預交日
//                                                 //OS_ID = pssTF.OS_ID, //來源單據識別碼
//                                                 //OS_NO = pssTF.OS_NO, //來源單據單號
//                                                 //WH = subItem.WH_No, //倉庫代號
//                        WH = pssTF.storehouse, //倉庫代號
//                                               //BAT_NO = pssTF.BAT_NO, //批號
//                        PRD_NO = pssTF.product_id, //貨品代號
//                        PRD_NAME = pssTF.product_name, //貨品名稱
//                        //PRD_MARK = pssTF.PRD_MARK,
//                        UNIT = pssTF.unit, //數量單位
//                        QTY = pssTF.quantity, //數量

//                        CSTN_SAL = 0, //成本
//                        UP = pssTF.unit_price, //單價
//                        //QTY_PS = pssTF.QTY, //擷取受訂數量
//                        AMTN_NET = pssTF.price - pssTF.tax, //未稅本位幣
//                        AMT = pssTF.price, //外幣金額                        
//                        TAX = pssTF.tax, //稅額
//                        AMTN_EP = 0,
//                        TAX_RTO = 5, //稅率
//                        CST_STD = 0,
//                        CUS_OS_NO = addMF_PSS.CUS_OS_NO,
//                        FREE_ID_DEF = "F", //是否定價政策搭贈品
//                        UP_STD = 0,
//                        CSTN_SAL_IFRS = 0,
//                        AMTN_EP_IFRS = 0,
//                        UP_MAIN_IFRS = 0,
//                        //SC_DD = null,   //生產日期

//                        //SEND_WH = "02", //送貨倉
//                        DIS_CNT = pssTF.discount,

//                        //REM = "TK_APP", //備註
//                    };
//                    addTF_PSSs.Add(addTF_PSS);
//                    indexTF++;
//                }
//                #endregion

//                #region == 立帳單 [MF_ARP] ==
//                var addMF_ARP = new MF_ARP
//                {
//                    ARP_ID = "1", //收付註記
//                    OPN_ID = "2", //開帳標誌
//                    ARP_NO = addMF_PSS.ARP_NO, //立帳單號
//                                               //BIL_ITM = addTF_PSS.KEY_ITM, //單據表身項次
//                    BIL_NO = addMF_PSS.PS_NO, //來源單號
//                    BIL_DD = addMF_PSS.PS_DD, //交易日
//                    CUS_NO = addMF_PSS.CUS_NO, //客戶代號
//                    PAY_DD = paydd, //結帳期
//                    INV_NO = addMF_PSS.INV_NO, //發票號碼
//                    DEP = addMF_PSS.DEP, //部門代號
//                    AMTN = addTF_PSSs.Sum(sum => sum.AMT), //應收本位幣金額
//                    AMTN_NET = addTF_PSSs.Sum(sum => sum.AMTN_NET), //未稅本位幣
//                    TAX = addTF_PSSs.Sum(sum => sum.TAX), //稅額
//                    AMT = 0,
//                    //AMTN_RCV = addTF_PSSs.Sum(sum => sum.AMT),
//                    EXC_RTO = 1, //匯率
//                    USR_NO = addMF_PSS.USR, //操作員
//                    //CLOSE_ID = "T", //結案
//                    REM = addMF_PSS.REM, //摘要 REM = addMF_PSS.REM, //摘要
//                    BIL_ID = addMF_PSS.PS_ID, //來源單識別碼
//                    PAY_REM = payrem, //交易方式
//                    CHK_DD = chkdd, //票據到期日
//                    //RP_DD = psdd,
//                    SYS_DATE = today, //輸單日期
//                    ZHANG_ID = "1",
//                    BAT_NO = "",
//                    SAL_NO = "",
//                    CUR_ID = "",
//                    BIL_TYPE = "",
//                    CONTRACT = ""


//                    //BIL_TYPE = addMF_PSS.BIL_TYPE, //單據類別
//                    //CHECK_MAN = "", //審核人  
//                };
//                #endregion

//                #region == 發票 [INV_NO] ==
//                var addINV_NO = new INV_NO
//                {
//                    INV_NO1 = addMF_PSS.INV_NO,
//                    BIL_ID = addMF_PSS.PS_ID,
//                    BIL_NO = addMF_PSS.PS_NO,
//                    INV_ID = input.category,
//                    INV_DD = input.invoice_date,
//                    INV_YM = input.reporting_period,
//                    CUS_NO = addMF_PSS.CUS_NO,
//                    UNI_NO_BUY = input.buyer_unified_number,
//                    TITLE_BUY = input.buyer,
//                    UNI_NO_PAY = input.unified_business_number,
//                    TITLE_PAY = input.seller,
//                    AMT = input.sales_amount,
//                    TAX = input.tax_price,
//                    TAX_ID1 = input.tax_category,
//                    TAX_ID2 = "1",
//                    CHK_CODE = input.checksum,
//                    METH_ID = "01",
//                    USR = "1110601",
//                    DEP = addMF_PSS.DEP,
//                    SYS_DATE = today,
//                    OUT_MTH = "0",
//                    B2BTYPE = "F",
//                    SDCHK = "F",
//                    DEP_COMP = "########",
//                    JORCV_TYPE = "3",
//                };
//                #endregion

//                #region == [DB]寫入 ==
//                try
//                {
//                    _MF_PSS_Repository.Create(addMF_PSS);
//                    foreach (var tf in addTF_PSSs)
//                    {
//                        _TF_PSS_Repository.Create(tf);
//                    }
//                    _MF_ARP_Repository.Create(addMF_ARP);
//                    _INV_NO_Repository.Create(addINV_NO);
//                    result.IsSuccess = true;
//                    result.E_StatusCode = E_StatusCode.成功;
//                    result.Title = $"【新增成功】銷貨單[{addMF_PSS.PS_NO}]。";
//                    result.Message = $"銷貨單[{addMF_PSS.PS_NO}]-{input.buyer}。";
//                    results.Add(result);
//                    //result.Nos.AddRange(addMF_PSSs_Final.Select(x => x.PS_NO));
//                }
//                catch (Exception ex)
//                {
//                    result.IsSuccess = false;
//                    result.E_StatusCode = E_StatusCode.資料存取異常;
//                    result.Message = $"【失敗】資料庫存取異常-訂單轉銷貨[{addMF_PSS.PS_NO}]-{input.buyer}。";
//                    result.Message_Exception = ex.Message;
//                    results.Add(result);
//                    //#region == 還原資料 [因新增失敗，刪除本次新增相關資料] ==
//                    //if (!this.PSS_CreateError_DeleteDatas(pssNos, today))
//                    //{
//                    //    resultData.Message += "還原新增的資料時發生錯誤。";
//                    //}
//                    //#endregion

//                    //result.IsSuccess = false;
//                    ////result.E_StatusCode = E_StatusCode.失敗;
//                    //result.Result.Add(resultData);
//                    //continue;
//                }
//                #endregion                
//            }
//            return results;
//        }
//    }
//}
