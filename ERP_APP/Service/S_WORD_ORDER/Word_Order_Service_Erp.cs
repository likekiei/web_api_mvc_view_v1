using ERP_APP.Service.S_WORD_ORDER;
using ERP_EF.Models;
using ERP_EF.Repository;
using Main_Common.Enum;
using Main_Common.Enum.E_StatusType;
using Main_Common.ExtensionMethod;
using Main_Common.Model.Account;
using Main_Common.Model.Data;
using Main_Common.Model.DTO.Cust;
using Main_Common.Model.DTO.MO_WorkOrder;
using Main_Common.Model.DTO.Order;
using Main_Common.Model.ERP;
using Main_Common.Model.Main;
using Main_Common.Model.Result;
using Main_Common.Model.ResultApi;
using Main_Common.Model.ResultApi.Order;
using Main_Common.Model.Search;
using Main_Common.Mothod.Message;
using Main_Common.Tool;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;

namespace ERP_APP.Service.S_WORD_ORDER
{
    /// <summary>
    /// 製令單相關
    /// </summary>
    public class Word_Order_Service_Erp
    {
        #region == 【DI注入用宣告】 ==

        /// <summary>
        /// 【DTO】主系統資料
        /// </summary>
        public readonly MainSystem_DTO _MainSystem_DTO;

        /// <summary>
        /// 【Tool】訊息處理
        /// </summary>
        public readonly Message_Tool _Message_Tool;

        /// <summary>
        /// ERP DB
        /// </summary>
        private readonly DB_T014Context _DB_T014Context;

        /// <summary>
        /// 製令單表頭檔
        /// </summary>
        private readonly C_ERP_Repository<MF_MO> _MF_MO_Repository;

        /// <summary>
        /// 製令單表身檔
        /// </summary>
        private readonly C_ERP_Repository<TF_MO> _TF_MO_Repository;

        /// <summary>
        /// 商品物物料表
        /// </summary>
        private readonly C_ERP_Repository<MF_BOM> _MF_BOM_Repository;

        /// <summary>
        /// 【ERP】製令單相關
        /// </summary>
        public readonly Word_Order_Service_Erp _Word_Order_Service_Erp;

        #endregion

        #region == 【全域宣告】 ==
        /// <summary>
        /// 【DTO】全部資料的DTO
        /// </summary>
        public readonly AllDataDTO _AllDataDTO = new AllDataDTO();
        #endregion


        #region == 建構 ==
        /// <summary>
        /// 建構
        /// </summary>
        /// <param name="_DB_T014Context">ERP資料庫</param>
        /// <param name="mainSystem_DTO">主系統資料</param>
        /// <param name="logService_Main">Log相關</param>
        /// <param name="messageTool">訊息處理</param>
        public Word_Order_Service_Erp(
            DB_T014Context db,
            MainSystem_DTO mainSystem_DTO,
            //LogService_Main logService_Main,

            Message_Tool messageTool)
        {

            this._MainSystem_DTO = mainSystem_DTO;
            //this._DB_T020Context = _DB_T020Context;       
            //this._LogService_Main = logService_Main;
            _MF_MO_Repository = new C_ERP_Repository<MF_MO>(db);
            _TF_MO_Repository = new C_ERP_Repository<TF_MO>(db);
            _MF_BOM_Repository = new C_ERP_Repository<MF_BOM>(db);

            this._Message_Tool = messageTool;
        }
        #endregion

        //--【方法】=================================================================================
        #region == 【全域變數】DB、Service ==       
        #endregion

        #region == 【全域變數】參數、屬性 ==
        private UserSession_Model _UserSession_Model = null; //登入者資訊
        private string Com_MainKey = null; //共用主要Key(使用前請先重置)
        private string LogErrorMsg = null; //共用Log錯誤訊息(使用前請先重置)
        private string ResultMsg_Finally = null; //共用最終回傳訊息(使用前請先重置)
        private bool Com_Check = false; //共用檢查結果(使用前請先重置)
        private bool Com_IsExist = false; //共用是否存在(使用前請先重置)
        private bool Com_Result = false; //共用處理結果(使用前請先重置)
        private ResultOutput Com_Result_DTO = null; //共用結果(使用前請先重置)
        private List<string> Com_OtherMsg_List = null; //共用其他訊息清單(使用前請先重置)
        private List<string> Com_TextList = null; //共用文字清單(使用前請先重置)
        private List<string> Com_Split = null; //共用Split結果(使用前請先重置)
        private List<SelectItemDTO> DropList_DTO = null; //共用下拉清單(使用前請先重置)

