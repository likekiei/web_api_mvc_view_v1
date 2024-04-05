using System;
using System.Collections.Generic;

namespace ERP_EF.Models;

public partial class MF_TI
{
    public string TI_ID { get; set; } = null!;

    public string TI_NO { get; set; } = null!;

    public DateTime? TI_DD { get; set; }

    public string? BIL_ID { get; set; }

    public string? BIL_NO { get; set; }

    public string? SAL_NO { get; set; }

    public string? CLOSE_ID { get; set; }

    public string? CUS_NO { get; set; }

    public string? DEP { get; set; }

    public string? BAT_NO { get; set; }

    public string? REM { get; set; }

    public string? USR { get; set; }

    public string? CHK_MAN { get; set; }

    public string? PRT_SW { get; set; }

    public string? CPY_SW { get; set; }

    public DateTime? CLS_DATE { get; set; }

    public string? BIL_TYPE { get; set; }

    public string? OS_ID { get; set; }

    public string? OS_NO { get; set; }

    public string? CHKTY_ID { get; set; }

    public string? CUS_OS_NO { get; set; }

    public string? MOB_ID { get; set; }

    public string? LOCK_MAN { get; set; }

    public DateTime? LOCK_DATE { get; set; }

    public DateTime? SYS_DATE { get; set; }

    public string? FX_WH { get; set; }

    public string? PRT_USR { get; set; }

    public string? CNTT_NO { get; set; }

    public string? CHKTYPEID { get; set; }

    public string? CANCEL_ID { get; set; }

    public DateTime? PRT_DATE { get; set; }

    public string? BACK_ID { get; set; }

    public string? BIL_COMP { get; set; }

    public DateTime? MODIFY_DD { get; set; }

    public string? MODIFY_MAN { get; set; }

    public string? SCAN_ID { get; set; }

    public string? SCAN_USR { get; set; }

    public DateTime? SCAN_DATE { get; set; }

    public string? ZJ_ID { get; set; }

    public byte[]? UP_DD_BIL { get; set; }

    public string? WMS_ID { get; set; }

    public virtual ICollection<TF_TI> TF_TI { get; set; } = new List<TF_TI>();
}
