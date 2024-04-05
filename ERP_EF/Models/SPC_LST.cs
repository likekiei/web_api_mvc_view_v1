using System;
using System.Collections.Generic;

namespace ERP_EF.Models;

public partial class SPC_LST
{
    public string SPC_NO { get; set; } = null!;

    public string? NAME { get; set; }

    public string? SPC_NO_UP { get; set; }

    public string? SPC_ITEM { get; set; }
}
