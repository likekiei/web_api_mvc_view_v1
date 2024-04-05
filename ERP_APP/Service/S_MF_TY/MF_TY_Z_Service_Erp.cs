using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_EF.Models;
using ERP_EF.Repository;
using Main_Common.Model.Account;
using Main_Common.Model.Data;
using Main_Common.Model.Main;
using Main_Common.Model.Result;
using Main_Common.Tool;

namespace ERP_APP.Service.S_MF_TY
{
    /// <summary>
    /// 生產繳庫驗收單表頭_自定義檔
    /// </summary>
    public class MF_TY_Z_Service_Erp
    {
        #region == 【DI注入用宣告】 ==
        /// <summary>
        /// 資料庫工作單元
        /// </summary>
        //public readonly IUnitOfWork _unitOfWork;
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
        //private readonly DB_T020Context _DB_T020Context;
        /// <summary>
        /// 生產繳庫驗收單表頭_自定義檔
        /// </summary>
        private readonly C_ERP_Repository<MF_TY_Z> _MF_TY_Z_Repository;
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
        public MF_TY_Z_Service_Erp(
            DB_T014Context db,
            MainSystem_DTO mainSystem_DTO,
            //LogService_Main logService_Main,
            Message_Tool messageTool)
        {
            this._MainSystem_DTO = mainSystem_DTO;
            _MF_TY_Z_Repository = new C_ERP_Repository<MF_TY_Z>(db);
            this._Message_Tool = messageTool;
        }
        #endregion

        //--【方法】=================================================================================

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

        #region == 檢查相關 ==
        /// <summary>
        /// 檢查日威單號是否存在(依日威送檢單號) [true存在]
        /// </summary>
        /// <param name="no">單號</param>
        /// <returns>[true：存在]</returns>
        public bool Check_IsExist_MF_TI_Z(string no)
        {
            var check = _MF_TY_Z_Repository.GetAlls(x => x.BBNUM == no).Any();
            return check;
        }
        #endregion



    }
}
