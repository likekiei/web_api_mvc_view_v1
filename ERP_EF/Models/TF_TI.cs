using System;
using System.Collections.Generic;

namespace ERP_EF.Models;

public partial class TF_TI
{
    public string TI_ID { get; set; } = null!;

    public string TI_NO { get; set; } = null!;

    public int ITM { get; set; }

    public string? PRD_NO { get; set; }

    public string? PRD_NAME { get; set; }

    public string? PRD_MARK { get; set; }

    public string? WH { get; set; }

    public string? UNIT { get; set; }

    public decimal? QTY { get; set; }

    public string? BIL_ID { get; set; }

    public string? BIL_NO { get; set; }

    public string? BAT_NO { get; set; }

    public string? REM { get; set; }

    public decimal? QTY_RTN { get; set; }

    public decimal? QTY_RTN_UNSH { get; set; }

    public string? CPY_SW { get; set; }

    public int? EST_ITM { get; set; }

    public decimal? QTY_RK { get; set; }

    public string? ID_NO { get; set; }

    public string? ZC_NO { get; set; }

    public string? CUS_NO { get; set; }

    public decimal? QTY1 { get; set; }

    public decimal? QTY1_RTN { get; set; }

    public decimal? QTY1_RTN_UNSH { get; set; }

    public string? FREE_ID { get; set; }

    public int? CK_ITM { get; set; }

    public string? SUP_PRD_NO { get; set; }

    public string? COMPOSE_IDNO { get; set; }

    public DateTime? B_DD { get; set; }

    public DateTime? E_DD { get; set; }

    public string? SL_NO { get; set; }

    public string? MM_NO { get; set; }

    public string? CUS_OS_NO { get; set; }

    public DateTime? VALID_DD { get; set; }

    public int? PRE_ITM { get; set; }

    public string? SH_NO_CUS { get; set; }

    public decimal? QTY_PS { get; set; }

    public decimal? QTY_PS_UNSH { get; set; }

    public string? CAS_NO { get; set; }

    public int? TASK_ID { get; set; }

    public decimal? USED_TIME { get; set; }

    public int? SL_ITM { get; set; }

    public string? GF_NO { get; set; }

    public decimal? QTY_CUS { get; set; }

    public DateTime? RK_DD { get; set; }

    public string? DEP_RK { get; set; }

    public string? CHKTY_ID { get; set; }

    public string? CNTT_NO { get; set; }

    public DateTime? PRODU_DD { get; set; }

    public decimal? QTY_RCK { get; set; }

    public decimal? QTY_RCK_UNSH { get; set; }

    public string? TRD_ID { get; set; }

    public string? TRD_NO { get; set; }

    public string? NEWMAT_FLAG { get; set; }

    public string? XRF_QCFLAG { get; set; }

    public string? XRF_SAMPLE { get; set; }

    public string? SUP_PRD_MARK { get; set; }

    public string? FX_NO { get; set; }

    public string? FX_NAME { get; set; }

    public string? FX_KND { get; set; }

    public string? FX_SPC { get; set; }

    public string? FX_UNIT { get; set; }

    public string? YH_NO { get; set; }

    public string? IS_SP { get; set; }

    public string? SL_ID { get; set; }

    public DateTime? SC_DD { get; set; }

    public string? CNT_FLAG { get; set; }

    public byte[]? UP_DD_BIL { get; set; }

    public string? CNT_NEED { get; set; }

    public int? TRD_ITM { get; set; }

    public int? HFSH_ITM { get; set; }

    public decimal? QTY1_RCK { get; set; }

    public decimal? QTY1_RCK_UNSH { get; set; }

    public decimal? QTY1_PS { get; set; }

    public decimal? QTY1_PS_UNSH { get; set; }

    public virtual MF_TI TI_ { get; set; } = null!;
}
