using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ERP_EF.Models;

public partial class DB_T014Context : DbContext
{
    public DB_T014Context()
    {
    }

    public DB_T014Context(DbContextOptions<DB_T014Context> options)
        : base(options)
    {
    }

    public virtual DbSet<MF_BOM> MF_BOM { get; set; }

    public virtual DbSet<MF_MO> MF_MO { get; set; }

    public virtual DbSet<MF_TI> MF_TI { get; set; }

    public virtual DbSet<MF_TI_Z> MF_TI_Z { get; set; }

    public virtual DbSet<MF_TY> MF_TY { get; set; }

    public virtual DbSet<MF_TY_Z> MF_TY_Z { get; set; }

    public virtual DbSet<SPC_LST> SPC_LST { get; set; }

    public virtual DbSet<TF_MO> TF_MO { get; set; }

    public virtual DbSet<TF_TI> TF_TI { get; set; }

    public virtual DbSet<TF_TI_Z> TF_TI_Z { get; set; }

    public virtual DbSet<TF_TY> TF_TY { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    => optionsBuilder.UseSqlServer("Server=localhost; Database = DB_TEST; MultipleActiveResultSets = True; TrustServerCertificate = true; user = sa; password = Attn3100; "); //寬宇主機 ERP測試DB
    //=> optionsBuilder.UseSqlServer("Server=localhost; Database = DB_T4; MultipleActiveResultSets = True; TrustServerCertificate = true; user = sa; password = Attn3100; "); //寬宇主機正式DB
    //=> optionsBuilder.UseSqlServer("Server=192.168.6.211,2019; Database = DB_T014; MultipleActiveResultSets = True; TrustServerCertificate = true; user = sa; password = Attn3100; "); //192.168.6.211,2019 測試
    //=> optionsBuilder.UseSqlServer("Server=localhost; Database = DB_T014; MultipleActiveResultSets = True; TrustServerCertificate = true; user = sa; password = Attn3100; "); //本機localhost DB檔
    //=> optionsBuilder.UseSqlServer("Server=localhost,2019; Database = DB_T014; MultipleActiveResultSets = True; TrustServerCertificate = true; user = sa; password = Attn3100; "); //192.168.6.211,2019 本機localhost測試

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Compatibility_196_404_30001");

        modelBuilder.Entity<MF_BOM>(entity =>
        {
            entity.HasKey(e => e.BOM_NO).HasName("PK__MF_BOM");

            entity.HasIndex(e => e.DEP, "K_BOM_DEP");

            entity.HasIndex(e => e.PRD_NO, "K_BOM_PRD");

            entity.Property(e => e.BOM_NO)
                .HasMaxLength(38)
                .IsUnicode(false);
            entity.Property(e => e.BZ_KND)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CANCEL_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CHK_MAN)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.CLS_DATE).HasColumnType("datetime");
            entity.Property(e => e.CONTRACT)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.CPY_SW)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CST).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_FCP).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_MAKE).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_MAK_ML).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_MAN).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_MAN_ML).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_ML).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_OUT).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_OUT_ML).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_PRD).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_PRD_ML).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CUS_NO)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.DEP)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.DEPRO_NO)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.DEP_INC)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.DM_XY)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.EC_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.END_DD).HasColumnType("datetime");
            entity.Property(e => e.IDX_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LOCK_DATE).HasColumnType("datetime");
            entity.Property(e => e.LOCK_MAN)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.MOB_ID)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MODIFY_DD).HasColumnType("datetime");
            entity.Property(e => e.MODIFY_MAN)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.MOD_NO)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NAME)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PF_NO)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.PHOTO_BOM).HasColumnType("image");
            entity.Property(e => e.PRD_KND)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.PRD_MARK)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("(space((0)))");
            entity.Property(e => e.PRD_NO)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PRT_DATE).HasColumnType("datetime");
            entity.Property(e => e.PRT_SW)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.PRT_USR)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.QTY).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY1).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_C1).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_C2).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.REM).HasColumnType("text");
            entity.Property(e => e.SEB_NO)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SPC)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.SYS_DATE).HasColumnType("datetime");
            entity.Property(e => e.TIME_CNT).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.TREE_STRU).HasColumnType("image");
            entity.Property(e => e.UNIT)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.USED_TIME).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.USR)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.USR_NO)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.VALID_DD).HasColumnType("datetime");
            entity.Property(e => e.WH_NO)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.XX_XY)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MF_MO>(entity =>
        {
            entity.HasKey(e => e.MO_NO).HasName("PK__MF_MO");

            entity.HasIndex(e => new { e.SO_NO, e.EST_ITM }, "K_MF_MO_SO_ITM");

            entity.HasIndex(e => new { e.BIL_NO, e.BIL_ID }, "K_MO_BIL");

            entity.HasIndex(e => e.BJ_NO, "K_MO_BJ_NO");

            entity.HasIndex(e => new { e.MO_DD, e.DEP }, "K_MO_DD");

            entity.HasIndex(e => new { e.SO_NO, e.DEP }, "K_MO_SO_DEP");

            entity.HasIndex(e => new { e.MRP_NO, e.PRD_MARK, e.WH, e.BAT_NO }, "K_MRP_NO_WH");

            entity.Property(e => e.MO_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BACK_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.BAT_NO)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.BIL_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.BIL_MAK)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.BIL_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BIL_TYPE)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.BJ_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BUILD_BIL).HasColumnType("text");
            entity.Property(e => e.CANCEL_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CAS_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CF_DD).HasColumnType("datetime");
            entity.Property(e => e.CF_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CHK_MAN)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.CLOSE_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CLS_DATE).HasColumnType("datetime");
            entity.Property(e => e.CNTT_NO)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.CONTRACT)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.CONTROL)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CPY_SW)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CST).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_MAKE).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_MAK_ML).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_MAN).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_MAN_ML).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_ML).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_OUT).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_OUT_ML).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_PRD).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_PRD_ML).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CUS_NO)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.CUS_OS_NO)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CU_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CV_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.DEP)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.END_DD).HasColumnType("datetime");
            entity.Property(e => e.FIN_DD).HasColumnType("datetime");
            entity.Property(e => e.GRP_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ID_NO)
                .HasMaxLength(38)
                .IsUnicode(false);
            entity.Property(e => e.ISFROMQD)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.ISMATCHBIL)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.ISNORMAL)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.ISSVS)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.LOCK)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.LOCK_DATE).HasColumnType("datetime");
            entity.Property(e => e.LOCK_MAN)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.MD_NO)
                .HasMaxLength(400)
                .IsUnicode(false);
            entity.Property(e => e.ML_BY_MM)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.ML_OK)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.MM_CURML)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.MOB_ID)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MODIFY_DD).HasColumnType("datetime");
            entity.Property(e => e.MODIFY_MAN)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.MO_DD).HasColumnType("datetime");
            entity.Property(e => e.MO_NO_ADD)
                .HasMaxLength(38)
                .IsUnicode(false);
            entity.Property(e => e.MRP_NO)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.NEED_DD).HasColumnType("datetime");
            entity.Property(e => e.OLD_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.OPN_DD).HasColumnType("datetime");
            entity.Property(e => e.OUT_DD_MOJ).HasColumnType("datetime");
            entity.Property(e => e.PG_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.PO_OK)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.PRD_MARK)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("(space((0)))");
            entity.Property(e => e.PRT_DATE).HasColumnType("datetime");
            entity.Property(e => e.PRT_SW)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.PRT_USR)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.Q2_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Q3_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.QC_YN)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.QL_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.QTY).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY1).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_CHK).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_CHK_UNSH).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_DM).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_DM_UNSH).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_FIN)
                .HasDefaultValueSql("('0')")
                .HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_FIN_UNSH)
                .HasDefaultValueSql("('0')")
                .HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_LOST).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_LOST_UNSH).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_ML)
                .HasDefaultValueSql("('0')")
                .HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_ML_UNSH)
                .HasDefaultValueSql("('0')")
                .HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_PG).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_PG_UNSH).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_QL).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_QL_UNSH).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_QS).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_QS_UNSH).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_RK).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_RK_UNSH).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.REM).HasColumnType("text");
            entity.Property(e => e.SEB_NO)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SO_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.SO_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.STA_DD).HasColumnType("datetime");
            entity.Property(e => e.SUP_PRD_MARK)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SUP_PRD_NO)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.SYS_DATE).HasColumnType("datetime");
            entity.Property(e => e.TIME_AJ).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.TIME_CNT).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.TS_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.UNIT)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.USED_TIME).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.USR)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.WH)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.ZT_DD).HasColumnType("datetime");
            entity.Property(e => e.ZT_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MF_TI>(entity =>
        {
            entity.HasKey(e => new { e.TI_ID, e.TI_NO }).HasName("PK__MF_TI");

            entity.HasIndex(e => new { e.TI_ID, e.TI_DD, e.DEP }, "K_TI_DD");

            entity.Property(e => e.TI_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.TI_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BACK_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.BAT_NO)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.BIL_COMP)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.BIL_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.BIL_NO)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.BIL_TYPE)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CANCEL_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CHKTYPEID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CHKTY_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CHK_MAN)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.CLOSE_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CLS_DATE).HasColumnType("datetime");
            entity.Property(e => e.CNTT_NO)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.CPY_SW)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CUS_NO)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.CUS_OS_NO)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.DEP)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.FX_WH)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.LOCK_DATE).HasColumnType("datetime");
            entity.Property(e => e.LOCK_MAN)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.MOB_ID)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MODIFY_DD).HasColumnType("datetime");
            entity.Property(e => e.MODIFY_MAN)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.OS_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.OS_NO)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.PRT_DATE).HasColumnType("datetime");
            entity.Property(e => e.PRT_SW)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.PRT_USR)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.REM).HasColumnType("text");
            entity.Property(e => e.SAL_NO)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.SCAN_DATE).HasColumnType("datetime");
            entity.Property(e => e.SCAN_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.SCAN_USR)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.SYS_DATE).HasColumnType("datetime");
            entity.Property(e => e.TI_DD).HasColumnType("datetime");
            entity.Property(e => e.UP_DD_BIL)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.USR)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.WMS_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.ZJ_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MF_TI_Z>(entity =>
        {
            entity.HasKey(e => new { e.TI_ID, e.TI_NO }).HasName("PK__MF_TI_Z");

            entity.Property(e => e.TI_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.TI_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BBNUM)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MF_TY>(entity =>
        {
            entity.HasKey(e => new { e.TY_ID, e.TY_NO }).HasName("PK__MF_TY");

            entity.HasIndex(e => new { e.TY_ID, e.TY_DD, e.DEP }, "K_TY_DD");

            entity.Property(e => e.TY_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.TY_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ARP_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BIL_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.BIL_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BIL_TYPE)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CANCEL_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CHK_KND)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CHK_MAN)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.CLOSE_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CLS_DATE).HasColumnType("datetime");
            entity.Property(e => e.CLS_ID_LOST)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CLS_ID_OK)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CNTT_NO)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.CPY_SW)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CUR_ID)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.CUS_NO)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.CUS_OS_NO)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.DEP)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.EXC_RTO).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.FX_WH)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.LOCK_DATE).HasColumnType("datetime");
            entity.Property(e => e.LOCK_MAN)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.MOB_ID)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MODIFY_DD).HasColumnType("datetime");
            entity.Property(e => e.MODIFY_MAN)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.OLEFIELD)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PRD_LIST)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.PRE_CLS_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.PRT_DATE).HasColumnType("datetime");
            entity.Property(e => e.PRT_SW)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.PRT_USR)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.REM).HasColumnType("text");
            entity.Property(e => e.SAL_NO)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.SCAN_DATE).HasColumnType("datetime");
            entity.Property(e => e.SCAN_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.SCAN_USR)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.SL_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.SYS_DATE).HasColumnType("datetime");
            entity.Property(e => e.TAX_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.TB_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TI_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.TI_NO)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.TY_DD).HasColumnType("datetime");
            entity.Property(e => e.USR)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.VOH_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.VOH_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.WMS_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.ZHANG_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MF_TY_Z>(entity =>
        {
            entity.HasKey(e => new { e.TY_ID, e.TY_NO }).HasName("PK__MF_TY_Z");

            entity.Property(e => e.TY_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.TY_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BBNUM)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DDDDEDN).HasColumnType("datetime");
            entity.Property(e => e.DDDDGRE).HasColumnType("datetime");
            entity.Property(e => e.DDDDSTA).HasColumnType("datetime");
            entity.Property(e => e.PPPNUM)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RRR2)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SPC_LST>(entity =>
        {
            entity.HasKey(e => e.SPC_NO).HasName("PK__SPC_LST");

            entity.Property(e => e.SPC_NO)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NAME)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SPC_ITEM)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.SPC_NO_UP)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TF_MO>(entity =>
        {
            entity.HasKey(e => new { e.MO_NO, e.ITM }).HasName("PK__TF_MO");

            entity.HasIndex(e => new { e.MO_NO, e.EST_ITM }, "I_MO_NO_EST_ITM");

            entity.HasIndex(e => new { e.PRD_NO, e.WH }, "K_MO_PRD_WH");

            entity.Property(e => e.MO_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BAT_NO)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.CHG_RTO)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CHK_RTN)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CHK_XZL)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.COMPOSE_IDNO)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CPY_SW)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CQ_FLG)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CST).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_MAK).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_MAN).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_ML).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_OUT).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.CST_PRD).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.GRP_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ID_NO)
                .HasMaxLength(38)
                .IsUnicode(false);
            entity.Property(e => e.LOS_RTO).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.MD_NO)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PAK_EXC).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.PAK_GW).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.PAK_MEAST).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.PAK_MEAST_UNIT)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.PAK_NW).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.PAK_UNIT)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PAK_WEIGHT_UNIT)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.PRD_MARK)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("(space(0))");
            entity.Property(e => e.PRD_NAME)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PRD_NO)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PRD_NO_CHG)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.QC_FLAG)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.QTY)
                .HasDefaultValueSql("('0')")
                .HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY1_DM).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY1_LOST).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY1_QL).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY1_RSV).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_BAS).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_BL).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_BL_UNSH).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_BOM).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_CHG_RTO).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_DM).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_DM_UNSH).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_LOST).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_QL).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_QL_UNSH).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_QL_YL).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_QL_YL_UNSH).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_RSV).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_STD).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_TS)
                .HasDefaultValueSql("('0')")
                .HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_TS_UNSH)
                .HasDefaultValueSql("('0')")
                .HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_UNSH)
                .HasDefaultValueSql("('0')")
                .HasColumnType("numeric(22, 8)");
            entity.Property(e => e.REM)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SEB_NO)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TW_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.UNIT)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.USEIN)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.USEIN_NO)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.WH)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.ZC_NO)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ZC_PRD)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ZC_REM).HasColumnType("text");
            entity.Property(e => e.ZZ_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ZZ_NO_CHD)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.MO_NONavigation).WithMany(p => p.TF_MO)
                .HasForeignKey(d => d.MO_NO)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TF_MO_MF_MO");
        });

        modelBuilder.Entity<TF_TI>(entity =>
        {
            entity.HasKey(e => new { e.TI_ID, e.TI_NO, e.ITM }).HasName("PK__TF_TI");

            entity.HasIndex(e => new { e.BIL_ID, e.BIL_NO }, "K_BIL_ID");

            entity.HasIndex(e => new { e.TI_NO, e.PRE_ITM, e.TI_ID }, "K_OTH_PRE_ITM");

            entity.HasIndex(e => e.BIL_NO, "K_TF_TI_BAT");

            entity.HasIndex(e => new { e.PRD_NO, e.PRD_MARK, e.WH, e.BAT_NO }, "K_TF_TI_PRD");

            entity.Property(e => e.TI_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.TI_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BAT_NO)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.BIL_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.BIL_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.B_DD).HasColumnType("datetime");
            entity.Property(e => e.CAS_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CHKTY_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CNTT_NO)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.CNT_FLAG)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CNT_NEED)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.COMPOSE_IDNO)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CPY_SW)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CUS_NO)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.CUS_OS_NO)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.DEP_RK)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.E_DD).HasColumnType("datetime");
            entity.Property(e => e.FREE_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.FX_KND)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FX_NAME)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.FX_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FX_SPC).HasColumnType("text");
            entity.Property(e => e.FX_UNIT)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.GF_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ID_NO)
                .HasMaxLength(38)
                .IsUnicode(false);
            entity.Property(e => e.IS_SP)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.MM_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NEWMAT_FLAG)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.PRD_MARK)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("(space(0))");
            entity.Property(e => e.PRD_NAME)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PRD_NO)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PRODU_DD).HasColumnType("datetime");
            entity.Property(e => e.QTY).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY1).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY1_PS).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY1_PS_UNSH).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY1_RCK).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY1_RCK_UNSH).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY1_RTN).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY1_RTN_UNSH).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_CUS).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_PS).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_PS_UNSH).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_RCK).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_RCK_UNSH).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_RK).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_RTN).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_RTN_UNSH).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.REM)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.RK_DD).HasColumnType("datetime");
            entity.Property(e => e.SC_DD).HasColumnType("datetime");
            entity.Property(e => e.SH_NO_CUS)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SL_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.SL_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SUP_PRD_MARK)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SUP_PRD_NO)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.TRD_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.TRD_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UNIT)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.UP_DD_BIL)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.USED_TIME).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.VALID_DD).HasColumnType("datetime");
            entity.Property(e => e.WH)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.XRF_QCFLAG)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.XRF_SAMPLE)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.YH_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ZC_NO)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.TI_).WithMany(p => p.TF_TI)
                .HasForeignKey(d => new { d.TI_ID, d.TI_NO })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TF_TI_MF_TI");
        });

        modelBuilder.Entity<TF_TI_Z>(entity =>
        {
            entity.HasKey(e => new { e.TI_ID, e.TI_NO, e.ITM }).HasName("PK__TF_TI_Z");

            entity.Property(e => e.TI_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.TI_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RRR2)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TF_TY>(entity =>
        {
            entity.HasKey(e => new { e.TY_ID, e.TY_NO, e.ITM }).HasName("PK__TF_TY");

            entity.HasIndex(e => new { e.TI_NO, e.TI_ITM }, "K_TI_NO_ITM");

            entity.Property(e => e.TY_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.TY_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AMT).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.AMTN).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.AMT_DIS_CNT).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.AUTO_MV)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.BAT_NO)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.BIL_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BUILD_BIL)
                .HasMaxLength(26)
                .IsUnicode(false);
            entity.Property(e => e.BUILD_BIL_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.CAS_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CNTT_NO)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.COMPOSE_IDNO)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CPY_SW)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CUS_OS_NO)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.DEP_RK)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.EST_BUILD_BIL)
                .HasMaxLength(26)
                .IsUnicode(false);
            entity.Property(e => e.FREE_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.FX_NAME)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.FX_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.GF_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ID_NO)
                .HasMaxLength(38)
                .IsUnicode(false);
            entity.Property(e => e.LS_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MM_ID)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.MM_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MO_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MVTO_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PC_NO)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PRC_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.PRD_MARK)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("(space(0))");
            entity.Property(e => e.PRD_NAME)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PRD_NO)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PRODU_DD).HasColumnType("datetime");
            entity.Property(e => e.QC_UP).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY1_CHK).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY1_LOST).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY1_OK).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_CHK).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_LOST).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_LOST_RTN).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_LOST_RTN_UNSH).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_OK).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_OK_RTN).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_OK_RTN_UNSH).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_PRE).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_PRE_UNSH).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_QC).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.QTY_RTN).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.RECHK_DD).HasColumnType("datetime");
            entity.Property(e => e.REM).HasColumnType("text");
            entity.Property(e => e.RK_DD).HasColumnType("datetime");
            entity.Property(e => e.RK_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RTN_ID)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.SH_NO_CUS)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SL_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SPC_NO)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TAX).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.TAX_RTO).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.TI_NO)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UNIT)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.UP).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.USED_TIME).HasColumnType("numeric(22, 8)");
            entity.Property(e => e.VALID_DD).HasColumnType("datetime");
            entity.Property(e => e.VALID_DD_NEW).HasColumnType("datetime");
            entity.Property(e => e.WH)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.ZC_NO)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.TY_).WithMany(p => p.TF_TY)
                .HasForeignKey(d => new { d.TY_ID, d.TY_NO })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TF_TY_MF_TY");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
