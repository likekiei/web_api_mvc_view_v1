using ERP_APP.Service.S_Product_Inspection;
using ERP_EF.Models;
using ERP_EF.Repository;
using Main_Common.Enum;
using Main_Common.Enum.E_ProjectType;
using Main_Common.Enum.E_StatusType;
using Main_Common.ExtensionMethod;
using Main_Common.Model.Account;
using Main_Common.Model.Data;
using Main_Common.Model.DTO.Cust;
using Main_Common.Model.DTO.Order;
using Main_Common.Model.DTO.TI_ProductInspection;
using Main_Common.Model.ERP;
using Main_Common.Model.Main;
using Main_Common.Model.Result;
using Main_Common.Model.ResultApi;
using Main_Common.Model.ResultApi.Order;
using Main_Common.Model.ResultApi.ProductInspection;
using Main_Common.Model.Tool;
using Main_Common.Mothod.Message;
using Main_Common.Tool;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Schema;

namespace ERP_APP.Service.S_Product_Inspection
{
    /// <summary>
    /// 送檢單相關
    /// </summary>
    public class Product_Inspection_Serveice_Erp
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
        /// ERPDB
        /// </summary>
        private readonly DB_T014Context _DB_T014Context;
        /// <summary>
        /// 送檢單表頭檔
        /// </summary>
        private readonly C_ERP_Repository<MF_TI> _MF_TI_Repository;
        /// <summary>
        /// 送檢單表頭自定義欄位檔
        /// </summary>
        private readonly C_ERP_Repository<MF_TI_Z> _MF_TI_Z_Repository;
        /// <summary>
        /// 送檢單表身檔
        /// </summary>
        private readonly C_ERP_Repository<TF_TI> _TF_TI_Repository;
        /// <summary>
        /// 送檢單表身自定義欄位檔
        /// </summary>
        private readonly C_ERP_Repository<TF_TI_Z> _TF_TI_Z_Repository;

        /// <summary>
        /// 【ERP】送檢單相關
        /// </summary>
        public readonly MF_TI_Z_Service_Erp _MF_TI_Z_Service_Erp;


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
        public Product_Inspection_Serveice_Erp(
            DB_T014Context db,
            MainSystem_DTO mainSystem_DTO,
            //LogService_Main logService_Main,
            MF_TI_Z_Service_Erp _MF_TI_Z_Service_Erp,
         
            Message_Tool messageTool)
        {

            this._MainSystem_DTO = mainSystem_DTO;
            //this._LogService_Main = logService_Main;
            _MF_TI_Repository = new C_ERP_Repository<MF_TI>(db);
            _MF_TI_Z_Repository = new C_ERP_Repository<MF_TI_Z>(db);
            _TF_TI_Repository = new C_ERP_Repository<TF_TI>(db);
            _TF_TI_Z_Repository = new C_ERP_Repository<TF_TI_Z>(db);
           
            this._MF_TI_Z_Service_Erp = _MF_TI_Z_Service_Erp;
            this._Message_Tool = messageTool;
        }
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
        #endregion



