using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DotNetEnv;
using FinancialSystem.Models.QueryParams;
using FinancialSystem.Models;
using Microsoft.AspNetCore.WebUtilities;
using FinancialSystem.Interfaces;

namespace FinancialSystem.Services
{
    public class UrlBuilder
    {
        public static string Build(string endpoint, QueryParams queryParams)
        {
            var url = Env.GetString("FredUrl") + endpoint + "?";
            var query = queryParams
                                .GetType()
                                .GetProperties()
                                .Where(prop => prop.CanRead && prop.GetValue(queryParams) != null)
                                .Select(prop => $"{prop.Name}={Uri.EscapeDataString(prop.GetValue(queryParams).ToString())}");                    
            return url + string.Join("&",query);
        }
    }
}