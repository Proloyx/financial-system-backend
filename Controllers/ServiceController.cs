using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using AutoMapper;
using FinancialSystem.Models;


namespace APIGrafana.Controllers
{
    [ApiController]
    [Route("service")]
    public class PingController : ControllerBase
    {
        private readonly IMapper _mapper;
        public PingController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("ping/{value}")]
        public async Task<ActionResult<PingReplyDTO>> MakePing(string value)
        {
            try
            {
                Ping ping = new();
                PingReply rep = await ping.SendPingAsync(value,1000);
                return Ok(_mapper.Map<PingReplyDTO>(rep));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
            
        }
    }
}