        /// <summary>
        /// 建立送檢單單筆資料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ProductInspectionResult CreateProductInspection(MfTiProductInspection_DTO input)
        {
            var result = new ProductInspectionResult();
            var today01 = DateTime.Now;
            var T6No = this.GetNo_TI(today01); //送檢單號

            {
                var tidd = new DateTime(input.ti_dd.Year, input.ti_dd.Month, input.ti_dd.Day); //入庫日期
                var today = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString()); //系統輸單日期
                #region == 送檢單-表頭 [MF_TI] ==
                var addMF_TI = new MF_TI
                {
                    TI_NO = T6No, //入庫單號
                    TI_DD = tidd, //入庫日期
                    TI_ID = "T6",// 單據別 T6送檢單
                    BIL_ID ="MO", //單據識別
                    BIL_NO = input.os_no, //來源單號
                    CLOSE_ID = "F", //結案否
                    CHKTY_ID = "T", //檢驗否
                    CUS_NO = input.cus_no, //廠商
                    OS_NO = input.os_no, //轉入單號
                    BAT_NO = input.bat_no,//批號
                    SAL_NO = input.sal_no, //經辦人員
                    DEP = input.dep, //部門
                    USR = "ADMIN", //製單人
                    CHK_MAN = "ADMIN", //審核
                    PRT_SW = "N", //列印註記
                    CUS_OS_NO = input.cus_os_no,//客戶訂單
                    CLS_DATE = input.ti_dd, //終審日期
                    OS_ID = "MO", //關聯單據ID
                    REM = input.rem, //備註
                    SYS_DATE =today,//輸單日期
                };
                #endregion

                #region == 送檢單-表頭-自定義table [MF_TI_Z] ==
                var addMF_TI_Z = new MF_TI_Z
                {
                    TI_NO = addMF_TI.TI_NO, //入庫單號
                    TI_ID = "T6", // 單據別 T6送檢單
                    BBNUM = input.bbnum, //"日威送檢單號
                };
                #endregion

                #region == 送檢單-表身 [TF_TI] ==
                var addTF_TIs = new List<TF_TI>();
                var addTF_TI_Zs = new List<TF_TI_Z>();
                var indexTF = 1;
                foreach (var tiTF in input.TFs)
                {
                    var addTF_TI = new TF_TI
                    {
                        ITM = indexTF , //流水項次
                        EST_ITM = indexTF ,//追蹤已交數量項次(流水項次)
                        PRD_NO = tiTF.prd_no, //品號
                        PRD_NAME = tiTF.prd_name, //品名
                        WH = tiTF.wh, //倉庫
                        UNIT = tiTF.unit, //單位
                        QTY = tiTF.qty, //數量
                        BIL_ID = "MO", //單據識別
                        BIL_NO = input.os_no, //來源單號
                        ID_NO = tiTF.prd_no + "->", //配方號 應該是製令單的生產成品編號和品號一樣  再加 "->" 符號??
                        QTY_RK = tiTF.qty, //追蹤轉入單中的已入庫量
                        CK_ITM = indexTF, //追蹤出庫單項次
                        PRE_ITM = indexTF, //原入庫單項次
                        TI_ID = "T6", //單據別 T6送檢單
                        TI_NO = addMF_TI.TI_NO, //入庫單號
                        CHKTY_ID ="T", //檢驗否
                        QTY1 = 0, // 副單位數量
                       
                    };

                    //送檢單-表身-自定義欄位 [TF_TI_Z]
                    var addTF_TI_Z = new TF_TI_Z
                    {
                        TI_ID = "T6", //單據別 T6送檢單
                        TI_NO = addTF_TI.TI_NO, //入庫單號
                        ITM = addTF_TI.ITM, //流水項次
                        RRR2 = tiTF.rrr2, //自定義欄位[備註2]
                    };

                    addTF_TIs.Add(addTF_TI); //送檢單-表身
                    addTF_TI_Zs.Add(addTF_TI_Z); //送檢單-表身-自定義欄位
                    indexTF++;

                }
                #endregion

                #region == [DB]寫入 ==
                try {

                    //送檢單表頭 MF_TI
                    try {

                        _MF_TI_Repository.Create(addMF_TI);

                    } catch (Exception ex)
                    {

                        //刪除新增
                        DelNew(addMF_TI.TI_NO);

                        result.IsSuccess = false;
                        result.E_StatusCode = E_StatusCode.資料存取異常;
                        //result.Message = $"【失敗】資料庫存取異常-[{addMF_TI.TI_NO}]-寫入[送檢單]異常。";
                        result.Message = $"【失敗】資料庫存取異常-[日威送檢單號{addMF_TI_Z.BBNUM}]-寫入[送檢單]異常。";
                        result.Message_Exception = ex.Message;
                        result.Message_Other = ex.InnerException.ToString();
                        return result;

                    }
                    //送檢單表頭_自定義欄位 MF_TI_Z
                    try
                    {

                        _MF_TI_Z_Repository.Create(addMF_TI_Z);

                    }
                    catch (Exception ex)
                    {

                        //刪除新增
                        DelNew(addMF_TI.TI_NO);

                        result.IsSuccess = false;
                        result.E_StatusCode = E_StatusCode.資料存取異常;
                        //result.Message = $"【失敗】資料庫存取異常-[{addMF_TI.TI_NO}]-寫入[送檢單]異常。";
                        result.Message = $"【失敗】資料庫存取異常-[日威送檢單號{addMF_TI_Z.BBNUM}]-寫入[送檢單]異常。";
                        result.Message_Exception = ex.Message;
                        result.Message_Other = ex.InnerException.ToString();
                        return result;

                    }

                    //送檢單表身 TF_TI
                    try
                    {
                        foreach (var tfti in addTF_TIs) { 

                        _TF_TI_Repository.Create(tfti);

                        }
                    }
                    catch (Exception ex)
                    {

                        //刪除新增
                        DelNew(addMF_TI.TI_NO);

                        result.IsSuccess = false;
                        result.E_StatusCode = E_StatusCode.資料存取異常;
                        //result.Message = $"【失敗】資料庫存取異常-[{addMF_TI.TI_NO}]-寫入[送檢單]異常。";
                        result.Message = $"【失敗】資料庫存取異常-[日威送檢單號{addMF_TI_Z.BBNUM}]-寫入[送檢單]異常。";
                        result.Message_Exception = ex.Message;
                        result.Message_Other = ex.InnerException.ToString();
                        return result;

                    }


                    //送檢單表身_自定義欄位 TF_TI_Z
                    try
                    {
                        foreach (var tftiz in addTF_TI_Zs)
                        {

                            _TF_TI_Z_Repository.Create(tftiz);

                        }
                    }
                    catch (Exception ex)
                    {

                        //刪除新增
                        DelNew(addMF_TI.TI_NO);

                        result.IsSuccess = false;
                        result.E_StatusCode = E_StatusCode.資料存取異常;
                        //result.Message = $"【失敗】資料庫存取異常-[{addMF_TI.TI_NO}]-寫入[送檢單]異常。";
                        result.Message = $"【失敗】資料庫存取異常-[日威送檢單號{addMF_TI_Z.BBNUM}]-寫入[送檢單]異常。";
                        result.Message_Exception = ex.Message;
                        result.Message_Other = ex.InnerException.ToString();
                        return result;

                    }


                    result.IsSuccess = true;
                    result.E_StatusCode = E_StatusCode.成功;
                    result.Message = $"【成功】ERP SUNLIKE 送檢單[{addMF_TI.TI_NO}]，[日威送檢單號{addMF_TI_Z.BBNUM}]。";
                    

                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.E_StatusCode = E_StatusCode.資料存取異常;
                    result.Message = $"【失敗】資料庫存取異常-日威送檢單號[{addMF_TI_Z.BBNUM}]。";
                    result.Message_Exception = ex.Message;

                }
                #endregion

            }

