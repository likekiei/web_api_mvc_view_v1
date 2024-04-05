using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Enum.E_ProjectType
{
    /// <summary>
    /// 報表群組種類
    /// </summary>
    public enum E_ReportGroupType
    {
        /// <summary>
        /// Default
        /// </summary>
        [Description("Default")]
        Default = 0,
        /// <summary>
        /// 明細
        /// </summary>
        [Description("明細")]
        明細 = 101001,
        /// <summary>
        /// 統計
        /// </summary>
        [Description("統計")]
        統計 = 102001,
    }
}