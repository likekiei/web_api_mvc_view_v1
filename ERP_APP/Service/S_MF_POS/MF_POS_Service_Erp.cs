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
//using Main_Common.Mothod.Message;
//using Main_Common.Tool;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Schema;

//namespace ERP_APP.Service.S_MF_POS
//{
//    /// <summary>
//    /// 採購受訂相關
//    /// </summary>
//    public class MF_POS_Service_Erp
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
//        //private readonly DB_T020Context _DB_T020Context;
//        /// <summary>
//        /// 採購受訂表頭檔
//        /// </summary>
//        private readonly C_ERP_Repository<MF_POS> _MF_POS_Repository;
//        /// <summary>
//        /// 
//        /// </summary>
//        private readonly C_ERP_Repository<MF_POS_Z> _MF_POS_Z_Repository;
//        /// <summary>
//        /// 採購受訂表身檔
//        /// </summary>
//        private readonly C_ERP_Repository<TF_POS> _TF_POS_Repository;
//        /// <summary>
//        /// 採購/受訂變更作業表頭庫
//        /// </summary>
//        private readonly C_ERP_Repository<MF_BG> _MF_BG_Repository;
//        /// <summary>
//        /// 採購/受訂變更作業表身庫
//        /// </summary>
//        private readonly C_ERP_Repository<TF_BG> _TF_BG_Repository;
//        /// <summary>
//        /// 採購/受訂變更作業表身庫(變更表頭)
//        /// </summary>
//        private readonly C_ERP_Repository<TF_BG1> _TF_BG1_Repository;
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
//        public MF_POS_Service_Erp(
//            DB_T020Context db,
//            MainSystem_DTO mainSystem_DTO,         
//            //LogService_Main logService_Main,
//            Message_Tool messageTool)
//        {

//            this._MainSystem_DTO = mainSystem_DTO;
//            //this._DB_T020Context = _DB_T020Context;
//            _MF_POS_Repository = new C_ERP_Repository<MF_POS>(db);
//            _MF_POS_Z_Repository = new C_ERP_Repository<MF_POS_Z>(db);
//            _TF_POS_Repository = new C_ERP_Repository<TF_POS>(db);
//            _MF_BG_Repository = new C_ERP_Repository<MF_BG>(db);
//            _TF_BG_Repository = new C_ERP_Repository<TF_BG>(db); 
//            _TF_BG1_Repository = new C_ERP_Repository<TF_BG1>(db);
//            //this._LogService_Main = logService_Main;
//            this._Message_Tool = messageTool;
//        }
//        #endregion

//        //--【方法】=================================================================================

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
//        private DateTime preday;
//        //private List<Product_DTO> _Product_DTOs = new List<Product_DTO>();
//        //private List<Order_DTO> _Order_DTOs = new List<Order_DTO>();
//        #endregion

//        //--【建構】=================================================================================

//        #region == 建構 ==
//        /// <summary>
//        /// 【建構】同源EntityContext
//        /// </summary>
//        /// <param name="input"></param>
//        //public Order_Service_Erp(UserSession_Model input)
//        //{
//        //    var db = new DB_T020Context();

//        //    _MF_YG_Repository = new C_ERP_Repository<MF_YG>(db);
//        //    _SALM_Repository = new C_ERP_Repository<SALM>(db);
//        //    //_TF_POS_Repository = new C_ERP_Repository<TF_POS>(db);
//        //    //_PRDT_Repository = new C_ERP_Repository<PRDT>(db);
//        //    //_Order_Repository = new C_ERP_Repository<Order>(db);

//        //    _UserSession_Model = input; //保存
//        //}
//        #endregion

//        //--【方法】=================================================================================
//        /// <summary>
//        /// 檢查訂單是否存在
//        /// </summary>
//        /// <param name="no"></param>
//        /// <returns>true存在</returns>
//        public bool Check_IsExist_POS(string no)
//        {
//            var check = _MF_POS_Repository.GetAlls(x => x.OS_NO == no).Any();
//            return check;
//        }

