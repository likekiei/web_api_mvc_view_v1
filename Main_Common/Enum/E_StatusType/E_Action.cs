using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Enum.E_StatusType
{
    /// <summary>
    /// 動作執行種類
    /// </summary>
    public enum E_Action
    {
        Default = 0,
        新增 = 1,
        修改 = 2,
        刪除 = 3,
        作廢 = 4,
        匯入 = 5,
        登入 = 6,
        登出 = 7,
        評估 = 8,
        //自評 = 9,
        標記 = 10,
        評分 = 11,
        計算 = 12,
        初始化 = 13,
        查詢 = 14,
    }
}
