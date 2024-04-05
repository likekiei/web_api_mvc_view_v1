using System;
using System.Collections.Generic;

namespace ERP_EF.Models;

public partial class TF_TY
{
    public string TY_ID { get; set; } = null!;

    public string TY_NO { get; set; } = null!;

    public int ITM { get; set; }

    public string? PRD_NO { get; set; }

    public string? PRD_NAME { get; set; }

    public string? PRD_MARK { get; set; }

    public string? WH { get; set; }

    public string? UNIT { get; set; }

    public decimal? QTY_CHK { get; set; }

    public decimal? QTY_OK { get; set; }

    public decimal? QTY_LOST { get; set; }

    public decimal? QTY_RTN { get; set; }

    public string? REM { get; set; }

    public string? BIL_NO { get; set; }

    public string? TI_NO { get; set; }

    public string? BAT_NO { get; set; }

    public string? MO_NO { get; set; }

    public string? CPY_SW { get; set; }

    public string? SPC_NO { get; set; }

    public string? PRC_ID { get; set; }

    public string? BUILD_BIL { get; set; }

    public string? BUILD_BIL_ID { get; set; }

    public int? EST_ITM { get; set; }

    public string? MM_NO { get; set; }

    public int? PRE_ITM { get; set; }

    public decimal? QTY_PRE { get; set; }

    public decimal? QTY_PRE_UNSH { get; set; }

    public string? ZC_NO { get; set; }

    public string? ID_NO { get; set; }

    public decimal? QC_UP { get; set; }

    public decimal? QTY1_CHK { get; set; }

    public decimal? QTY1_OK { get; set; }

    public decimal? QTY1_LOST { get; set; }

    public string? EST_BUILD_BIL { get; set; }

    public string? FREE_ID { get; set; }

    public int? CK_ITM { get; set; }

    public string? RK_NO { get; set; }

    public string? COMPOSE_IDNO { get; set; }

    public DateTime? VALID_DD { get; set; }

    public string? SL_NO { get; set; }

    public int? TI_ITM { get; set; }

    public string? SH_NO_CUS { get; set; }

    public string? AUTO_MV { get; set; }

    public string? MVTO_NO { get; set; }

    public decimal? QTY_OK_RTN { get; set; }

    public decimal? QTY_OK_RTN_UNSH { get; set; }

    public decimal? QTY_LOST_RTN { get; set; }

    public decimal? QTY_LOST_RTN_UNSH { get; set; }

    public string? MM_ID { get; set; }

    public string? RTN_ID { get; set; }

    public int? KEY_ITM { get; set; }

    public string? CAS_NO { get; set; }

    public int? TASK_ID { get; set; }

    public decimal? USED_TIME { get; set; }

    public string? GF_NO { get; set; }

    public string? CUS_OS_NO { get; set; }

    public decimal? AMTN { get; set; }

    public decimal? UP { get; set; }

    public decimal? TAX { get; set; }

    public decimal? TAX_RTO { get; set; }

    public decimal? AMT { get; set; }

    public string? PC_NO { get; set; }

    public decimal? QTY_QC { get; set; }

    public DateTime? RK_DD { get; set; }

    public string? DEP_RK { get; set; }

    public DateTime? VALID_DD_NEW { get; set; }

    public string? CNTT_NO { get; set; }

    public string? LS_NO { get; set; }

    public decimal? AMT_DIS_CNT { get; set; }

    public DateTime? RECHK_DD { get; set; }

    public DateTime? PRODU_DD { get; set; }

    public string? FX_NO { get; set; }

    public string? FX_NAME { get; set; }

    public int? HFSH_ITM { get; set; }

    public virtual MF_TY TY_ { get; set; } = null!;
}
