using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Enum.E_ProjectType
{
    /// <summary>
    /// 權限種類
    /// </summary>
    public enum E_PermissionType
    {
        /// <summary>
        /// 無權限
        /// </summary>
        無權限 = 0,
        /// <summary>
        /// 一般使用者
        /// </summary>
        一般使用者 = 1,
        /// <summary>
        /// 公司使用者
        /// </summary>
        公司使用者 = 2,
        /// <summary>
        /// 公司管理者
        /// </summary>
        公司管理者 = 3,
        /// <summary>
        /// 連線設定權限
        /// </summary>
        //連線設定 = 990,
        /// <summary>
        /// 最高權限
        /// </summary>
        Admin = 900,
        /// <summary>
        /// 後門最高權限
        /// </summary>
        AdminBackDoor = 999,
    }
}