//        /// <summary>
//        /// 新增訂單表頭
//        /// </summary>
//        /// <param name="input"></param>
//        /// <param name="input2"></param>
//        public void CreateMF(MF_POS input, MF_POS_Z input2)
//        {
//            var check = _MF_POS_Repository.GetAlls(x => x.OS_NO == input.OS_NO);
//            if (!check.Any())
//            {
//                _MF_POS_Repository.Create(input);
//                _MF_POS_Z_Repository.Create(input2);
//            }
//        }

//        /// <summary>
//        /// 新增訂單表身
//        /// </summary>
//        /// <param name="input"></param>
//        public void CreateTF(TF_POS input)
//        {
//            var check = _TF_POS_Repository.GetAlls(x => x.OS_NO == input.OS_NO && x.ITM == input.ITM).ToList();
//            if (!check.Any())
//            {
//                _TF_POS_Repository.Create(input);
//            }
//        }

//        public List<TF_POS> GetItemsInfo(int orderKey)
//        {
//            //var data0 = _MF_POS_Z_Repository.GetAlls(x => x.OrderKey == orderKey).FirstOrDefault();
//            var data0 = _MF_POS_Z_Repository.GetAll().FirstOrDefault();
//            if (data0 != null)
//            {
//                var data = _TF_POS_Repository.GetAlls(x => x.OS_NO == data0.OS_NO).ToList();
//                return data;
//            }
//            else
//            {
//                return new List<TF_POS>();
//            }
//        }

//        /// <summary>
//        /// 【新版】建立異動單據
//        /// </summary>
//        /// <param name="item"></param>
//        /// <param name="item_z"></param>
//        /// <param name="items"></param>
//        /// <returns></returns>
//        public (bool isChange, TF_BG1 master, List<TF_BG> items) CreateBG(MF_POS item, MF_POS_Z item_z, List<(TF_POS input, ECUD type)> items)
//        {

//            var record = new MF_BG
//            {
//                BG_NO = this.GetBG_NO(preday),
//                BG_DD = item.SYS_DATE,
//                DEP = item.PO_DEP,
//                REM = "訂單修正",
//                USR = "ADMIN",
//                PRT_SW = "N",
//                CHK_MAN = "ADMIN",
//                BG_ID = "IS",
//                CLS_DATE = item.SYS_DATE,
//                SYS_DATE = item.SYS_DATE,
//                BG_TYPE = "3"
//            };  //summary
//            TF_BG1 recordBG1 = new TF_BG1(); //表頭

//            List<TF_BG> recordBG = new List<TF_BG>(); //表身

//            //var check = _MF_POS_Z_Repository.GetAlls(x => x.OrderKey == item_z.OrderKey);
//            var check = _MF_POS_Z_Repository.GetAlls(x => x.OS_NO == item_z.OS_NO);
//            if (check.Any())
//            {
//                var mf_posData = _MF_POS_Repository.GetAlls(x => x.OS_ID == "SO" && x.OS_NO == item.OS_NO).FirstOrDefault();

//                #region 檢查表頭有無資訊更正
//                if (mf_posData.OS_DD != item.OS_DD)
//                {
//                    //this.generateMF(recordBG1)
//                    if (string.IsNullOrEmpty(recordBG1.BG_ID))
//                    {
//                        recordBG1.BG_ID = "IS";
//                    }
//                    recordBG1.OS_DD = item.OS_DD;
//                }
//                if (mf_posData.CUS_NO != item.CUS_NO)
//                {
//                    //this.generateMF(recordBG1)
//                    if (string.IsNullOrEmpty(recordBG1.BG_ID))
//                    {
//                        recordBG1.BG_ID = "IS";
//                    }
//                    recordBG1.CUS_NO = item.CUS_NO;
//                }
//                if (mf_posData.PO_DEP != item.PO_DEP)
//                {
//                    //this.generateMF(recordBG1)
//                    if (string.IsNullOrEmpty(recordBG1.BG_ID))
//                    {
//                        recordBG1.BG_ID = "IS";
//                    }
//                    recordBG1.PO_DEP = item.PO_DEP;
//                }
//                if (mf_posData.SAL_NO != item.SAL_NO)
//                {
//                    //this.generateMF(recordBG1)
//                    if (string.IsNullOrEmpty(recordBG1.BG_ID))
//                    {
//                        recordBG1.BG_ID = "IS";
//                    }
//                    recordBG1.SAL_NO = item.SAL_NO;
//                }
//                if (mf_posData.REM != item.REM)
//                {
//                    //this.generateMF(recordBG1)
//                    if (string.IsNullOrEmpty(recordBG1.BG_ID))
//                    {
//                        recordBG1.BG_ID = "IS";
//                    }
//                    recordBG1.REM = item.REM;
//                }

