using ERP_APP.Service.S_Product_Inspection;
using ERP_APP.Service.S_SPC_LST;
using ERP_EF.Models;
using ERP_EF.Repository;
using Main_Common.Enum.E_StatusType;
using Main_Common.Model.Account;
using Main_Common.Model.Data;
using Main_Common.Model.DTO.MO_WorkOrder;
using Main_Common.Model.DTO.TI_ProductInspection;
using Main_Common.Model.DTO.TY_ProductAcceptance;
using Main_Common.Model.Main;
using Main_Common.Model.Result;
using Main_Common.Model.ResultApi.ProductAcceptance;
using Main_Common.Model.ResultApi.ProductInspection;
using Main_Common.Tool;
using Microsoft.VisualBasic;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace ERP_APP.Service.S_MF_TY
{
    /// <summary>
    /// 生產繳庫驗收單相關
    /// </summary>
    public class MF_TY_Service_Erp
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
        /// 生產繳庫驗收單_表頭檔
        /// </summary>
        private readonly C_ERP_Repository<MF_TY> _MF_TY_Repository;
        /// <summary>
        /// 生產繳庫驗收單_表頭自定義欄位檔
        /// </summary>
        private readonly C_ERP_Repository<MF_TY_Z> _MF_TY_Z_Repository;
        /// <summary>
        /// 生產繳庫驗收單_表身檔
        /// </summary>
        private readonly C_ERP_Repository<TF_TY> _TF_TY_Repository;

        /// <summary>
        /// 送檢單_表頭自定義欄位檔 
        /// </summary>
        private readonly C_ERP_Repository<MF_TI_Z> _MF_TI_Z_Repository;

        /// <summary>
        /// 查(不合格)原因欄位檔
        /// </summary>
        private readonly C_ERP_Repository<SPC_LST> _SPC_LST_Repository;

        /// <summary>
        /// 【ERP】生產繳庫驗收單相關
        /// </summary>
        public readonly MF_TY_Z_Service_Erp _MF_TY_Z_Service_Erp;

        /// <summary>
        /// 【ERP】(不合格)原因單相關
        /// </summary>
        public readonly SPC_LST_Service_Erp _SPC_LST_Service_Erp;

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
        public MF_TY_Service_Erp(
            DB_T014Context db,
            MainSystem_DTO mainSystem_DTO,
            //LogService_Main logService_Main,
            MF_TY_Z_Service_Erp _MF_TY_Z_Service_Erp,
            SPC_LST_Service_Erp _SPC_LST_Service_Erp,

            Message_Tool messageTool)
        {

            this._MainSystem_DTO = mainSystem_DTO;
            //this._LogService_Main = logService_Main;
            _MF_TY_Repository = new C_ERP_Repository<MF_TY>(db);
            _MF_TY_Z_Repository = new C_ERP_Repository<MF_TY_Z>(db);
            _TF_TY_Repository = new C_ERP_Repository<TF_TY>(db);
            _SPC_LST_Repository = new C_ERP_Repository<SPC_LST>(db);



            this._MF_TY_Z_Service_Erp = _MF_TY_Z_Service_Erp;
            this._SPC_LST_Service_Erp = _SPC_LST_Service_Erp;
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
        /// 建立生產繳庫驗收單_單筆資料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ProductAcceptanceResult CreateProductAcceptance(MfTyProductAcceptance_DTO input)
        {
            var result = new ProductAcceptanceResult();
            var today01 = DateTime.Now;
            var TPNo = this.GetNo_TY(today01); //生產繳庫驗收單號
            var tydd = new DateTime(input.ty_dd.Year, input.ty_dd.Month, input.ty_dd.Day); //驗收日期
            var today = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString()); //系統輸單日期
            var Datas = new List<MfTyProductAcceptance_DTO>();
            var ddddsta = new DateTime(input.ddddsta.Year, input.ddddsta.Month, input.ddddsta.Day, input.ddddsta.Hour, input.ddddsta.Minute, input.ddddsta.Second); //上機生產時間
            var ddddend = new DateTime(input.ddddend.Year, input.ddddend.Month, input.ddddend.Day, input.ddddend.Hour, input.ddddend.Minute, input.ddddend.Second); //下機生產時間
            var ddddgre = new DateTime(input.ddddgre.Year, input.ddddgre.Month, input.ddddgre.Day, input.ddddgre.Hour, input.ddddgre.Minute, input.ddddgre.Second); //綠燈總機時間

            #region == DB資料 ==        
            var db = new DB_T014Context();
            #endregion
            //過濾送檢單號
            var dbMF_TI_Z = db.MF_TI_Z.Where(x => x.BBNUM == input.ti_no).FirstOrDefault();

            if (dbMF_TI_Z == null)
            {
                //result.PageDTO = pageDTO;
                result.IsSuccess = true;
                result.Message = $"查無 {input.ti_no} 相符的日威送檢單號的送檢單資料。";
                result.Datas = Datas;
                return result;
            }

            //送檢單表頭自定義欄位的日威送檢單號過濾出ERP的真正送檢單號寫入DB MfTy的OS_NO欄位(生產繳庫驗收單的(送檢單轉入單號)入庫單號)
            //if (input.ti_no !=null) {

            //   var dbMF_TI_Z = db.MF_TI_Z.Where(x => x.BBNUM == input.ti_no).FirstOrDefault();

            //    //無單號處理
            //    if (dbMF_TI_Z == null)
            //    {
            //        //result.PageDTO = pageDTO;
            //        result.IsSuccess = true;
            //        result.Message = $"查無 {input.ti_no} 相符的日威送檢單號的送檢單資料。";
            //        result.Datas = Datas;
            //        return result;
            //    }

            //}


            #region == 生產繳庫驗收單-表頭 [MF_TY] ==
            var addMF_TY = new MF_TY
            {
                TY_NO = TPNo, //驗收單號
                TY_DD = tydd, //驗收日期
                //TI_NO = dbMF_TI_Z.FirstOrDefault().TI_NO, //送檢單的入庫單號
                TI_NO = dbMF_TI_Z.TI_NO,
                REM = input.rem, //備註
                DEP = input.dep, //驗收部門
                SAL_NO = input.sal_no, //驗收人員
                PRT_SW = "N", //列印註記
                TY_ID = "TP", //驗收識別
                CLS_DATE = tydd, //終審日期
                SYS_DATE = today, //系統輸單日期
                CHK_KND ="1", //檢驗類型 寫入 1 常規檢驗
                CUS_OS_NO = input.cus_os_no, //客戶訂單
                
            };
            #endregion

            #region == 生產繳庫驗收單-表頭-自定義table [MF_TY_Z] ==
            var addMF_TY_Z = new MF_TY_Z
            {
                TY_ID = "TP", //驗收識別
                TY_NO = addMF_TY.TY_NO, //驗收單號
                PPPNUM = input.pppnum, //機台號碼(部門代號)
                DDDDSTA = ddddsta, //上機生產時間
                DDDDEDN = ddddend, //下機生產時間
                DDDDGRE = ddddgre, //綠燈總機時間
                RRR2 = input.rrr2, //備註2
                BBNUM = input.bbnum, //日威送驗收單號
            };
            #endregion

            #region == 生產繳庫驗收單-表身 [TF_TY] ==
            var addTF_TYs = new List<TF_TY>();
            var addSPC_LSTs = new List<SPC_LST>();
            var indexTF = 1;
            foreach (var tyTF in input.TFs)
            {
                var addTF_TY = new TF_TY
                {
                    ITM = indexTF, //項次
                    PRD_NO = tyTF.prd_no, //品號
                    PRD_NAME = tyTF.prd_name, //品名 
                    WH = tyTF.wh, //倉庫
                    UNIT = tyTF.unit, //單位
                    QTY_CHK = tyTF.qty_chk,  //驗貨數量
                    QTY_OK = tyTF.qty_ok, //合格數量
                    QTY_LOST = tyTF.qty_lost, //不合格量
                    SPC_NO = tyTF.spc_no, //原因代號
                    PRC_ID = "1", //填寫預設:1.報廢
                    TI_NO = addMF_TY.TI_NO, //送檢單的入庫單號(轉入)
                    MO_NO = tyTF.mo_no, //製令單號  
                    BIL_NO = tyTF.mo_no, //來源單號(製令單號)
                    TY_NO = addMF_TY.TY_NO,//驗收單號
                    REM = tyTF.rem,//備註
                    ID_NO = tyTF.prd_no + "->", //配方號
                    RK_DD = addMF_TY.TY_DD, //入庫日期
                    EST_ITM = indexTF, //追蹤已交數量項次
                    TY_ID = "TP", //驗收識別
                    TI_ITM = indexTF, //關聯入庫單項次 

                };
                addTF_TYs.Add(addTF_TY);
                indexTF++;


                //如果不合格原因，inpur的原因代號值過濾後，SPC_LST(不合格原因table)沒有就儲存[代號和不合格原因]，有就跳過!
                //tyTF.spc_no原因代號不是空值
                #region == 不合格原因 [SPC_LST] ==
                if (tyTF.spc_no != null) {

                    //如果SPC_NO不存在就儲存DB SPC_LST
                    if (!_SPC_LST_Service_Erp.Check_IsExist_SPC_LST(tyTF.spc_no)) {

                        var addSPC_LST = new SPC_LST
                        {
                            SPC_NO = tyTF.spc_no,
                            NAME = tyTF.spc_name,
                        };
                        addSPC_LSTs.Add(addSPC_LST);
                    }

                }
                #endregion

            }
            #endregion

            #region == [DB]寫入 ==
            try
            {
                //生產繳庫驗收單-表頭 MF_TY
                try
                {
                    _MF_TY_Repository.Create(addMF_TY);
                
                }catch (Exception ex) {

                    //刪除新增
                    DelNew(addMF_TY.TY_NO);

                    result.IsSuccess = false;
                    result.E_StatusCode = E_StatusCode.資料存取異常;
                    result.Message = $"【失敗】資料庫存取異常-[日威生產繳庫驗收單號{addMF_TY_Z.BBNUM}]-寫入[生產繳庫驗收單]異常。";
                    result.Message_Exception = ex.Message;
                    result.Message_Other = ex.InnerException.ToString();
                    return result;

                }

                //生產繳庫驗收單-表頭-自定義欄位 MF_TY_Z
                try
                {
                    _MF_TY_Z_Repository.Create(addMF_TY_Z);

                }
                catch (Exception ex)
                {

                    //刪除新增
                    DelNew(addMF_TY.TY_NO);

                    result.IsSuccess = false;
                    result.E_StatusCode = E_StatusCode.資料存取異常;
                    result.Message = $"【失敗】資料庫存取異常-[日威生產繳庫驗收單號{addMF_TY_Z.BBNUM}]-寫入[生產繳庫驗收單]異常。";
                    result.Message_Exception = ex.Message;
                    result.Message_Other = ex.InnerException.ToString();
                    return result;

                }

                //生產繳庫驗收單-表身  TF_TY
                try
                {
                    foreach (var tf_ty in addTF_TYs) { 
                    _TF_TY_Repository.Create(tf_ty);
                    }
                }
                catch (Exception ex)
                {

                    //刪除新增
                    DelNew(addMF_TY.TY_NO);

                    result.IsSuccess = false;
                    result.E_StatusCode = E_StatusCode.資料存取異常;
                    result.Message = $"【失敗】資料庫存取異常-[日威生產繳庫驗收單號{addMF_TY_Z.BBNUM}]-寫入[生產繳庫驗收單]異常。";
                    result.Message_Exception = ex.Message;
                    result.Message_Other = ex.InnerException.ToString();
                    return result;

                }


                //生產繳庫驗收單-表身的不合格原因  SPC_LST 
                //addSPC_LSTs如有資料再寫入DB
                if (addSPC_LSTs.Count > 0)
                {

                    try
                    {

                        foreach (var spcLst in addSPC_LSTs)
                        {
                            _SPC_LST_Repository.Create(spcLst);
                        }

                    }
                    catch (Exception ex)
                    {

                        //如DB存取異常，將已儲存DB的不合格原因都刪掉!
                        foreach (var spcLst in addSPC_LSTs)
                        {
                            DelSpc(spcLst.SPC_NO);
                        }

                            //刪除新增
                            DelNew(addMF_TY.TY_NO);

                        result.IsSuccess = false;
                        result.E_StatusCode = E_StatusCode.資料存取異常;
                        result.Message = $"【失敗】資料庫存取異常-[日威生產繳庫驗收單號{addMF_TY_Z.BBNUM}]-寫入[生產繳庫驗收單]異常。";
                        result.Message_Exception = ex.Message;
                        result.Message_Other = ex.InnerException.ToString();
                        return result;

                    }
                }

                result.IsSuccess = true;
                result.E_StatusCode = E_StatusCode.成功;
                result.Message = $"【成功】ERP SUNLIKE 生產繳庫驗收單號[{addMF_TY.TY_NO}]，[日威生產繳庫驗收單號{addMF_TY_Z.BBNUM}]。";

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.E_StatusCode = E_StatusCode.資料存取異常;
                result.Message = $"【失敗】資料庫存取異常-日威送生產繳庫驗收單號[{addMF_TY_Z.BBNUM}]。";
                result.Message_Exception = ex.Message;
            }
            #endregion


            return result;
        }




        /// <summary>
        ///【單筆】【生產繳庫驗收單】取得生產繳庫驗收單號
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GetNo_TY(DateTime date)
        {
            var presentNO = this.GetNos_TY(date, 1).FirstOrDefault();
            return presentNO;
        }



        /// <summary>
        /// 【多筆】【生產繳庫驗收單】取得生產繳庫驗收單號
        /// </summary>
        /// <param name="date"></param>
        /// <param name="count">要取幾組代號</param>
        /// <returns></returns>
        public List<string> GetNos_TY(DateTime date, int count)
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
            var numberPrefix = "TP";
            var query = numberPrefix + "_" + yy + MM.ToString("00") + dd.ToString("00");
            var queryLength = query.Length;

            bool _Mf_Ti_No = db.MF_TY.Any(x => x.TY_NO.StartsWith(query));

            // 取下一個流水號
            if (_Mf_Ti_No) // 取最大的SN
            {
                snNo = int.Parse(db.MF_TY.Where(x => x.TY_NO.StartsWith(query)).OrderByDescending(o => o.TY_NO).FirstOrDefault().TY_NO.Substring(queryLength)) + 1;
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
        /// 刪除新增的不合格原因
        /// </summary>
        /// <param name="spcno"></param>
        public void DelSpc(string spcno) {

            if (this.Check_IsExist_SPC_LST(spcno)) {

                var dels = _SPC_LST_Repository.GetAlls(x => x.SPC_NO == spcno);

                foreach (var del in dels) {

                    try
                    {
                        var db1 = new DB_T014Context();
                        db1.SPC_LST.Remove(del);
                        db1.SaveChanges();
                    }
                    catch { }
                }

            }

        
        }




        /// <summary>
        /// 刪除新增
        /// </summary>
        /// <param name="tyno"></param>
        public void DelNew(string tyno)
        {
            //del MF_TY 生產繳庫驗收單-表頭
            if (this.Check_IsExist_MF_TY(tyno))
            {
                var dels = _MF_TY_Repository.GetAlls(x => x.TY_NO == tyno);
                foreach (var del in dels)
                {
                    try
                    {
                        var db1 = new DB_T014Context();
                        db1.MF_TY.Remove(del);
                        db1.SaveChanges();
                    }
                    catch { }
                }

            }

            //del MF_TY_Z  生產繳庫驗收單表頭_自定義欄位檔
            if (this.Check_IsExist_MF_TY_Z(tyno))
            {
                var dels = _MF_TY_Z_Repository.GetAlls(x => x.TY_NO == tyno);
                foreach (var del in dels)
                {
                    try
                    {
                        var db1 = new DB_T014Context();
                        db1.MF_TY_Z.Remove(del);
                        db1.SaveChanges();
                    }
                    catch { }
                }

            }

            //del TF_TY 生產繳庫驗收單_表身
            if (this.Check_IsExist_TF_TY(tyno))
            {
                var dels = _TF_TY_Repository.GetAlls(x => x.TY_NO == tyno);
                foreach (var del in dels)
                {
                    try
                    {
                        var db1 = new DB_T014Context();
                        db1.TF_TY.Remove(del);
                        db1.SaveChanges();
                    }
                    catch { }
                }

            }


        }


        #region == 檢查相關 ==
        /// <summary>
        /// 檢查生產繳庫驗收單表頭資料是否存在 [true存在]
        /// </summary>
        /// <param name="no">單號</param>
        /// <returns>[true：存在]</returns>
        public bool Check_IsExist_MF_TY(string no)
        {
            var check = _MF_TY_Repository.GetAlls(x => x.TY_NO == no).Any();
            return check;
        }

        /// <summary>
        /// 檢查不合格原因資料是否存在 [true存在]
        /// </summary>
        /// <param name="spcno"></param>
        /// <returns></returns>
        public bool Check_IsExist_SPC_LST(string spcno)
        {
            var check = _SPC_LST_Repository.GetAlls(x => x.SPC_NO == spcno).Any();
            return check;
        }


        /// <summary>
        /// 檢查生產繳庫驗收單表頭自定義資料是否存在 [true存在]
        /// </summary>
        /// <param name="no">單號</param>
        /// <returns>[true：存在]</returns>
        public bool Check_IsExist_MF_TY_Z(string no)
        {
            var check = _MF_TY_Z_Repository.GetAlls(x => x.TY_NO == no).Any();
            return check;
        }

        /// <summary>
        /// 檢查生產繳庫驗收單表身資料是否存在 [true存在]
        /// </summary>
        /// <param name="no">單號</param>
        /// <returns>[true：存在]</returns>
        public bool Check_IsExist_TF_TY(string no)
        {
            var check = _TF_TY_Repository.GetAlls(x => x.TY_NO == no).Any();
            return check;
        }


        #endregion







    }
}
