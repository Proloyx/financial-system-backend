using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialSystem.Models;
using FinancialSystem.Models.QueryParams;

namespace FinancialSystem.Interfaces
{
    public interface IRequest
    {
        public Task<HttpResponseMessage> Send(string url);
    }
}