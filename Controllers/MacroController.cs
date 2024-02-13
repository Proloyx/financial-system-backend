using FinancialSystem.Models;
using Microsoft.AspNetCore.Mvc;
using FinancialSystem.Interfaces;
using System.Text.Json;
using FinancialSystemBackend.Models;
namespace FinancialSystem.Controllers

{
    [Route("[controller]")]
    public class MacroController : ControllerBase
    {
        IRequest _request;
        public MacroController(IRequest request)
        {
            _request = request;
        }

        [HttpGet("observation/{id}")]
        public async Task<ActionResult<List<Observation>>> GetObservation(string id)
        { 
            Response response = await _request.GetObservation(id);
            Series root = JsonSerializer.Deserialize<Series>(response.Result.ToString());
            return Ok(root.observations.Select(b=> new {
                date = b.date,
                value = b.value
            }));
        }
        [HttpGet("search")]
        public async Task<ActionResult<List<Seriess>>> Search(string search)
        {
            Response response = await _request.Search(search);
            Search sea = JsonSerializer.Deserialize<Search>(response.Result.ToString());
            return Ok(sea.seriess.Select(b=>new{
                id = b.id,
                title = b.title,
                observation_start = b.observation_start,
                observation_end = b.observation_end,
                frequency = b.frequency,
                units = b.units,
                seasonal_adjustment= b.seasonal_adjustment,
                notes = b.notes
            }));
        }
    }
}   
