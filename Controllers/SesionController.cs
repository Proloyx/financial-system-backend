using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DotNetEnv;
using FinancialSystem.Models.UserModels;
using Google.Apis.Util;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FinancialSystem
{
    [ApiController]
    [Route("sesion")]
    public class SesionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly FirestoreDb _firestore;

        public SesionController(IMapper mapper, FirestoreDb firestore)
        {
            _mapper = mapper;
            _firestore = firestore;
        }

        [HttpGet("login")]
        public async Task<ActionResult<string>> LoginUserAsync([FromQuery] UserLogin user)
        {
            try
            {
                var collection = _firestore.Collection("users").WhereEqualTo("email", user.email).WhereEqualTo("password", user.password).Limit(1);
                var snapshot = await collection.GetSnapshotAsync();
                if (snapshot.Count == 0) return BadRequest("Credenciales Inválidas");
                var userlogged = snapshot.ElementAt(0).ConvertTo<User>();

                var tokenhandler = new JwtSecurityTokenHandler();
                var tokendesc = new SecurityTokenDescriptor{
                    Subject = new ClaimsIdentity(new Claim[]{
                        new Claim(ClaimTypes.Sid, userlogged.id),
                        new Claim(ClaimTypes.Name, userlogged.user_name),
                        new Claim(ClaimTypes.Email, userlogged.email),
                        new Claim("Profesion", userlogged.profesion),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
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

        // [HttpPost("register")]
        // public async Task<ActionResult> RegisterUserAsync(UserRegister user)
        // {
        //     try
        //     {
        //         var collection = _firestore.Collection("users");
        //         var ret = await collection.AddAsync(_mapper.Map<User>(user));
        //         return ret.Id != null ? Created() : BadRequest("No se pudo crear al usuario");
        //     }
        //     catch (Exception e)
        //     {
        //         return StatusCode(500, e.Message);
        //     }
        // }
    }
}