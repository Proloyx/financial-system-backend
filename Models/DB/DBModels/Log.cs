using System;
using System.Collections.Generic;

namespace FinancialSystem.Models.DB.DBModels;

public partial class Log
{
    public int LogId { get; set; }

    public string? LogMessage { get; set; }
}
