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
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FinancialSystem.Models.DB.AppDBContext;

namespace FinancialSystem.Controllers

{
    [Route("company")]
    public class CompanyController : ControllerBase
    {
        private readonly IRequest _request;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public CompanyController(IRequest request, IMapper maper, AppDbContext context)
        {
            _request = request;
            _mapper = maper;
            _context = context;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<List<Company>>> GetAsync(string name)
        {
            try
            {
                var companies = await _context.Companies.Where(c => c.Entityname.ToLower().Contains(name)).ToListAsync();
                return !companies.IsNullOrEmpty() ? Ok(companies) : NotFound("No se encontró la compañía");
                
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
  