        //private List<Product_DTO> _Product_DTOs = new List<Product_DTO>();
        //private List<Order_DTO> _Order_DTOs = new List<Order_DTO>();
        #endregion



        //--【方法】=================================================================================

        //測試第一版
        /// <summary>
        /// 製令單 
        /// </summary>
        /// <returns></returns>
        //public OrderResult GetWorkOrders(MoWorkOrder_Filter input)
        //{
        //    var result = new OrderResult();
        //    var Datas = new List<MfMoWorkOrder_DTO>();
        //    var check = true;
        //    MessageInfo exceptionDTO = null; //用來紀錄錯誤訊息

        //    #region == DB資料 ==
        //    IQueryable<MF_MO> dbMF_MO = null;
        //    IQueryable<TF_MO> dbTF_MO = null;
        //    #endregion

        //    try {

        //        dbMF_MO = _MF_MO_Repository.GetAll();
        //        dbTF_MO = _TF_MO_Repository.GetAll();

        //        //製令單表頭查詢期間
        //        if (input.Query_Date_STA != null && input.Query_Date_END != null)
        //        {
        //            //製領單表頭
        //            dbMF_MO = dbMF_MO.Where(x => x.MO_DD >= input.Query_Date_STA && x.MO_DD <= input.Query_Date_END);

        //            //取製領單表頭依查詢日期對應的表身 資料
        //            //dbTF_MO = dbTF_MO.Where(x => dbMF_MO.Any(z => z.MO_NO == x.MO_NO));

        //            //查詢期間無單號處理
        //            if (dbMF_MO.Count() == 0)
        //            {
        //                //result.PageDTO = pageDTO;
        //                result.IsSuccess = true;
        //                result.Message = $"查無 {input.Query_Date_STA}-{input.Query_Date_END} 製令單。";
        //                result.Datas = Datas;
        //                return result;
        //            }     
        //        }


        //        //#region == Join製令單表頭和表身資料 ==            

        //        var dbData = dbMF_MO.Join(dbTF_MO, a => a.MO_NO, b => b.MO_NO, (a, b) => new
        //        {
        //            MO_NO = a.MO_NO,
        //            MO_DD = a.MO_DD,
        //            MRP_NO = a.MRP_NO,
        //            ID_NO = a.ID_NO,
        //            DEP = a.DEP,
        //            QTY = a.QTY,
        //            UNIT = a.UNIT,
        //            WH = a.WH,
        //            CUS_NO = a.CUS_NO,
        //            CUS_OS_NO = a.CUS_OS_NO,
        //            STA_DD = a.STA_DD,
        //            END_DD = a.END_DD,
        //            NEED_DD = a.NEED_DD,
        //            //SPC =??
        //            REM = a.REM,
        //            CF_ID = a.CF_ID,
        //            ZT_ID = a.ZT_ID,
        //            CLOSE_ID = a.CLOSE_ID,
        //            ML_BY_MM = a.ML_BY_MM,
        //            PRD_NO = b.PRD_NO,
        //            PRD_NAME = b.PRD_NAME,
        //            TF_WH = b.WH,
        //            TF_UNIT = b.UNIT,
        //            QTY_RSV = b.QTY_RSV,
        //            QTY_STD = b.QTY_STD,
        //            EST_ITM = b.EST_ITM,


        //        }).GroupBy(g => new { g.MO_NO });


        //        #region == 整理資料 ==
        //        var dataMO = dbData.ToList();


        //        foreach (var item in dataMO)
        //        {
        //            //表頭
        //            var mf = new MfMoWorkOrder_DTO
        //            {
        //                mo_no = item.FirstOrDefault().MO_NO, // 製令單號
        //                mo_dd = item.FirstOrDefault().MO_DD.GetValueOrDefault(), // 製單日期
        //                mrp_no = item.FirstOrDefault().MRP_NO.Trim(),// 生產成品
        //                id_no = item.FirstOrDefault().ID_NO, // 配方
        //                dep = item.FirstOrDefault().DEP, // 製造部門
        //                qty = item.FirstOrDefault().QTY.GetValueOrDefault(), // 數量
        //                unit = item.FirstOrDefault().UNIT, // 製造單位
        //                wh = item.FirstOrDefault().WH, // 預入倉庫
        //                cus_no = item.FirstOrDefault().CUS_NO, // 需求客戶
        //                cus_os_no = item.FirstOrDefault().CUS_OS_NO, // 客戶訂單
        //                sta_dd = item.FirstOrDefault().STA_DD.GetValueOrDefault(), // 預開工日
        //                end_dd = item.FirstOrDefault().END_DD.GetValueOrDefault(), // 預完工日
        //                need_dd = item.FirstOrDefault().NEED_DD.GetValueOrDefault(), // 需求日期
        //                rem = item.FirstOrDefault().REM, // 備註
        //                spc = "", // 規格，目前erp這欄位是計算欄位值~所以要有規則(關聯的table)取資料!，關聯 商品物料表 的 貨品規格欄位 table:MF_BOM 關聯欄位:PRD_NO 取值欄位SPC 
        //            };

