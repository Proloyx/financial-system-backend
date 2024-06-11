using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DotNetEnv;

namespace FinancialSystem.Models.QueryParams
{
    public abstract class QueryParams
    {
        public string api_key { get; }
        public string file_type { get; }

        public QueryParams()
        {
            api_key = Env.GetString("FredApiKey");
            file_type = "json";
        }
    }
}