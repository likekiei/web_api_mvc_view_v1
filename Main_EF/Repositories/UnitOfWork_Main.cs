using Main_EF.Interface.ITableRepository;
using Main_EF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Main_Common.Model.Data.Connect;
using Main_Common.Model.Result;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Main_EF.Repositories.TableRepository;
using Main_EF.Table;
using Main_Common.Enum;

namespace Main_EF.Repositories
{
    /// <summary>
    /// 【Main】資料庫工作單元
    /// </summary>
    public class UnitOfWork_Main : IUnitOfWork
    {
        #region == 【全域宣告】 ==
        /// <summary>
        /// Db Context
        /// </summary>
        private readonly DbContext_Main _dbContext;

        /// <summary>
        /// TestTemplate Repository
        /// </summary>
        public ITestTemplateRepository _TestTemplateRepository { get; }
        /// <summary>
        /// 公司 Repository
        /// </summary>
        public ICompanyRepository _CompanyRepository { get; }
        /// <summary>
        /// 功能代碼 Repository
        /// </summary>
        public IFunctionCodeRepository _FunctionCodeRepository { get; }
        /// <summary>
        /// 登入狀態 Repository
        /// </summary>
        public ILoginStatusRepository_Main _LoginStatusRepository_Main { get; }
        /// <summary>
        /// Log Repository
        /// </summary>
        public ILogRepository_Main _LogRepository_Main { get; }
        /// <summary>
        /// Log Repository
        /// </summary>
        public ISimpleLogRepository_Main _SimpleLogRepository_Main { get; }
        /// <summary>
        /// 公司 Repository
        /// </summary>
        public IRoleRepository _RoleRepository { get; }
        /// <summary>
        /// 使用者 Repository
        /// </summary>
        public IUserRepository _UserRepository { get; }
        #endregion

        #region == 建構 ==
        /// <summary>
        /// 建構
        /// </summary>
        /// <param name="dbContext">Db Context</param>
        /// <param name="testTemplateRepository">TestTemplate Repository</param>
        /// <param name="companyRepository">公司 Repository</param>
        /// <param name="functionCodeRepository">功能代碼 Repository</param>
        /// <param name="logRepository_Main">Log Repository</param>
        /// <param name="loginStatusRepository_Main">登入狀態 Repository</param>
        /// <param name="roleRepository">角色 Repository</param>
        /// <param name="userRepository">使用者 Repository</param>
        public UnitOfWork_Main(DbContext_Main dbContext,
                            ITestTemplateRepository testTemplateRepository,
                            ICompanyRepository companyRepository,
                            IFunctionCodeRepository functionCodeRepository,
                            ILogRepository_Main logRepository_Main,
                            ISimpleLogRepository_Main simpleLogRepository_Main,
                            ILoginStatusRepository_Main loginStatusRepository_Main,
                            IRoleRepository roleRepository,
                            IUserRepository userRepository)
        {
            _dbContext = dbContext;
            _TestTemplateRepository = testTemplateRepository;
            _CompanyRepository = companyRepository;
            _FunctionCodeRepository = functionCodeRepository;
            _LogRepository_Main = logRepository_Main;
            _SimpleLogRepository_Main = simpleLogRepository_Main;
            _LoginStatusRepository_Main = loginStatusRepository_Main;
            _RoleRepository = roleRepository;
            _UserRepository = userRepository;
        }
        #endregion

        #region == 處理 ==
        /// <summary>
        /// 存儲 (實際執行DB寫入)
        /// </summary>
        /// <returns></returns>
        /// <remarks>返回值表示已影響的資料庫項目數量</remarks>
        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 取DbContext
        /// </summary>
        /// <returns></returns>
        public DbContext_Main Get_DB()
        {
            return _dbContext;
        }

        /// <summary>
        /// 取DbContext
        /// </summary>
        /// <returns></returns>
        public DbContext_Main Get_DbContext()
        {
            return _dbContext;
        }

        /// <summary>
        /// 檢查DB連線
        /// </summary>
        /// <param name="isUpdateMigrations">是否為資料庫移轉的檢查</param>
        /// <returns></returns>
        public ResultSimple Check_DbLink(bool isUpdateMigrations)
        {
            ResultSimple result = new ResultSimple(true);
            // 取DB名稱
            var dbName = _dbContext.Database.GetDbConnection().Database;
            // 連線測試
            var check = _dbContext.Database.CanConnect();
            // [T：連線成功][F：連線失敗]
            if (check)
            {
                try
                {
                    // [T：不是資料庫移轉，才做取資料檢查]
                    if (!isUpdateMigrations)
                    {
                        var testData = _dbContext._TestTemplate.ToList(); //測試取資料
                    }
                }
                catch (Exception ex)
                {
                    return new ResultSimple(false, $"[{dbName}]取資料測試失敗");
                }

            }
            else
            {
                return new ResultSimple(false, $"[{dbName}]連線失敗");
            }

            result.Message = $"[{dbName}]連線成功";
            return result; //沒有Error，才會到這
        }

        /// <summary>
        /// 變更DbContext
        /// </summary>
        /// <param name="connectDTO">連線資訊</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>目前專案沒有使用的打算，所以只測試到切換這件事能執行，但沒有嘗試更全面的測試。</para>
        /// <para>故未來如果有客戶需要使用到DB切換的功能，請自行完善相關功能。</para>
        /// </remarks>
        public ResultSimple Change_DB(Connect_DTO connectDTO)
        {
            ResultSimple result = new ResultSimple(true);

            //connectDTO = new Connect_DTO
            //{
            //    Path = "192.168.6.211,2019",
            //    Catalog = "C_TK_WorkManageSystem_Core001",
            //    User = "sa",
            //    Password = "Attn3100",
            //};

            try
            {
                _dbContext.ChangeDatabase(
                    initialCatalog: connectDTO.Catalog,
                    dataSource: connectDTO.Path,
                    userId: connectDTO.User,
                    password: connectDTO.Password);
            }
            catch (Exception)
            {
                result = new ResultSimple(false, "資料庫切換異常");
                return result;
            }

            return result;
        }

        /// <summary>
        /// 更新資料庫移轉
        /// </summary>
        /// <param name="targetMigrationVersion">指定移轉目標版本(如果沒給，則移轉至最新)</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>僅移轉當前DbContext，不切換DB。</para>
        /// <para>如果要切換DB，請自行完善相關功能。</para>
        /// </remarks>
        /// <returns></returns>
        public ResultSimple Update_DbMigration(string? targetMigrationVersion)
        {
            var result = new ResultSimple(true);

            #region == DB連線測試 ==
            var dbLinkCheck = this.Check_DbLink(true);
            if (dbLinkCheck.IsSuccess == false)
            {
                result = dbLinkCheck;
                return result;
            }
            #endregion

            #region == 資料庫移轉 ==
            try
            {
                // [有無傳入移轉目標][T：無，移轉至最新][F：有，指定移轉]
                if (string.IsNullOrEmpty(targetMigrationVersion))
                {
                    _dbContext.Database.Migrate();
                    //using (var context = new _dbContext())
                    {
                        //context.Database.EnsureCreated();

                        
                    }
                }
                else
                {
                    _dbContext.Database.GetService<IMigrator>().Migrate(targetMigrationVersion);
                }
            }
            catch (Exception)
            {
                result = new ResultSimple(false, "資料庫移轉失敗");
                return result;
            }
            #endregion

            result.Message = "移轉成功";
            return result;
        }
        #endregion

        #region == Dispose ==
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        #endregion
    }
}
