using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialSystemBackend.Interfaces
{
    public interface IUrlBuilder
    {
        public string GetUrl(string Url, Dictionary<string,string?> QueryParams);
    }
}