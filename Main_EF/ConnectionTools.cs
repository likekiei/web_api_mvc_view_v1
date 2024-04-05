using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_EF
{
    /// <summary>
    /// DbContext的擴充功能
    /// </summary>
    public static class ConnectionTools
    {
        #region == 方法 ==
        /// <summary>
        /// 變更連線資訊
        /// </summary>
        /// <param name="dbContext">DbContext</param>
        /// <param name="initialCatalog">資料庫名稱</param>
        /// <param name="dataSource">資料庫路徑</param>
        /// <param name="userId">帳號</param>
        /// <param name="password">密碼</param>
        public static void ChangeDatabase(this DbContext dbContext,
            string initialCatalog = "",
            string dataSource = "",
            string userId = "",
            string password = "")
        {
            try
            {
                // 取原DbContext的連線字串
                var orgConnectionString = dbContext.Database.GetDbConnection().ConnectionString;

                // 連線字串Builder
                var sqlCnxStringBuilder = new SqlConnectionStringBuilder(orgConnectionString);

                // 設定值
                if (!string.IsNullOrEmpty(initialCatalog))
                    sqlCnxStringBuilder.InitialCatalog = initialCatalog;
                if (!string.IsNullOrEmpty(dataSource))
                    sqlCnxStringBuilder.DataSource = dataSource;
                if (!string.IsNullOrEmpty(userId))
                    sqlCnxStringBuilder.UserID = userId;
                if (!string.IsNullOrEmpty(password))
                    sqlCnxStringBuilder.Password = password;

                // 變更DbContext的連線字串
                dbContext.Database.SetConnectionString(sqlCnxStringBuilder.ConnectionString);
            }
            catch (Exception ex)
            {
                // set log item if required
                throw;
            }
        }
        #endregion
    }
}
