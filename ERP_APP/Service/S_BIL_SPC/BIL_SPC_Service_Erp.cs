//using ERP_EF.Models;
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

//namespace ERP_APP.Service.S_BIL_SPC
//{
//    /// <summary>
//    /// 單據類別設定相關
//    /// </summary>
//    public class BIL_SPC_Service_Erp
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
//        public BIL_SPC_Service_Erp(
//            DB_T020Context _DB_T020Context,
//            MainSystem_DTO mainSystem_DTO,
//            //LogService_Main logService_Main,
//            Message_Tool messageTool)
//        {

//            this._MainSystem_DTO = mainSystem_DTO;
//            this._DB_T020Context = _DB_T020Context;
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

//        //    _MF_YG_ReWHitory = new C_ERP_ReWHitory<MF_YG>(db);
//        //    _SALM_ReWHitory = new C_ERP_ReWHitory<SALM>(db);
//        //    //_TF_WH_ReWHitory = new C_ERP_ReWHitory<TF_WH>(db);
//        //    //_PRDT_ReWHitory = new C_ERP_ReWHitory<PRDT>(db);
//        //    //_Order_ReWHitory = new C_ERP_ReWHitory<Order>(db);

//        //    _UserSession_Model = input; //保存
//        //}
//        #endregion

//        //--【方法】================================================================================= 
//        /// <summary>
//        /// 建立預設之服務項目
//        /// </summary>
//        public void CreateDefaultServiceItems()
//        {
//            #region 預設值設定
//            var items = new List<BIL_SPC>();
//            var item1 = new BIL_SPC
//            {
//                BIL_ID = "SA",
//                SPC_ID = "OB",
//                SPC_NO = "01",
//                NAME = "搬家服務"
//            };
//            var item2 = new BIL_SPC
//            {
//                BIL_ID = "SA",
//                SPC_ID = "OB",
//                SPC_NO = "01",
//                NAME = "包材服務"
//            };
//            var item3 = new BIL_SPC
//            {
//                BIL_ID = "SA",
//                SPC_ID = "OB",
//                SPC_NO = "01",
//                NAME = "倉租租賃"
//            };
//            var item4 = new BIL_SPC
//            {
//                BIL_ID = "SA",
//                SPC_ID = "OB",
//                SPC_NO = "01",
//                NAME = "租借服務"
//            };

//            items.Add(item1);
//            items.Add(item2);
//            items.Add(item3);
//            items.Add(item4);

//            #endregion

//            foreach (var x in items)
//            {
//                var check = _DB_T020Context.BIL_SPC.Where(xx => xx.BIL_ID == x.BIL_ID && xx.SPC_ID == x.SPC_ID && xx.SPC_NO == x.SPC_NO);
//                if (!check.Any())
//                {
//                    _DB_T020Context.BIL_SPC.Add(x);
//                }
//            }
//        }

//    }
//}