        //            //表身
        //            mf.TFs = item.Select(x => new TfMoWorkOrder_DTO
        //            {
        //              prd_no = x.PRD_NO, // 品號
        //                prd_name = x.PRD_NAME, // 品名
        //                tf_wh = x.TF_WH, // 倉庫
        //                tf_umit = x.TF_UNIT, // 單位
        //                qty_rsv = x.QTY_RSV.GetValueOrDefault(), // 應發數
        //                qty_std = x.QTY_STD.GetValueOrDefault(), // 單位標準用量
        //                est_itm = x.EST_ITM.GetValueOrDefault(), // 單據追蹤項次

        //            }).ToList();
        //            Datas.Add(mf);

        //        }

        //    }
        //    catch (Exception ex)
        //    {      
        //        var m = ex.Message;
        //        result.IsSuccess = false;
        //        result.Title = "【取得資料失敗】";
        //        result.Message = $"內部錯誤。";
        //        result.Datas = null;
        //        return result;
        //    }
        //    #endregion

        //    result.IsSuccess = true;
        //    result.Message = $"【成功】取得製令單。";
        //    result.Datas = Datas;
        //    return result;

        //}







        //第二版


        /// <summary>
        /// 製令單 
        /// </summary>
        /// <returns></returns>
        public OrderResult GetWorkOrders(MoWorkOrder_Filter input)
        {
            var result = new OrderResult();
            var Datas = new List<MfMoWorkOrder_DTO>();
            var check = true;
            MessageInfo exceptionDTO = null; //用來紀錄錯誤訊息

            #region == DB資料 ==
            IQueryable<MF_MO> dbMF_MO = null;
            IQueryable<TF_MO> dbTF_MO = null;
            IQueryable<MF_BOM> dbMF_BOM = null;
            #endregion

            try
            {

                dbMF_MO = _MF_MO_Repository.GetAll();
                dbTF_MO = _TF_MO_Repository.GetAll();
                dbMF_BOM = _MF_BOM_Repository.GetAll();

                //製令單表頭查詢期間
                if (input.Query_Date_STA != null && input.Query_Date_END != null)
                {
                    //製領單表頭
                    dbMF_MO = dbMF_MO.Where(x => x.MO_DD >= input.Query_Date_STA && x.MO_DD <= input.Query_Date_END);

                    //取製領單表頭依查詢日期對應的表身 資料
                    //dbTF_MO = dbTF_MO.Where(x => dbMF_MO.Any(z => z.MO_NO == x.MO_NO));

                    //查詢期間無單號處理
                    if (dbMF_MO.Count() == 0)
                    {
                        //result.PageDTO = pageDTO;
                        result.IsSuccess = true;
                        result.Message = $"查無 {input.Query_Date_STA}-{input.Query_Date_END} 製令單。";
                        result.Datas = Datas;
                        return result;
                    }
                }


                #region == Join製令單表頭和商品物料表和表身資料 ==

                var join_header = from a in dbMF_MO
                                  join b in dbMF_BOM on a.MRP_NO equals b.PRD_NO into ab
                                  from b in ab.DefaultIfEmpty()
                                  select new
                                  {
                                      MO_NO = a.MO_NO,
                                      MO_DD = a.MO_DD,
                                      MRP_NO = a.MRP_NO,
                                      ID_NO = a.ID_NO,
                                      DEP = a.DEP,
                                      QTY = a.QTY,
                                      UNIT = a.UNIT,
                                      WH = a.WH,
                                      CUS_NO = a.CUS_NO,
                                      CUS_OS_NO = a.CUS_OS_NO,
                                      STA_DD = a.STA_DD,
                                      END_DD = a.END_DD,
                                      NEED_DD = a.NEED_DD,
                                      SPC = b.SPC,
                                      REM = a.REM,
                                      CF_ID = a.CF_ID,
                                      ZT_ID = a.ZT_ID,
                                      CLOSE_ID = a.CLOSE_ID,
                                      ML_BY_MM = a.ML_BY_MM,
                                      BAT_NO  = a.BAT_NO,
                                  };


                // == Join製令單表頭和商品物料表join_header 和 表身資料dbTF_MO ==
                var dbData = join_header.Join(dbTF_MO, a => a.MO_NO, b => b.MO_NO, (a, b) => new
                {
                    MO_NO = a.MO_NO,
                    MO_DD = a.MO_DD,
                    MRP_NO = a.MRP_NO,
                    ID_NO = a.ID_NO,
                    DEP = a.DEP,
                    QTY = a.QTY,
                    UNIT = a.UNIT,
                    WH = a.WH,
                    CUS_NO = a.CUS_NO,
                    CUS_OS_NO = a.CUS_OS_NO,
                    STA_DD = a.STA_DD,
                    END_DD = a.END_DD,
                    NEED_DD = a.NEED_DD,
                    SPC = a.SPC,
                    REM = a.REM,
                    CF_ID = a.CF_ID,
                    ZT_ID = a.ZT_ID,
                    CLOSE_ID = a.CLOSE_ID,
                    ML_BY_MM = a.ML_BY_MM,
                    BAT_NO = a.BAT_NO,
                    PRD_NO = b.PRD_NO,
                    PRD_NAME = b.PRD_NAME,
                    TF_WH = b.WH,
                    TF_UNIT = b.UNIT,
                    QTY_RSV = b.QTY_RSV,
                    QTY_STD = b.QTY_STD,
                    EST_ITM = b.EST_ITM,


                }).GroupBy(g => new { g.MO_NO });

                #endregion


                #region == 整理資料 ==
                var dataMO = dbData.ToList();


                foreach (var item in dataMO)
                {
                    //表頭
                    var mf = new MfMoWorkOrder_DTO
                    {
                        mo_no = item.FirstOrDefault().MO_NO, // 製令單號
                        mo_dd = item.FirstOrDefault().MO_DD.GetValueOrDefault(), // 製單日期
                        mrp_no = item.FirstOrDefault().MRP_NO.Trim(),// 生產成品
                        id_no = item.FirstOrDefault().ID_NO, // 配方
                        dep = item.FirstOrDefault().DEP, // 製造部門
                        qty = item.FirstOrDefault().QTY.GetValueOrDefault(), // 數量
                        unit = item.FirstOrDefault().UNIT, // 製造單位
                        wh = item.FirstOrDefault().WH, // 預入倉庫
                        cus_no = item.FirstOrDefault().CUS_NO, // 需求客戶
                        cus_os_no = item.FirstOrDefault().CUS_OS_NO, // 客戶訂單
                        sta_dd = item.FirstOrDefault().STA_DD.GetValueOrDefault(), // 預開工日
                        end_dd = item.FirstOrDefault().END_DD.GetValueOrDefault(), // 預完工日
                        need_dd = item.FirstOrDefault().NEED_DD.GetValueOrDefault(), // 需求日期
                        rem = item.FirstOrDefault().REM, // 備註
                        spc = item.FirstOrDefault().SPC, // 規格，目前erp這欄位是計算欄位值~所以要有規則(關聯的table)取資料!，關聯 商品物料表 的 貨品規格欄位 table:MF_BOM 關聯欄位:PRD_NO 取值欄位SPC 
                        cf_id = item.FirstOrDefault().CF_ID, // 生產狀態/1.未發放F;2.發放生產T
                        zt_id = item.FirstOrDefault().ZT_ID, // 生產狀態/3.暫停生產
                        close_id = item.FirstOrDefault().CLOSE_ID, // 生產狀態/4.結束生產
                        ml_by_mm = item.FirstOrDefault().ML_BY_MM, // 倒沖領料
                        bat_no = item.FirstOrDefault().BAT_NO,//批號

                    };

                    //表身
                    mf.TFs = item.Select(x => new TfMoWorkOrder_DTO
                    {
                        est_itm = x.EST_ITM.GetValueOrDefault(), // 單據追蹤項次
                        prd_no = x.PRD_NO, // 品號
                        prd_name = x.PRD_NAME, // 品名
                        tf_wh = x.TF_WH, // 倉庫
                        tf_umit = x.TF_UNIT, // 單位
                        qty_rsv = x.QTY_RSV.GetValueOrDefault(), // 應發數
                        qty_std = x.QTY_STD.GetValueOrDefault(), // 單位標準用量                    
                        qty_rsv_lost = x.QTY_RSV.GetValueOrDefault(), // 應領量

                    }).ToList();
                    Datas.Add(mf);

                }

            }
            catch (Exception ex)
            {
                var m = ex.Message;
                result.IsSuccess = false;
                result.Title = "【取得資料失敗】";
                result.Message = $"內部錯誤。";
                result.Datas = null;
                return result;
            }
            #endregion

            result.IsSuccess = true;
            result.Message = $"【成功】取得製令單。";
            result.Datas = Datas;
            return result;

        }



    }
}
