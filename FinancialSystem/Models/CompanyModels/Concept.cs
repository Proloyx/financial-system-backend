using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialSystem.Models.CompanyModels
{
    public class Concept
    {
        public string cik { get; set; }
        public string taxonomy { get; set; }
        public string tag { get; set; }
        public string label { get; set; }
        public string description { get; set; }
        public string entityName { get; set; }
        public Units units { get; set; }
    }
}