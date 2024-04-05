//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Main_Common.Enum.E_ProjectType
//{
//    /// <summary>
//    /// 【初始建構用】【不允許刪除】【未來以DB上的資料為主】【應該會可以增刪修】角色
//    /// </summary>
//    public enum E_Role
//    {
//        // 不要亂改，此為默認基本權限，要跟資料庫對照。
//        // 這邊有的，資料庫一定要有。
//        // 資料庫有的，這邊不一定會有。
//        // 考慮到未來如果開放使用者自行增減角色，故請以資料庫之角色為主，但這邊的基本角色還是能用的。

//        /// <summary>
//        /// 無權限
//        /// </summary>
//        無權限 = 0,
//        /// <summary>
//        /// 一般使用者
//        /// </summary>
//        一般使用者 = 1,
//        /// <summary>
//        /// 公司使用者
//        /// </summary>
//        公司使用者 = 2,
//        /// <summary>
//        /// 公司管理者
//        /// </summary>
//        公司管理者 = 3,
//        /// <summary>
//        /// 連線設定權限
//        /// </summary>
//        //連線設定 = 990,
//        /// <summary>
//        /// 最高權限
//        /// </summary>
//        Admin = 900,
//        /// <summary>
//        /// 後門最高權限
//        /// </summary>
//        AdminBackDoor = 999,
//    }
//}
