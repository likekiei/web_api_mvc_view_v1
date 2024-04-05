using System;
using System.Collections.Generic;

namespace ERP_EF.Models;

public partial class TF_TI_Z
{
    public string TI_ID { get; set; } = null!;

    public string TI_NO { get; set; } = null!;

    public int ITM { get; set; }

    public string? RRR2 { get; set; }
}
