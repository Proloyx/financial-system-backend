using System;
using System.Collections.Generic;

namespace FinancialSystem.Models.DB.DBSQLite;

public partial class Company
{
    public string CIK { get; set; } = null!;
    public string EntityName { get; set; } = null!;
}
