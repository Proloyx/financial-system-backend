using FinancialSystem.Models;
using Microsoft.AspNetCore.Mvc;
using FinancialSystem.Interfaces;
using System.Text.Json;
using FinancialSystem.Models.QueryParams;
using Sprache;
using System;
using System.Reflection;
using FinancialSystem.Services;
using FinancialSystem.Models.ObservationModels;
using FinancialSystem.Models.SearchModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
namespace FinancialSystem.Controllers

{
    [Route("series")]
    [Authorize]
    public class SeriesController : ControllerBase
    {
        private readonly IRequest _request;
        private readonly IMapper _mapper;
        public SeriesController(IRequest request, IMapper maper)
        {
            _request = request;
            _mapper = maper;
        }

        [HttpGet("observation")]
        public async Task<ActionResult<List<ObservationRet>>> GetObservationAsync([FromQuery] ObservationParams observationParams){
            try
            {
                string url = UrlBuilder.Build("/series/observations", observationParams);
                var response = await _request.Send(url);
                var result = await response.Content.ReadAsStringAsync();
                var des = JsonSerializer.Deserialize<Series>(result)?.observations;
                var ret = _mapper.Map<List<ObservationRet>>(des);
                return response.IsSuccessStatusCode ? Ok(ret) : StatusCode((int)response.StatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<SeriessRet>>> GetSearchAsync([FromQuery] SearchParams searchParams)
        {
            try
            {
                string url = UrlBuilder.Build("/series/search", searchParams);
                var response = await _request.Send(url);
                var result = await response.Content.ReadAsStringAsync();
                var des = JsonSerializer.Deserialize<Search>(result)?.seriess;
                var ret = _mapper.Map<List<SeriessRet>>(des);
                return response.IsSuccessStatusCode ? Ok(ret) : StatusCode((int)response.StatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}   
