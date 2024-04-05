using System;
using System.Collections.Generic;

namespace ERP_EF.Models;

public partial class MF_MO
{
    public string MO_NO { get; set; } = null!;

    public DateTime? MO_DD { get; set; }

    public DateTime? STA_DD { get; set; }

    public DateTime? END_DD { get; set; }

    public string? BIL_ID { get; set; }

    public string? BIL_NO { get; set; }

    public string? MRP_NO { get; set; }

    public string? PRD_MARK { get; set; }

    public string? WH { get; set; }

    public string? SO_NO { get; set; }

    public string? UNIT { get; set; }

    public decimal? QTY { get; set; }

    public decimal? QTY1 { get; set; }

    public DateTime? NEED_DD { get; set; }

    public string? DEP { get; set; }

    public string? CUS_NO { get; set; }

    public string? CLOSE_ID { get; set; }

    public string? USR { get; set; }

    public string? CHK_MAN { get; set; }

    public string? BAT_NO { get; set; }

    public string? REM { get; set; }

    public string? PO_OK { get; set; }

    public string? MO_NO_ADD { get; set; }

    public decimal? QTY_FIN { get; set; }

    public decimal? QTY_FIN_UNSH { get; set; }

    public decimal? TIME_AJ { get; set; }

    public decimal? QTY_ML { get; set; }

    public decimal? QTY_ML_UNSH { get; set; }

    public string? BUILD_BIL { get; set; }

    public decimal? CST_MAKE { get; set; }

    public decimal? CST_PRD { get; set; }

    public decimal? CST_OUT { get; set; }

    public decimal? CST_MAN { get; set; }

    public decimal? USED_TIME { get; set; }

    public decimal? CST { get; set; }

    public string? PRT_SW { get; set; }

    public DateTime? OPN_DD { get; set; }

    public DateTime? FIN_DD { get; set; }

    public string? BIL_MAK { get; set; }

    public string? CPY_SW { get; set; }

    public string? CONTRACT { get; set; }

    public int? EST_ITM { get; set; }

    public string? ML_OK { get; set; }

    public string? MD_NO { get; set; }

    public decimal? QTY_RK { get; set; }

    public decimal? QTY_RK_UNSH { get; set; }

    public DateTime? CLS_DATE { get; set; }

    public string? ID_NO { get; set; }

    public decimal? QTY_CHK { get; set; }

    public string? CONTROL { get; set; }

    public string? ISNORMAL { get; set; }

    public string? QC_YN { get; set; }

    public string? MM_CURML { get; set; }

    public string? TS_ID { get; set; }

    public string? BIL_TYPE { get; set; }

    public string? CNTT_NO { get; set; }

    public string? MOB_ID { get; set; }

    public string? LOCK_MAN { get; set; }

    public DateTime? LOCK_DATE { get; set; }

    public string? SEB_NO { get; set; }

    public string? GRP_NO { get; set; }

    public DateTime? OUT_DD_MOJ { get; set; }

    public DateTime? SYS_DATE { get; set; }

    public string? PG_ID { get; set; }

    public string? SUP_PRD_NO { get; set; }

    public decimal? TIME_CNT { get; set; }

    public string? ML_BY_MM { get; set; }

    public string? CAS_NO { get; set; }

    public int? TASK_ID { get; set; }

    public string? OLD_ID { get; set; }

    public string? CF_ID { get; set; }

    public string? CUS_OS_NO { get; set; }

    public string? PRT_USR { get; set; }

    public decimal? QTY_QL { get; set; }

    public decimal? QTY_QL_UNSH { get; set; }

    public string? QL_ID { get; set; }

    public string? Q2_ID { get; set; }

    public string? Q3_ID { get; set; }

    public string? ISSVS { get; set; }

    public decimal? QTY_DM { get; set; }

    public decimal? QTY_DM_UNSH { get; set; }

    public string? LOCK { get; set; }

    public decimal? QTY_LOST { get; set; }

    public decimal? QTY_LOST_UNSH { get; set; }

    public string? ISFROMQD { get; set; }

    public string? ZT_ID { get; set; }

    public DateTime? ZT_DD { get; set; }

    public string? CV_ID { get; set; }

    public string? CU_NO { get; set; }

    public decimal? QTY_CHK_UNSH { get; set; }

    public string? CANCEL_ID { get; set; }

    public string? SUP_PRD_MARK { get; set; }

    public DateTime? PRT_DATE { get; set; }

    public string? BJ_NO { get; set; }

    public DateTime? MODIFY_DD { get; set; }

    public string? MODIFY_MAN { get; set; }

    public int? DEC_UN { get; set; }

    public decimal? QTY_QS { get; set; }

    public decimal? QTY_QS_UNSH { get; set; }

    public string? BACK_ID { get; set; }

    public decimal? QTY_PG { get; set; }

    public decimal? QTY_PG_UNSH { get; set; }

    public string? SO_ID { get; set; }

    public string? ISMATCHBIL { get; set; }

    public DateTime? CF_DD { get; set; }

    public decimal? CST_MAN_ML { get; set; }

    public decimal? CST_MAK_ML { get; set; }

    public decimal? CST_PRD_ML { get; set; }

    public decimal? CST_OUT_ML { get; set; }

    public decimal? CST_ML { get; set; }

    public virtual ICollection<TF_MO> TF_MO { get; set; } = new List<TF_MO>();
}
