using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Enum.E_StatusType
{
    /// <summary>
    /// 狀態碼
    /// </summary>
    public enum E_StatusCode
    {
        Default = 0,
        成功 = 1,
        失敗 = 2,
        不執行 = 3,
        處理異常 = 4,
        權限異常 = 5,
        不存在的執行 = 6,
        無資料 = 7,

        資料存取異常 = 1000,
        取資料失敗 = 1001,
        不允許新增 = 1002,
        不允許修改 = 1003,
        不允許刪除 = 1004,
        不允許設定 = 1005,
        不允許拋轉Erp = 1006,
        已結案 = 1007,

        登入逾時 = 2000,
        Token驗證錯誤 = 2001,
        登入異常 = 2002,

        檢查異常 = 5000,
        查無項目 = 5001,
        查無相關資料 = 5002,
        存在相同資料 = 5003,
        存在相關資料 = 5004,

        必須存在一個項目 = 7001,
        無表身項目 = 7002,
        回寫時發生錯誤 = 7003,

        計算異常 = 8001,
    }
}
