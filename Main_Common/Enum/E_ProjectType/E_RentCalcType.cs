using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Enum.E_ProjectType
{
    /// <summary>
    /// 倉租計算種類
    /// </summary>
    public enum E_RentCalcType
    {
        /// <summary>
        /// Default
        /// </summary>
        [Description("Default")]
        Default = 0,
        /// <summary>
        /// 月結
        /// </summary>
        [Description("月結")]
        月結 = 1,
        /// <summary>
        /// 出庫結
        /// </summary>
        [Description("出庫結")]
        出庫結 = 2,
    }
}
