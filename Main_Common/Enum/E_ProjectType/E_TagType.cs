using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Enum.E_ProjectType
{
    /// <summary>
    /// 標籤種類
    /// </summary>
    public enum E_TagType
    {
        /// <summary>
        /// Default
        /// </summary>
        [Description("Default")]
        Default = 0,
        /// <summary>
        /// 入庫單單號
        /// </summary>
        [Description("入庫單單號")]
        入庫單單號 = 201001,
        /// <summary>
        /// 出庫單單號
        /// </summary>
        [Description("出庫單單號")]
        出庫單單號 = 202001,
        /// <summary>
        /// 出庫單訂單編號
        /// </summary>
        [Description("出庫單訂單編號")]
        出庫單訂單編號 = 202002,
        /// <summary>
        /// 出庫單SO編號
        /// </summary>
        [Description("出庫單SO編號")]
        出庫單SO編號 = 202003,
        /// <summary>
        /// 出庫單櫃號
        /// </summary>
        [Description("出庫單櫃號")]
        出庫單櫃號 = 202004,
    }
}
