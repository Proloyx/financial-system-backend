using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using DotNetEnv;
using FinancialSystem.Models.DB.DBModels;
using FinancialSystem.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FinancialSystem
{
    [ApiController]
    [Route("sesion")]
    public class SesionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public SesionController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        //Hay que ver si es necesario devolver el password
        [HttpGet("login")]
        public async Task<ActionResult<string>> LoginAsync([FromQuery] UserLogin user)
        {
            try
            {
                var logging = await _context.Users.Include(r => r.Roles).FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
                if (logging == null) return BadRequest("Credenciales Inválidas");
                var logged = _mapper.Map<UserRet>(logging);
                var tokenhandler = new JwtSecurityTokenHandler();
                var tokendesc = new SecurityTokenDescriptor{
                    Subject = new ClaimsIdentity(new Claim[]{
                        new Claim("user", JsonSerializer.Serialize(logged))
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.GetString("Key"))),SecurityAlgorithms.HmacSha256Signature)
                };

                 var token = tokenhandler.CreateToken(tokendesc);
                 return Ok("Bearer " + tokenhandler.WriteToken(token));
             }
             catch (Exception e)
             {
                 return StatusCode(500, e.Message);
             }
         }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(UserRegister user)
        {
            try
            {
                var email = _context.Users.Any(u => u.Email == user.Email);
                if (email) return BadRequest("Existe un usuario con ese correo");

                await _context.AddAsync(_mapper.Map<User>(user));
                var ret = await _context.SaveChangesAsync();
                return ret != 0 ? Ok("Se registró correctamente el usuario") : BadRequest("ERROR al registrar el usuario");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}