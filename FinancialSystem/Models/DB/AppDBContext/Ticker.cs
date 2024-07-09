using System;
using System.Collections.Generic;

namespace FinancialSystem.Models.DB.AppDBContext;

public partial class Ticker
{
    public string CikStr { get; set; } = null!;

    public string Ticker1 { get; set; } = null!;

    public string Title { get; set; } = null!;
}
