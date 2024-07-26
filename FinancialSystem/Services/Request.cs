using FinancialSystem.Interfaces;
using System.Text.Json;
using System.Text;
using Serilog;

namespace FinancialSystem.Services
{
    public class Request:IRequest
    {
        public async Task<HttpResponseMessage> SendAsync(string url){
            using(var client = new HttpClient()){
                HttpResponseMessage response = await client.GetAsync(url);
                return response;
            }
        
        }
    }
}