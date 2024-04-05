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

//namespace ERP_APP.Service.S_MF_PSS
//{
//    /// <summary>
//    /// 倉庫相關
//    /// </summary>
//    public class MF_PSS_Service_Erp
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
//        /// 進銷貨表頭檔
//        /// </summary>
//        private readonly C_ERP_Repository<MF_PSS> _MF_PSS_Repository;
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
//        public MF_PSS_Service_Erp(
//            DB_T020Context db,
//            MainSystem_DTO mainSystem_DTO,
//            //LogService_Main logService_Main,
//            Message_Tool messageTool)
//        {

//            this._MainSystem_DTO = mainSystem_DTO;
//            //this._DB_T020Context = _DB_T020Context;
//            //this._LogService_Main = logService_Main;
//            _MF_PSS_Repository = new C_ERP_Repository<MF_PSS>(db);
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
//        /// <summary>
//        /// 檢查銷貨單是否存在(依單號) [true存在]
//        /// </summary>
//        /// <param name="no">單號</param>
//        /// <returns>[true：存在]</returns>
//        public bool Check_IsExist_PS_NO(string no)
//        {
//            var check = _MF_PSS_Repository.GetAlls(x => x.PS_NO == no).Any();
//            return check;
//        }

//        /// <summary>
//        /// 取得銷貨單號
//        /// </summary>
//        /// <param name="no"></param>
//        /// <returns>true存在</returns>
//        public string GetPSNo(DateTime day)
//        {
//            string presentNO = "";
//            string str = "SA";
//            string yy = (day.Year - 1911).ToString();
//            yy = yy.Substring(yy.Length - 1);
//            string mm = day.Month.ToString("D2");
//            switch (mm)
//            {
//                case "10":
//                    mm = "A";
//                    break;
//                case "11":
//                    mm = "B";
//                    break;
//                case "12":
//                    mm = "C";
//                    break;
//            }
//            string dd = day.Day.ToString("D2");
//            string query = str + yy + mm + dd;

//            if (_MF_PSS_Repository.GetAlls(x => x.PS_NO.StartsWith(query) && x.PS_NO.Substring(x.PS_NO.Length - 1, 1) == "_").Any())
//            {
//                var temp = (int.Parse(_MF_PSS_Repository.GetAlls(x => x.PS_NO.StartsWith(query) && x.PS_NO.Substring(x.PS_NO.Length - 1, 1) == "_").OrderByDescending(o => o.PS_NO).FirstOrDefault().PS_NO.Substring(6, 4)) + 1).ToString("0000");
//                presentNO = query + temp + "_";
//            }
//            else
//            {
//                presentNO = query + "0001_";
//            }
//            return presentNO;
//        }

//        /// <summary>
//        /// 由部門取得倉庫
//        /// </summary>
//        /// <param name="no"></param>
//        /// <returns>true存在</returns>
//        public MF_PSS GetWH_ByDept(string no)
//        {
//            var data = _MF_PSS_Repository.GetAlls(x => x.DEP == no).SingleOrDefault();
//            return data;

//        }

//    }
//}
