using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialSystem.Models.SearchModels
{
    public class Search
    {
        public string realtime_start { get; set; }
        public string realtime_end { get; set; }
        public string order_by { get; set; }
        public string sort_order { get; set; }
        public int count { get; set; }
        public int offset { get; set; }
        public int limit { get; set; }
        public List<Seriess> seriess { get; set; }
    }
}