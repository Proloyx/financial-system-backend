using System;
using System.Collections.Generic;

namespace FinancialSystem.Models;

public partial class PingReplyDTO
{
    public string? Address { get; set; }
    public long RoundtripTime { get; set; }
    public int Ttl { get; set; }
    public int BufferSize { get; set; }
    public string? Status { get; set; }
}