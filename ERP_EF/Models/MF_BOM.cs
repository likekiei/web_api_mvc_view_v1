using System;
using System.Collections.Generic;

namespace ERP_EF.Models;

public partial class MF_BOM
{
    public string BOM_NO { get; set; } = null!;

    public string? NAME { get; set; }

    public string? PRD_NO { get; set; }

    public string? PRD_MARK { get; set; }

    public string? PF_NO { get; set; }

    public string? WH_NO { get; set; }

    public string? PRD_KND { get; set; }

    public string? UNIT { get; set; }

    public decimal? QTY { get; set; }

    public decimal? QTY1 { get; set; }

    public decimal? CST_MAKE { get; set; }

    public decimal? CST_PRD { get; set; }

    public decimal? CST_MAN { get; set; }

    public decimal? CST_OUT { get; set; }

    public decimal? USED_TIME { get; set; }

    public decimal? CST { get; set; }

    public string? USR_NO { get; set; }

    public byte[]? TREE_STRU { get; set; }

    public string? DEP { get; set; }

    public byte[]? PHOTO_BOM { get; set; }

    public string? EC_NO { get; set; }

    public DateTime? VALID_DD { get; set; }

    public DateTime? END_DD { get; set; }

    public string? REM { get; set; }

    public string? USR { get; set; }

    public string? CHK_MAN { get; set; }

    public string? PRT_SW { get; set; }

    public string? CPY_SW { get; set; }

    public DateTime? CLS_DATE { get; set; }

    public string? MOB_ID { get; set; }

    public string? LOCK_MAN { get; set; }

    public DateTime? LOCK_DATE { get; set; }

    public string? SEB_NO { get; set; }

    public string? MOD_NO { get; set; }

    public decimal? TIME_CNT { get; set; }

    public string? PRT_USR { get; set; }

    public string? DEP_INC { get; set; }

    public string? SPC { get; set; }

    public DateTime? SYS_DATE { get; set; }

    public string? BZ_KND { get; set; }

    public decimal? CST_FCP { get; set; }

    public string? CANCEL_ID { get; set; }

    public string? DEPRO_NO { get; set; }

    public string? CUS_NO { get; set; }

    public DateTime? PRT_DATE { get; set; }

    public decimal? QTY_C1 { get; set; }

    public decimal? QTY_C2 { get; set; }

    public DateTime? MODIFY_DD { get; set; }

    public string? MODIFY_MAN { get; set; }

    public string? IDX_NO { get; set; }

    public string? CONTRACT { get; set; }

    public string? DM_XY { get; set; }

    public string? XX_XY { get; set; }

    public decimal? CST_MAN_ML { get; set; }

    public decimal? CST_MAK_ML { get; set; }

    public decimal? CST_PRD_ML { get; set; }

    public decimal? CST_OUT_ML { get; set; }

    public decimal? CST_ML { get; set; }
}
