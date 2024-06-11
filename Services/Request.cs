using FinancialSystem.Interfaces;
using System.Text.Json;
using System.Text;

namespace FinancialSystem.Services
{
    public class Request:IRequest
    {
        public async Task<HttpResponseMessage> Send(string url){
            using(var client = new HttpClient()){
                HttpResponseMessage response = await client.GetAsync(url);
                return response;
            }
        }
    }
}