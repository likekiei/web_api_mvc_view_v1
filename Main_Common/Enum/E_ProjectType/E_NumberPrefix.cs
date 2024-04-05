using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Enum.E_ProjectType
{
    /// <summary>
    ///  編碼前綴 (描述文字為編碼前綴)
    /// </summary>
    public enum E_NumberPrefix
    {
        #region == Main DB ==
        [Description("")]
        Default = 0,
        [Description("m101")]
        連線群組 = 1,
        [Description("m102")]
        連線資訊 = 2,
        [Description("m103")]
        連線設定檔 = 3,
        [Description("m104")]
        公司 = 4,
        [Description("m105")]
        角色 = 5,
        [Description("m106")]
        功能代碼 = 6,
        [Description("m107")]
        使用者 = 7,
        #endregion

        #region == Erp DB ==
        // ...
        #endregion
    }
}
