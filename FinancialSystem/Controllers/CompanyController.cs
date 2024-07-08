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
                Console.WriteLine(Directory.GetCurrentDirectory());
                Console.WriteLine("");

                string currentDirectory = Directory.GetCurrentDirectory();
                string[] files = Directory.GetFiles(currentDirectory);
                foreach (string file in files)
                {
                    Console.WriteLine(file);
                }
                
                Console.WriteLine("");
                Console.WriteLine(Path.Combine(Directory.GetCurrentDirectory(), "Data"));
                Console.WriteLine("");
                string currentDirectory3 = Path.Combine(Directory.GetCurrentDirectory(), "Data");
                string[] files3 = Directory.GetFiles(currentDirectory3);
                foreach (string file in files3)
                {
                    Console.WriteLine(file);
                }
                
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
  
