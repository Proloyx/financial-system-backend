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
            using (var context = new SQLiteDbContext()) {
                List<Company> companies = await context.Companies.Where(c => c.EntityName.ToLower().Contains(name)).ToListAsync();
                return Ok(companies);
            }
        }
    }
}   
