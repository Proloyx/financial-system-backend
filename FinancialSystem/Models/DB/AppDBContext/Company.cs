using System;
using System.Collections.Generic;

namespace FinancialSystem.Models.DB.AppDBContext;

public partial class Company
{
    public string Cik { get; set; } = null!;

    public string Entityname { get; set; } = null!;
}