//                if (!string.IsNullOrEmpty(recordBG1.BG_ID)) //表頭已變更
//                {
//                    recordBG1.ITM = 1;
//                    recordBG1.BG_FLAG = "1";
//                    recordBG1.OS_NO = item.OS_NO;
//                    recordBG1.BG_REM = "訂單修正";
//                    recordBG1.PRE_ITM = 1;
//                    recordBG1.BG_DD = item.SYS_DATE;
//                    recordBG1.BG_NO = record.BG_NO;
//                    recordBG1.BG_ID = "IS";
//                }
//                #endregion

//                #region 檢查表身有無資訊更正
//                var index = 1;
//                foreach (var x in items)
//                {
//                    var _item = new TF_BG();
//                    switch (x.type)
//                    {
//                        case ECUD.C:
//                            _item.UP = x.input.UP;
//                            _item.AMT = x.input.AMT;
//                            _item.AMTN = x.input.AMTN;
//                            _item.TAX = x.input.TAX;
//                            _item.EST_DD = x.input.EST_DD;
//                            _item.PRE_EST_DD = x.input.PRE_EST_DD;

//                            _item.ITM = index;
//                            _item.BG_FLAG = "2";
//                            _item.OS_NO = x.input.OS_NO;
//                            _item.BIL_ITM = x.input.ITM;
//                            _item.BG_REM = "門市修正";
//                            _item.BG_DD = x.input.EST_DD;
//                            _item.EST_ITM = x.input.ITM;
//                            _item.BG_ID = "IS";
//                            _item.BG_NO = record.BG_NO;
//                            _item.OTH_ITM = null;
//                            _item.SQ_ITM = null;
//                            _item.PRE_ITM = 1;
//                            _item.QTY_FRAC1 = 1;
//                            _item.EXC_RTO = 1;
//                            _item.TAX_ID = "2";

