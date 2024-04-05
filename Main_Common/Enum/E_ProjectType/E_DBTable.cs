using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Enum.E_ProjectType
{
    /// <summary>
    /// 資料表項目
    /// </summary>
    public enum E_DBTable
    {
        /// <summary>
        /// Default
        /// </summary>
        Default = 0,
        /// <summary>
        /// NotTable
        /// </summary>
        NotTable = 1,
        /// <summary>
        /// Login_Record(無實際Table)
        /// </summary>
        Login_Record = 2,

        #region == 系統設定DB ==
        /// <summary>
        /// Log紀錄
        /// </summary>
        System_Log = 100000,
        /// <summary>
        /// 連線資訊
        /// </summary>
        System_Connect = 100001,
        /// <summary>
        /// 連線群組
        /// </summary>
        System_Connect_Group = 100002,
        #endregion

        #region == 主程式DB ==
        /// <summary>
        /// Log紀錄
        /// </summary>
        Main_Log = 200000,
        /// <summary>
        /// 檔案庫
        /// </summary>
        Main_mFile = 200001,
        /// <summary>
        /// 公司
        /// </summary>
        Main_Company = 200002,
        /// <summary>
        /// 角色
        /// </summary>
        Main_Role = 200003,
        /// <summary>
        /// 權限
        /// </summary>
        Main_Function = 200004,
        /// <summary>
        /// 使用者
        /// </summary>
        Main_User = 200005,
        #endregion
    }
}