            return result ;
        }



        /// <summary>
        ///【單筆】【送檢單】取得送檢單號
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GetNo_TI(DateTime date)
        {
            var presentNO = this.GetNos_MF_TI(date, 1).FirstOrDefault();
            return presentNO;
        }



        /// <summary>
        /// 【多筆】【送檢單】取得送檢單號
        /// </summary>
        /// <param name="date"></param>
        /// <param name="count">要取幾組代號</param>
        /// <returns></returns>
        public List<string> GetNos_MF_TI(DateTime date, int count)
        {
            var db = new DB_T014Context();
            //var dbContext = new ERP_DB();
            var result = new List<string>();
            string yy = (date.Year - 1911).ToString();  // 將西元年轉換為民國年
            yy = yy.Substring(yy.Length - 2);
            int MM = date.Month;
            int dd = date.Day;
            int snNo; // 流水號的部份
            string presentNO;
            var numberPrefix = "T6";
            var query = numberPrefix + "_" + yy + MM.ToString("00") + dd.ToString("00");
            var queryLength = query.Length;

            bool _Mf_Ti_No = db.MF_TI.Any(x => x.TI_NO.StartsWith(query));

            // 取下一個流水號
            if (_Mf_Ti_No) // 取最大的SN
            {      
                snNo = int.Parse(db.MF_TI.Where(x => x.TI_NO.StartsWith(query)).OrderByDescending(o => o.TI_NO).FirstOrDefault().TI_NO.Substring(queryLength)) + 1;
            }
            else // 默認SN
            {
                snNo = 1;
            }

            // 依需求數遞加流水號
            for (int i = 0; i < count; i++)
            {
                snNo += i == 0 ? 0 : 1; // 遞加i(第一圈不改變，故從0開始)
                presentNO = query + snNo.ToString("0000"); // 代號生成
                result.Add(presentNO);
            }


            return result;
        }


        /// <summary>
        /// 刪除新增
        /// </summary>
        /// <param name="tino"></param>
        public void DelNew(string tino)
        {
            //del MF_TI 送檢單表頭
            if (this.Check_IsExist_MF_TI(tino))
            {
                var dels = _MF_TI_Repository.GetAlls(x => x.TI_NO == tino);
                foreach (var del in dels)
                {
                    try
                    {
                        var db1 = new DB_T014Context();
                        db1.MF_TI.Remove(del);
                        db1.SaveChanges();
                    }
                    catch { }
                }

            }

            //del MF_TI_Z 送檢單表頭_自定義欄位檔
            if (_MF_TI_Z_Service_Erp.Check_IsExist_MF_TI_Z_TI_NO(tino))
            {
                var dels = _MF_TI_Z_Repository.GetAlls(x => x.TI_NO == tino);
                foreach (var del in dels)
                {
                    try
                    {
                        var db1 = new DB_T014Context();
                        db1.MF_TI_Z.Remove(del);
                        db1.SaveChanges();
                    }
                    catch { }
                }

            }

            //del TF_TI 送檢單表身
            if (this.Check_IsExist_TF_TI(tino))
            {
                var dels = _TF_TI_Repository.GetAlls(x => x.TI_NO == tino);
                foreach (var del in dels)
                {
                    try
                    {
                        var db1 = new DB_T014Context();
                        db1.TF_TI.Remove(del);
                        db1.SaveChanges();
                    }
                    catch { }
                }

            }

            //del TF_TI_Z 送檢單表身-自定義欄位
            if (this.Check_IsExist_TF_TI_Z(tino))
            {
                var dels = _TF_TI_Z_Repository.GetAlls(x => x.TI_NO == tino);
                foreach (var del in dels)
                {
                    try
                    {
                        var db1 = new DB_T014Context();
                        db1.TF_TI_Z.Remove(del);
                        db1.SaveChanges();
                    }
                    catch { }
                }

            }



        }



        #region == 檢查相關 ==
        /// <summary>
        /// 檢查送檢單表頭是否存在 [true存在]
        /// </summary>
        /// <param name="no">單號</param>
        /// <returns>[true：存在]</returns>
        public bool Check_IsExist_MF_TI(string no)
        {
            var check = _MF_TI_Repository.GetAlls(x => x.TI_NO == no).Any();
            return check;
        }

        /// <summary>
        /// 檢查送檢單表身是否存在 [true存在]
        /// </summary>
        /// <param name="no">單號</param>
        /// <returns>[true：存在]</returns>
        public bool Check_IsExist_TF_TI(string no)
        {
            var check = _TF_TI_Repository.GetAlls(x => x.TI_NO == no).Any();
            return check;
        }

        /// <summary>
        /// 檢查送檢單表身-自定義欄位是否存在 [true存在]
        /// </summary>
        /// <param name="no">單號</param>
        /// <returns>[true：存在]</returns>
        public bool Check_IsExist_TF_TI_Z(string no)
        {
            var check = _TF_TI_Z_Repository.GetAlls(x => x.TI_NO == no).Any();
            return check;
        }

        #endregion




    }
}
