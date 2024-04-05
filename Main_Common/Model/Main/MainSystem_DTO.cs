using Main_Common.Enum.E_ProjectType;
using Main_Common.Enum.E_StatusType;
using Main_Common.Model.Account;
using Main_Common.Model.Data.Log;
using Main_Common.Model.Message;
using Main_Common.Model.Result;
using Main_Common.Model.Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main_Common.Model.Main
{
    /// <summary>
    /// 【DTO】主系統資料
    /// </summary>
    public class MainSystem_DTO
    {
        #region == 主要屬性 ===============================================================================
        /// <summary>
        /// 執行的綁定Key
        /// </summary>
        public Guid BindKey_ByAction { get; }
        /// <summary>
        /// 例外的綁定Key
        /// </summary>
        public Guid BindKey_ByException { get; set; }
        /// <summary>
        /// 是否執行例外過濾器
        /// </summary>
        public bool IsRunExceptionFilter { get; set; }
        /// <summary>
        /// 資料庫Table種類
        /// </summary>
        public E_DBTable DBTableType { get; set; }
        /// <summary>
        /// Log種類
        /// </summary>
        public E_LogType LogType { get; set; }
        /// <summary>
        /// 執行動作種類
        /// </summary>
        public E_Action ActionType { get; set; }
        /// <summary>
        /// 結果訊息
        /// </summary>
        public ResultSimple Result { get; set; }
        /// <summary>
        /// 登入者資訊
        /// </summary>
        public UserSession_Model UserSession { get; set; }
        /// <summary>
        /// 權限參數
        /// </summary>
        public Permission_DTO Permission { get; set; }
        /// <summary>
        /// Log紀錄清單
        /// </summary>
        public List<MainSystem_Log> LogList { get; set; }
        /// <summary>
        /// 執行方法資訊
        /// </summary>
        public MethodBase MethodBase { get; set; }
        #endregion

        #region == 建構 ===============================================================================
        /// <summary>
        /// 建構-初始值
        /// </summary>
        public MainSystem_DTO()
        {
            this.BindKey_ByAction = Guid.NewGuid();
            this.Result = new ResultSimple(true, "尚未初始化");
            //this.LogInfo = new MainSystem_Log();
            this.LogList = new List<MainSystem_Log>();
            //this.LogList = new List<Log_Model>();
        }
        #endregion

        #region == 方法 ===============================================================================
        /// <summary>
        /// 設定LogConfig
        /// </summary>
        public void Set_LogConfig(E_DBTable eDBTable, E_LogType eLogType, E_Action eAction)
        {
            this.DBTableType = eDBTable;
            this.LogType = eLogType;
            this.ActionType = eAction;
        }
        #endregion
    }
}
