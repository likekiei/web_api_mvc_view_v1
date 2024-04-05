using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Enum.Document
{
    /// <summary>
    /// 立帳方式
    /// </summary>
    public enum E_SetARP_Method
    {
        /// <summary>
        /// Default
        /// </summary>
        Default = 0,
        單張立帳 = 1,
        不立帳 = 2,
        收到發票才立帳 = 3,
    }
}
