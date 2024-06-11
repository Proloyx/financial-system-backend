using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialSystem.Models.SearchModels
{
    public class Seriess
    {
        public string? id { get; set; }
        public string? realtime_start { get; set; }
        public string? realtime_end { get; set; }
        public string? title { get; set; }
        public string? observation_start { get; set; }
        public string? observation_end { get; set; }
        public string? frequency { get; set; }
        public string? frequency_short { get; set; }
        public string? units { get; set; }
        public string? units_short { get; set; }
        public string? seasonal_adjustment { get; set; }
        public string? seasonal_adjustment_short { get; set; }
        public string? last_updated { get; set; }
        public int popularity { get; set; }
        public int group_popularity { get; set; }
        public string? notes { get; set; }
    }
}