using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Enum.E_ProjectType
{
    /// <summary>
    /// 單位種類
    /// </summary>
    public enum E_UnitType
    {
        /// <summary>
        /// Default
        /// </summary>
        [Description("Default")]
        Default = 0,

        #region == 體積相關的單位(201XXX) ==
        /// <summary>
        /// 公分
        /// </summary>
        [Description("公分")]
        CM = 201001,
        #endregion

    }
}
