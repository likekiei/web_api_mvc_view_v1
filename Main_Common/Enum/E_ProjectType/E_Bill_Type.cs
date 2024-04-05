using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Enum.E_StatusType
{
    /// <summary>
    /// 單據種類
    /// </summary>
    public enum E_Bill_Type
    {
        /// <summary>
        /// Default
        /// </summary>
        [Description("Default")]
        Default = 0,

        /// <summary>
        /// 入庫
        /// </summary>
        [Description("入庫")]
        入庫 = 201001,
        /// <summary>
        /// 出庫
        /// </summary>
        [Description("出庫")]
        出庫 = 202001,
    }
}
