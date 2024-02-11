using FinancialSystem.Interfaces;
using FinancialSystem.Models;
using System.Text.Json;
using System.Text;
using DotNetEnv;
using FinancialSystemBackend.Services;

namespace FinancialSystem.Services
{
    public class Request:IRequest
    {
        public async Task<Response> GetObservation (string id){
            using (var client = new HttpClient()) {
                var response = await client.GetAsync(
                    $"https://{Env.GetString("FredUrl")}/series/observations?series_id={id}&api_key={Env.GetString("FredApiKey")}&observation_start=2010-01-01&file_type=json");
                return new Response{
                    StatusCode = (int)response.StatusCode,
                    Result = await response.Content.ReadAsStringAsync()
                };
            }  
        }
        public async Task<Response> Search (string search){
            using (var client = new HttpClient()) {
                
                var response = await client.GetAsync(
                    $"https://{Env.GetString("FredUrl")}/series/search?search_text={search}&api_key={Env.GetString("FredApiKey")}&limit=50&file_type=json");
                return new Response{
                    StatusCode = (int)response.StatusCode,
                    Result = await response.Content.ReadAsStringAsync()
                };
            }  
        }
    }
}