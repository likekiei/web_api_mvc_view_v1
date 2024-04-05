using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Enum.E_ProjectType
{
    /// <summary>
    /// 登入來源種類
    /// </summary>
    public enum E_LoginFromType
    {
        /// <summary>
        /// Default
        /// </summary>
        Default = 0,
        /// <summary>
        /// 網站
        /// </summary>
        Web = 1,
        /// <summary>
        /// Api
        /// </summary>
        Api = 2,
        /// <summary>
        /// 其它
        /// </summary>
        其它 = 999,
    }
}
