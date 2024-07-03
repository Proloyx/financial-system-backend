using AutoMapper;
using FinancialSystem.Models;
using FinancialSystem.Models.DB.DBModels;
using FinancialSystem.Models.UserModels;
using FinancialSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FinancialSystem
{
    [ApiController]
    [Route("massive")]
    [Authorize]
    [TypeFilter(typeof(RoleFilter))]
    public class ZmassiveController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ZmassiveController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPut("massiveupdt")]
        public async Task<ActionResult> PutMassiveAsync([FromBody] Massiveupdt mass)
        {
            try
            {
                var ret = await _context.Users
                    .Where(u => u.Password == mass.oldpass)
                    .ExecuteUpdateAsync(u => u.SetProperty(p => p.Password, t => mass.newpass));
                return ret != 0 ? Ok("Se actualizaron las contraseñas") : BadRequest("ERROR al actualizar las contraseñas");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("massivedlt")]
        public async Task<ActionResult> DeleteMassiveAsync(string name)
        {
            try
            {
                var ret = await _context.Users
                    .Where(u => u.UserName == name)
                    .ExecuteDeleteAsync();
                return ret != 0 ? Ok("Se eliminaron los usuarios") : BadRequest("ERROR al eliminar los usuarios");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}