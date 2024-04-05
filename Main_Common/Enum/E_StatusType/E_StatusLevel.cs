using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Enum.E_StatusType
{
    /// <summary>
    /// 狀態等級
    /// </summary>
    public enum E_StatusLevel
    {
        /// <summary>
        /// Default
        /// </summary>
        Default = 0,
        /// <summary>
        /// 正常 (無狀況)
        /// </summary>
        正常 = 100,
        /// <summary>
        /// 警告 (嚴重影響程式繼續執行)
        /// </summary>
        警告 = 200,
        /// <summary>
        /// 注意 (不要影響程式繼續執行，為系統可接受狀況，但須記錄下來)
        /// </summary>
        注意 = 300,
    }
}