//                            _item.BG_ID = "IS";
//                            _item.PRE_ITM_OS = x.input.ITM;
//                            break;
//                        case ECUD.U:
//                            var UpdateItem = _TF_POS_Repository.GetAlls(xx => xx.OS_ID == "SO" && xx.OS_NO == x.input.OS_NO && xx.ITM == x.input.ITM).FirstOrDefault();
//                            if (UpdateItem != null)
//                            {
//                                if (UpdateItem.UP != x.input.UP)
//                                {
//                                    if (string.IsNullOrEmpty(_item.BG_ID))
//                                    {
//                                        _item.BG_ID = "IS";
//                                    }
//                                    _item.UP = x.input.UP;
//                                    _item.AMT = x.input.AMT;
//                                    _item.AMTN = x.input.AMTN;
//                                    _item.TAX = x.input.TAX;
//                                }
//                                if (UpdateItem.PRE_EST_DD != x.input.EST_DD)
//                                {
//                                    if (string.IsNullOrEmpty(_item.BG_ID))
//                                    {
//                                        _item.BG_ID = "IS";
//                                    }
//                                    _item.PRE_EST_DD = x.input.EST_DD;
//                                }
//                                if (!string.IsNullOrEmpty(_item.BG_ID)) //表身已變更
//                                {
//                                    _item.ITM = index;
//                                    _item.BG_FLAG = "1";
//                                    _item.OS_NO = x.input.OS_NO;
//                                    _item.BIL_ITM = x.input.ITM;
//                                    _item.BG_REM = "訂單修正";
//                                    _item.BG_DD = x.input.EST_DD;
//                                    _item.EST_ITM = x.input.ITM;
//                                    _item.BG_ID = "IS";
//                                    _item.BG_NO = record.BG_NO;
//                                    _item.OTH_ITM = 0;
//                                    _item.SQ_ITM = 0;
//                                    _item.PRE_ITM = 1;
//                                    _item.QTY_FRAC1 = 1;
//                                    _item.EXC_RTO = 1;
//                                    _item.TAX_ID = "2";
//                                    _item.PRE_ITM_OS = UpdateItem.PRE_ITM;
//                                }
//                            }
//                            break;
//                        case ECUD.D:
//                            var RemoveItem = _TF_POS_Repository.GetAlls(xx => xx.OS_ID == "SO" && xx.OS_NO == x.input.OS_NO && xx.ITM == x.input.ITM).FirstOrDefault();
//                            if (RemoveItem != null)
//                            {
//                                _item.ITM = index;
//                                _item.BG_FLAG = "3";
//                                _item.OS_NO = x.input.OS_NO;
//                                _item.BIL_ITM = x.input.ITM;
//                                _item.BG_REM = "門市修正";
//                                _item.BG_DD = x.input.EST_DD;
//                                _item.EST_ITM = x.input.ITM;
//                                _item.BG_ID = "IS";
//                                _item.BG_NO = record.BG_NO;
//                                _item.OTH_ITM = 0;
//                                _item.SQ_ITM = 0;
//                                _item.PRE_ITM = 1;
//                                _item.EXC_RTO = 1;
//                                _item.TAX_ID = "2";
//                                _item.PRE_ITM_OS = RemoveItem.PRE_ITM;
//                            }
//                            break;
//                        default:
//                            break;
//                    }
//                    //var UpdateItem = _TF_POSRepository.GetAlls(xx => xx.OS_ID == "SO" && xx.OS_NO == x.OS_NO && xx.ITM == x.ITM).FirstOrDefault();
//                    if (!string.IsNullOrEmpty(_item.BG_ID)) //該筆資訊確實有修正
//                    {
//                        recordBG.Add(_item);
//                    }
//                    index++;
//                }
//                #endregion

//                #region 重新整理表身的順序

//                #endregion
//                var rearrangeIndex = 1;
//                foreach (var xItem in recordBG)
//                {
//                    xItem.ITM = rearrangeIndex;
//                    rearrangeIndex++;
//                }

//                #region DB transaction
//                if (!string.IsNullOrEmpty(recordBG1.BG_ID) || recordBG.Count() > 0)
//                {
//                    _MF_BG_Repository.Create(record);

//                    if (!string.IsNullOrEmpty(recordBG1.BG_ID))
//                    {
//                        _TF_BG1_Repository.Create(recordBG1);
//                    }
//                    else
//                    {
//                        recordBG1 = null;
//                    }

//                    foreach (var nitem in recordBG)
//                    {
//                        _TF_BG_Repository.Create(nitem);
//                    }

//                    return (isChange: true, master: recordBG1, items: recordBG);
//                }
//                else
//                {
//                    return (isChange: false, master: recordBG1, items: recordBG);
//                }
//                #endregion
//            }
//            else
//            {
//                return (isChange: false, master: recordBG1, items: recordBG);
//            }
//        }

//        /// <summary>
//        /// 修改訂單表頭
//        /// </summary>
//        /// <param name="input"></param>
//        /// <param name="input2"></param>
//        /// <param name="isChange"></param>
//        public void UpdateMF(MF_POS input, MF_POS_Z input2, bool isChange)
//        {
//            //var data_z = _MF_POS_Z_Repository.GetAlls(x => x.OrderKey == input2.OrderKey).FirstOrDefault();
//            var data_z = _MF_POS_Z_Repository.GetAlls(x => x.OS_NO == input2.OS_NO).FirstOrDefault();
//            if (data_z != null)
//            {
//                var o_No = data_z.OS_NO;

