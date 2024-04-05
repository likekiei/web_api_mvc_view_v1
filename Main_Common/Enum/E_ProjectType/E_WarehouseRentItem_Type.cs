using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Enum.E_ProjectType
{
    /// <summary>
    /// 倉租項目種類
    /// </summary>
    public enum E_WarehouseRentItem_Type
    {
        /// <summary>
        /// Default
        /// </summary>
        [Description("Default")]
        Default = 0,
        /// <summary>
        /// 入庫
        /// </summary>
        [Description("固定項目")]
        固定項目 = 1,
        /// <summary>
        /// 出庫
        /// </summary>
        [Description("桶子項目")]
        桶子項目 = 2,
    }
}
