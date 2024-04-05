using System;
using System.Collections.Generic;

namespace ERP_EF.Models;

public partial class TF_MO
{
    public string MO_NO { get; set; } = null!;

    public int ITM { get; set; }

    public string? PRD_NO { get; set; }

    public string? PRD_NAME { get; set; }

    public string? PRD_MARK { get; set; }

    public string? WH { get; set; }

    public string? UNIT { get; set; }

    public decimal? QTY_RSV { get; set; }

    public decimal? QTY_LOST { get; set; }

    public decimal? QTY { get; set; }

    public decimal? QTY_UNSH { get; set; }

    public string? BAT_NO { get; set; }

    public string? REM { get; set; }

    public decimal? CST { get; set; }

    public string? ZC_NO { get; set; }

    public string? TW_ID { get; set; }

    public string? ZC_REM { get; set; }

    public string? USEIN { get; set; }

    public string? CPY_SW { get; set; }

    public string? USEIN_NO { get; set; }

    public string? PRD_NO_CHG { get; set; }

    public decimal? QTY1_RSV { get; set; }

    public decimal? QTY1_LOST { get; set; }

    public string? ID_NO { get; set; }

    public string? MD_NO { get; set; }

    public decimal? QTY_TS { get; set; }

    public decimal? QTY_TS_UNSH { get; set; }

    public int? TS_ITM { get; set; }

    public string? COMPOSE_IDNO { get; set; }

    public int? EST_ITM { get; set; }

    public int? PRE_ITM { get; set; }

    public decimal? LOS_RTO { get; set; }

    public decimal? QTY_STD { get; set; }

    public string? SEB_NO { get; set; }

    public string? GRP_NO { get; set; }

    public string? ZC_PRD { get; set; }

    public string? CHG_RTO { get; set; }

    public int? CHG_ITM { get; set; }

    public decimal? QTY_CHG_RTO { get; set; }

    public decimal? QTY_QL { get; set; }

    public decimal? QTY_QL_UNSH { get; set; }

    public decimal? QTY1_QL { get; set; }

    public decimal? QTY_DM { get; set; }

    public decimal? QTY_DM_UNSH { get; set; }

    public decimal? QTY1_DM { get; set; }

    public decimal? QTY_BL { get; set; }

    public string? CHK_RTN { get; set; }

    public decimal? QTY_BL_UNSH { get; set; }

    public decimal? QTY_QL_YL { get; set; }

    public decimal? QTY_QL_YL_UNSH { get; set; }

    public decimal? QTY_BAS { get; set; }

    public decimal? QTY_BOM { get; set; }

    public string? QC_FLAG { get; set; }

    public string? CQ_FLG { get; set; }

    public string? CHK_XZL { get; set; }

    public string? ZZ_NO { get; set; }

    public string? ZZ_NO_CHD { get; set; }

    public string? PAK_UNIT { get; set; }

    public decimal? PAK_EXC { get; set; }

    public decimal? PAK_NW { get; set; }

    public string? PAK_WEIGHT_UNIT { get; set; }

    public decimal? PAK_GW { get; set; }

    public decimal? PAK_MEAST { get; set; }

    public string? PAK_MEAST_UNIT { get; set; }

    public decimal? CST_ML { get; set; }

    public decimal? CST_MAN { get; set; }

    public decimal? CST_MAK { get; set; }

    public decimal? CST_PRD { get; set; }

    public decimal? CST_OUT { get; set; }

    public virtual MF_MO MO_NONavigation { get; set; } = null!;
}
