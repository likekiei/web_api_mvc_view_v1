using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Enum.E_ProjectType
{
    /// <summary>
    /// Excel主要工作表名稱對照
    /// </summary>
    public enum E_Excel_MainSheetName
    {
        [Description("Default")]
        Default = 0,
        [Description("使用者")]
        使用者資料匯入,
        [Description("產品")]
        產品資料匯入,
        [Description("銷售")]
        銷售資料匯入,
        [Description("獎懲")]
        獎懲資料匯入,
    }
}
