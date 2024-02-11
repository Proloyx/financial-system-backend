using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialSystem.Models;

namespace FinancialSystem.Interfaces
{
    public interface IRequest
    {
        public Task<Response> GetObservation (string chart);
        public Task<Response> Search (string search);
    }
}