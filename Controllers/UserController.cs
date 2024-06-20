using AutoMapper;
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
    [Route("user")]
    [Authorize]
    [TypeFilter(typeof(RoleFilter))]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public UserController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("get")]
        public async Task<ActionResult<List<UserList>>> GetUsersAsync()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                if (users.IsNullOrEmpty()) return BadRequest("No existen usuarios");
                var ret = _mapper.Map<List<UserList>>(users);
                return Ok(ret);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<User>> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.UserId == id );
                if (user==null) return NotFound("No se encontró el usuario");
                return Ok(_mapper.Map<UserRet>(user));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddUserAsync([FromBody] UserRegister user)
        {
            try
            {
                var email = _context.Users.Any(u => u.Email == user.Email);
                if (email) return BadRequest("Existe un usuario con ese correo");

                await _context.AddAsync(_mapper.Map<User>(user));
                var ret = await _context.SaveChangesAsync();
                return ret != 0 ? Ok("Se añadió el usuario") : BadRequest("ERROR al añadir al usuario");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> PutUserAsync(int id, [FromBody] UserRegister userupdated)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null) return NotFound("No se encontró el usuario");
                user.UserName = userupdated.UserName;
                user.Email = userupdated.Email;
                user.Password = userupdated.Password;
                var ret = await _context.SaveChangesAsync();
                return ret != 0 ? Ok("Se actualizó el usuario") : BadRequest("ERROR al actualizar al usuario");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null) return NotFound("No se encontró el usuario");
                _context.Users.Remove(user);
                var ret = await _context.SaveChangesAsync();
                return ret != 0 ? Ok("Se eliminó el usuario") : BadRequest("ERROR al eliminar al usuario");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("admin/{id}")]
        public async Task<ActionResult> SetUserAdminAsync(int id)
        {
            try
            {
                var role = await _context.Roles.FindAsync(2);
                if (role == null) return BadRequest("No se encontró el rol admin");

                var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.UserId == id );
                if (user == null) return NotFound("No se encontró el usuario");
                if (user.Roles.Contains(role)) return BadRequest("El usuario ya es admin");

                user.Roles.Add(role);
                var ret = await _context.SaveChangesAsync();

                return ret != 0 ? Ok("Se añadió el rol de admin al usuario") : BadRequest("ERROR al añadir el rol de admin al usuario");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("unadmin/{id}")]
        public async Task<ActionResult> QuitarUserAdminAsync(int id)
        {
            try
            {
                var role = await _context.Roles.FindAsync(2);
                if (role == null) return BadRequest("No se encontró el rol admin");

                var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.UserId == id );
                if (user == null) return NotFound("No se encontró el usuario");
                if (!user.Roles.Contains(role)) return BadRequest("El usuario no es admin");
                
                user.Roles.Remove(role);
                var ret = await _context.SaveChangesAsync();

                return ret != 0 ? Ok("Se eliminó el rol de admin al usuario") : BadRequest("ERROR al eliminar el rol de admin al usuario");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}