//                if (data_z.OS_NO != input2.OS_NO) //受訂單號被修正
//                {
//                    //受訂單表頭_Z
//                    data_z.OS_NO = input2.OS_NO;

//                    //受訂單表頭
//                    var data = _MF_POS_Repository.GetAlls(x => x.OS_ID == "SO" && x.OS_NO == o_No).FirstOrDefault();
//                    data.OS_NO = input2.OS_NO;
//                    //受訂單表身
//                    var tfDatas = _TF_POS_Repository.GetAlls(x => x.OS_ID == "SO" && x.OS_NO == o_No).ToList();
//                    foreach (var x in tfDatas)
//                    {
//                        x.OS_NO = input2.OS_NO;
//                        _TF_POS_Repository.SaveChanges();
//                    }
//                    //異動單表頭
//                    var eData = _TF_BG_Repository.GetAlls(x => x.OS_NO == o_No).FirstOrDefault();
//                    eData.OS_NO = input2.OS_NO;
//                    _TF_BG_Repository.SaveChanges();
//                    //異動單表身
//                    var eDatas = _TF_BG1_Repository.GetAlls(x => x.OS_NO == o_No).ToList();
//                    foreach (var x in eDatas)
//                    {
//                        x.OS_NO = input2.OS_NO;
//                        _TF_BG1_Repository.SaveChanges();
//                    }
//                }

//                //受訂單表頭_Z
//                //data_z.Departure = input2.Departure;
//                //data_z.Destination = input2.Destination;
//                //data_z.InvoiceAdr = input2.InvoiceAdr;
//                //data_z.Version = input2.Version;
//                //data_z.TaxIdNumber = input2.TaxIdNumber;
//                //data_z.Title = input2.Title;
//                _MF_POS_Z_Repository.SaveChanges();
//                //受訂單表頭
//                var posData = _MF_POS_Repository.GetAlls(x => x.OS_ID == "SO" && x.OS_NO == input.OS_NO).FirstOrDefault();
//                posData.OS_DD = input.OS_DD;
//                posData.CUS_NO = input.CUS_NO;
//                posData.PO_DEP = input.PO_DEP;
//                posData.SAL_NO = input.SAL_NO;
//                posData.EST_DD = input.EST_DD;
//                posData.AMTN_NET = input.AMTN_NET;
//                posData.TAX = input.TAX;
//                posData.PAY_DD = input.PAY_DD;
//                posData.CHK_DD = input.CHK_DD;
//                posData.CLS_DATE = input.CLS_DATE;
//                posData.SYS_DATE = input.SYS_DATE;
//                posData.REM = input.REM;
//                if (isChange)
//                {
//                    posData.MODIFY_DD = input.SYS_DATE;
//                    posData.MODIFY_MAN = "ADMIN";
//                }
//                _MF_POS_Repository.SaveChanges();

//            }
//        }

//        /// <summary>
//        /// 取得變更作業單號MF_BG.NO
//        /// </summary>
//        /// <param name="date"></param>
//        /// <returns></returns>
//        private string GetBG_NO(DateTime date)
//        {
//            int yyyy = date.Year;
//            int MM = date.Month;
//            int dd = date.Day;
//            string presentNO;
//            var query = "IS-" + yyyy.ToString("0000") + MM.ToString("00") + dd.ToString("00");
//            if (_MF_BG_Repository.GetAlls(x => x.BG_NO.StartsWith(query)).Any())
//            {
//                var mm = (int.Parse(_MF_BG_Repository.GetAlls(x => x.BG_ID == "IS" && x.BG_NO.StartsWith(query)).OrderByDescending(o => o.BG_NO).FirstOrDefault().BG_NO.Substring(11)) + 1).ToString("000");
//                presentNO = query + mm;
//            }
//            else
//            {
//                presentNO = query + "001";
//            }
//            return presentNO;

//        }
//    }
//}
