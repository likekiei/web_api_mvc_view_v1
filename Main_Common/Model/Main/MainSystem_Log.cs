using Main_Common.Model.Account;
using Main_Common.Model.Data.Log;
using Main_Common.Model.Message;
using Main_Common.Model.Result;
using Main_Common.Model.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.Main
{
    /// <summary>
    /// 【DTO】主系統Log紀錄
    /// </summary>
    public class MainSystem_Log
    {
        #region == 主要屬性 ===============================================================================
        /// <summary>
        /// 綁定Key
        /// </summary>
        public Guid BindKey { get; set; }
        /// <summary>
        /// Log資訊
        /// </summary>
        public Log_DTO LogInfo { get; set; }
        /// <summary>
        /// 訊息清單
        /// </summary>
        public List<Message_DTO> MessageList { get; set; }
        #endregion

        #region == 建構 ===============================================================================
        /// <summary>
        /// 建構-初始值
        /// </summary>
        public MainSystem_Log()
        {
            this.MessageList = new List<Message_DTO>();
        }
        #endregion
    }
}
