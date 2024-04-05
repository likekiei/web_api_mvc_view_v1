using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Enum.E_MothodType
{
    /// <summary>
    /// 檢查方式
    /// </summary>
    public enum E_CheckMothod
    {
        Default = 0,
        必填 = 1,
        字串Max = 2,
        字串Min = 3,
        型別轉換 = 4,
    }
}
