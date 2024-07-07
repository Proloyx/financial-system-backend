using FinancialSystem.Models;
using Microsoft.AspNetCore.Mvc;
using FinancialSystem.Interfaces;
using System.Text.Json;
using FinancialSystem.Models.QueryParams;
using FinancialSystem.Services;
using FinancialSystem.Models.ObservationModels;
using FinancialSystem.Models.SearchModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using FinancialSystem.Models.DB.DBModels;
using Microsoft.EntityFrameworkCore;
using FinancialSystem.Models.DB.DBSQLite;
using Microsoft.IdentityModel.Tokens;

namespace FinancialSystem.Controllers

{
    [Route("company")]
    public class CompanyController : ControllerBase
    {
        private readonly IRequest _request;
        private readonly IMapper _mapper;
        public CompanyController(IRequest request, IMapper maper)
        {
            _request = request;
            _mapper = maper;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<List<Company>>> GetAsync(string name)
        {
            try
            {
                using (var context = new SQLiteDbContext()) {
                List<Company> companies = await context.Companies.Where(c => c.EntityName.ToLower().Contains(name)).ToListAsync();
                return !companies.IsNullOrEmpty() ? Ok(companies) : NotFound("No se encontró la compañía");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("sqlite")]
        public IActionResult Trying()
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                string[] files = Directory.GetFiles(currentDirectory);
                Console.WriteLine(files);
                string currentDirectory3 = Path.Combine(Directory.GetCurrentDirectory(), "Data");
                string[] files3 = Directory.GetFiles(currentDirectory3);
                Console.WriteLine(files3);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
  
