using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Enum.E_StatusType
{
    /// <summary>
    /// 出庫種類
    /// </summary>
    public enum E_Outbound_Type
    {
        /// <summary>
        /// Default
        /// </summary>
        [Description("Default")]
        Default = 0,

        /// <summary>
        /// 內銷
        /// </summary>
        [Description("內銷")]
        內銷 = 200001,
        /// <summary>
        /// 外銷
        /// </summary>
        [Description("外銷")]
        外銷 = 200002,
    }
}
