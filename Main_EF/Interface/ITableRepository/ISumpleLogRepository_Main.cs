using Main_EF.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_EF.Interface.ITableRepository
{
    /// <summary>
    /// 【Main】【interface】Log Repository
    /// </summary>
    /// <remarks></remarks>
    public interface ISimpleLogRepository_Main : IGenericRepository<SimpleLog>
    {
        // 如非通用方法，加在這邊

        #region == 檢查相關 ==
        // ...
        #endregion

        #region == 取資料相關 ==
        // ...
        #endregion

        #region == 更新資料相關 ==
        // ...
        #endregion

        #region == 其他 ==
        // ...
        #endregion
    }
}
