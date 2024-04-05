using Main_EF.Table;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Main_EF
{
    /// <summary>
    /// EF DbContext
    /// </summary>
    public class DbContext_Main : DbContext
    {
        // 在DI注入時一起設定連線字串

        public DbContext_Main(DbContextOptions<DbContext_Main> contextOptions) : base(contextOptions)
        {
            
        }

        #region == Model Builder Setting ==
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer(
            //    @"Server=192.168.6.211,2019;Database=Snow_UnitOfWork_Demo1;Trusted_Connection=True;user id=sa;password=Attn3100;TrustServerCertificate=true;Integrated Security=False");
            ////optionsBuilder.UseSqlServer("Server=192.168.6.212;Database=RD_KCSC;MultipleActiveResultSets=True;user id=sa;password=Attn3100;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region == 設定-欄位預設值 ==
            modelBuilder.Entity<Companys>().Property(p => p.GUID).HasDefaultValueSql("newsequentialid()");
            modelBuilder.Entity<FunctionCode>().Property(p => p.GUID).HasDefaultValueSql("newsequentialid()");
            modelBuilder.Entity<Log>().Property(p => p.Id).HasDefaultValueSql("newsequentialid()");
            modelBuilder.Entity<LoginStatus>().Property(p => p.Id).HasDefaultValueSql("newsequentialid()");
            modelBuilder.Entity<Role>().Property(p => p.GUID).HasDefaultValueSql("newsequentialid()");
            modelBuilder.Entity<User>().Property(p => p.GUID).HasDefaultValueSql("newsequentialid()");
            #endregion

            #region == 設定-關聯性執行動作 ==
            #region == 【L1】範本 ==
            // 關聯欄位好像要[not null]，才能處理關聯性刪除。

            //// 範本，1對1
            //// 設定-[XXX]連帶刪除[XXX] (1對1)(A是主體，B依賴於A)
            //modelBuilder.Entity<Companys>() // 要設定的Model[稱為A](主體)
            //    .HasOne(t => t.SystemTimestampInfo) // 設定A的關聯子項
            //    .WithOne(t => t.CompanyInfo) // 設定B的依賴對象
            //    .HasForeignKey<SystemTimestamp>(x => x.CompanyId) // 設定關聯子項[稱為B]的外建(FK)
            //    .OnDelete(DeleteBehavior.Cascade); // NoAction:關閉關聯刪除。  Cascade:啟動關聯刪除(預設默認為啟動)

            //// 範本，1對多 (別人影響自己)
            //// 設定 -[XXX]連帶刪除[XXX](1對多)
            //modelBuilder.Entity<LoginStatus>() // 要設定的Model[稱為A]
            //    .HasOne(e => e.User_Info) // 哪個Model刪除時[稱為B]，會影響到自己[A]
            //    .WithMany(d => d.LoginStatus_List) // [B]要影響的Model[A]
            //    .HasForeignKey(e => e.User_Id) // [A]的關聯Key
            //    .OnDelete(DeleteBehavior.NoAction); // NoAction:關閉關聯刪除。  Cascade:啟動關聯刪除(預設默認為啟動)

            //// 範本，1對多 (自己影響別人)
            //// 設定 -[XXX]連帶刪除[XXX](1對多)
            //modelBuilder.Entity<Companys>() // 要影響別人的Model[稱為A]
            //    .HasMany(e => e.UserInfos) // [A]要影響的Model[稱為B]
            //    .WithOne(d => d.CompanyInfo) // [A]刪除時，會影響到[B]
            //    .HasForeignKey(e => e.Company_Id) // [B]的關聯Key
            //    .OnDelete(DeleteBehavior.NoAction); // 關閉關聯刪除
            #endregion

            #region == 【L1】公司-連帶刪除 ==
            // 設定-[公司]連帶刪除[系統時間戳記] (1對1)
            modelBuilder.Entity<Companys>()
                .HasOne(t => t.SystemTimestampInfo)
                .WithOne(t => t.CompanyInfo)
                .HasForeignKey<SystemTimestamp>(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade); // 啟動關聯刪除

            // 設定-[公司]連帶刪除[角色] (1對多)
            modelBuilder.Entity<Role>()
                .HasOne(e => e.CompanyInfo)
                .WithMany()
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.NoAction); // 關閉關聯刪除

            // 設定-[公司]連帶刪除[功能代碼] (1對多)
            modelBuilder.Entity<FunctionCode>()
                .HasOne(e => e.CompanyInfo)
                .WithMany()
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.NoAction); // 關閉關聯刪除

            // 設定-[公司]連帶刪除[使用者] (1對多)
            modelBuilder.Entity<User>()
                .HasOne(e => e.CompanyInfo)
                .WithMany(d => d.UserList)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.NoAction); // 關閉關聯刪除
            #endregion

            #region == 【L1】角色-連帶刪除 ==
            // 設定-[角色]連帶刪除[使用者] (1對多)
            modelBuilder.Entity<User>()
                .HasOne(e => e.RoleInfo)
                .WithMany(d => d.UserList)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.NoAction); // 關閉關聯刪除

            // 設定-[角色]連帶刪除[功能代碼] (1對多)
            modelBuilder.Entity<FunctionCode>()
                .HasOne(e => e.RoleInfo)
                .WithMany(d => d.FunctionCodeList)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Cascade); // 啟用關聯刪除
            #endregion

            #region == 【L1】使用者-連帶刪除 ==
            //// [使用者]關閉關聯刪除[登入狀態]
            //modelBuilder.Entity<LoginStatus>()
            //    .HasOne(e => e.User_Info)
            //    .WithMany(d => d.LoginStatus_List)
            //    .HasForeignKey(e => e.User_Id)
            //    .OnDelete(DeleteBehavior.NoAction);
            #endregion
            #endregion
        }
        #endregion

        #region == Table ==
        /// <summary>
        /// Test
        /// </summary>
        public DbSet<_TestTemplate> _TestTemplate { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public virtual DbSet<Companys> Companys { get; set; }
        /// <summary>
        /// 功能代碼
        /// </summary>
        public virtual DbSet<FunctionCode> FunctionCode { get; set; }
        /// <summary>
        /// Log紀錄
        /// </summary>
        public virtual DbSet<Log> Log { get; set; }
        /// <summary>
        /// 登入狀態
        /// </summary>
        public virtual DbSet<LoginStatus> LoginStatus { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public virtual DbSet<Role> Role { get; set; }
        /// <summary>
        /// 系統時間戳記
        /// </summary>
        public virtual DbSet<SystemTimestamp> SystemTimestamp { get; set; }
        /// <summary>
        /// 使用者
        /// </summary>
        public virtual DbSet<User> User { get; set; }
        /// <summary>
        /// 簡易Log紀錄
        /// </summary>
        public virtual DbSet<SimpleLog> SimpleLog { get; set; }
        /// <summary>
        /// Log紀錄
        /// </summary>
        public virtual DbSet<ErrorLog> ErrorLog { get; set; }
        #endregion
    }
}
