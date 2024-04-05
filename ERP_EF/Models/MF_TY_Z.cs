using System;
using System.Collections.Generic;

namespace ERP_EF.Models;

public partial class MF_TY_Z
{
    public string TY_ID { get; set; } = null!;

    public string TY_NO { get; set; } = null!;

    public string? PPPNUM { get; set; }

    public string? BBNUM { get; set; }

    public string? RRR2 { get; set; }

    public DateTime? DDDDSTA { get; set; }

    public DateTime? DDDDEDN { get; set; }

    public DateTime? DDDDGRE { get; set; }
}
