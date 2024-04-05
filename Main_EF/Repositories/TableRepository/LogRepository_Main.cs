using Main_EF.Interface.ITableRepository;
using Main_EF.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_EF.Repositories.TableRepository
{
    /// <summary>
    /// 【Main】Log Repository
    /// </summary>
    public class LogRepository_Main : GenericRepository_Main<Log>, ILogRepository_Main
    {
        public LogRepository_Main(DbContext_Main dbContext) : base(dbContext)
        {

        }

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
