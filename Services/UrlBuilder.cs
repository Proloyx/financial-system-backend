using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialSystemBackend.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

namespace FinancialSystemBackend.Services
{
    public static class UrlBuilder//:IUrlBuilder
    {
        public static string GetUrl(string Url, Dictionary<string,string?> QueryParams)
        {
            string newurl = QueryHelpers.AddQueryString(Url, QueryParams);
            return newurl;
        }
    }
}