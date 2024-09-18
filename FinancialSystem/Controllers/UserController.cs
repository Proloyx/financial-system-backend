using AutoMapper;
using FinancialSystem.Interfaces;
using FinancialSystem.Models;
using FinancialSystem.Models.DB.AppDBContext;
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
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("get")]
        public async Task<ActionResult<List<UserList>>> GetUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetUsersAsync();
                if (users.IsNullOrEmpty()) return NotFound("No existen usuarios");
                return Ok(users);
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
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null) return NotFound("No se encontró el usuario");
                return Ok(user);
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
                var ret = await _userRepository.AddUserAsync(user);
                if (ret == -1) return BadRequest("Existe un usuario con ese correo");
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
                var ret = await _userRepository.PutUserAsync(id, userupdated);
                if (ret == -1) return NotFound("No se encontró el usuario");
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
                var ret = await _userRepository.DeleteUserAsync(id);
                if (ret == -1) return NotFound("No se encontró el usuario");
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
                var ret = await _userRepository.SetUserAdminAsync(id);
                if (ret == -1) return BadRequest("No se encontró el rol admin");
                if (ret == -2) return NotFound("No se encontró el usuario");
                if (ret == -3) return BadRequest("El usuario ya es admin");

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
                var ret = await _userRepository.QuitarUserAdminAsync(id);
                if (ret == -1) return BadRequest("No se encontró el rol admin");
                if (ret == -2) return NotFound("No se encontró el usuario");
                if (ret == -3) return BadRequest("El usuario no es admin");
                return ret != 0 ? Ok("Se eliminó el rol de admin al usuario") : BadRequest("ERROR al eliminar el rol de admin al usuario");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("massiveupdt")]
        public async Task<ActionResult> PutMassiveAsync([FromBody] Massiveupdt mass)
        {
            try
            {
                var ret = await _userRepository.PutMassiveAsync(mass);
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
                var ret = await _userRepository.DeleteMassiveAsync(name);
                return ret != 0 ? Ok("Se eliminaron los usuarios") : BadRequest("ERROR al eliminar los usuarios");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}