using Main_Common.Model.Data.Connect;
using Main_Common.Model.Result;
using Main_EF.Interface.ITableRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_EF.Interface
{
    /// <summary>
    /// 【Main】【interface】資料庫工作單元
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        #region == Table Repository ==
        ITestTemplateRepository _TestTemplateRepository { get; }
        ICompanyRepository _CompanyRepository { get; }
        IFunctionCodeRepository _FunctionCodeRepository { get; }
        ILoginStatusRepository_Main _LoginStatusRepository_Main { get; }
        ILogRepository_Main _LogRepository_Main { get; }
        ISimpleLogRepository_Main _SimpleLogRepository_Main { get; }
        IRoleRepository _RoleRepository { get; }
        IUserRepository _UserRepository { get; }
        #endregion

        #region == 方法 ==
        /// <summary>
        /// 存儲 (實際執行DB寫入)
        /// </summary>
        /// <returns></returns>
        /// <remarks>返回值表示已影響的資料庫項目數量</remarks>
        int Save();

        /// <summary>
        /// 取DbContext
        /// </summary>
        /// <returns></returns>
        DbContext_Main Get_DB();

        /// <summary>
        /// 檢查DB連線
        /// </summary>
        /// <param name="isUpdateMigrations">是否為資料庫移轉的檢查</param>
        /// <returns></returns>
        ResultSimple Check_DbLink(bool isUpdateMigrations);

        /// <summary>
        /// 變更DbContext
        /// </summary>
        /// <param name="connectDTO">連線資訊</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>目前專案沒有使用的打算，所以只測試到切換這件事能執行，但沒有嘗試更全面的測試。</para>
        /// <para>故未來如果有客戶需要使用到DB切換的功能，請自行完善相關功能。</para>
        /// </remarks>
        ResultSimple Change_DB(Connect_DTO connectDTO);

        /// <summary>
        /// 更新資料庫移轉
        /// </summary>
        /// <param name="targetMigrationVersion">指定移轉目標版本。 (如果沒給，則移轉至最新)</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>僅移轉當前DbContext，不切換DB。</para>
        /// <para>如果要切換DB，請自行完善相關功能。</para>
        /// </remarks>
        /// <returns></returns>
        ResultSimple Update_DbMigration(string? targetMigrationVersion);
        #endregion
    }
}
