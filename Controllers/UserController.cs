using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using FinancialSystem.Models.UserModels;
using FirebaseAdmin.Auth;
using Google.Apis.Util;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialSystem
{
    [ApiController]
    [Route("user")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly FirestoreDb _firestore;

        public UserController(IMapper mapper, FirestoreDb firestore)
        {
            _mapper = mapper;
            _firestore = firestore;
        }

        [HttpGet("getall")]
        public async Task<ActionResult<List<UserList>>> GetUsersAsync()
        {
            try
            {
                var collection = _firestore.Collection("users");
                var snapshot = await collection.GetSnapshotAsync();
                var users = snapshot.Documents.Select(s => s.ConvertTo<User>()).ToList();
                var ret = _mapper.Map<List<UserList>>(users);
                return users.Count != 0 ? Ok(ret) : NotFound("No existen usuarios");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<User>> GetUserByIdAsync(string id)
        {
            try
            {
                var collection = _firestore.Collection("users");
                var snapshot = await collection.Document(id).GetSnapshotAsync();
                var ret = snapshot.ConvertTo<User>();
                return ret != null ? Ok(ret) : NotFound("El usuario no se encontró");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddUserAsync(UserRegisterAdmin user)
        {
            try
            {
                var collection = _firestore.Collection("users");
                var ret = await collection.AddAsync(_mapper.Map<User>(user));
                return ret.Id != null ? StatusCode(201, "El usuario se añadió") : BadRequest("No se pudo añadir al usuario");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //Hay que hacer bien la actualizacion
        [HttpPut("update/{id}")]
        public async Task<ActionResult> PutUserAsync(string id, UserRegister user)
        {
            try
            {
                var collection = _firestore.Collection("users");
                var snapshot = await collection.Document(id).GetSnapshotAsync();
                if (!snapshot.Exists) return NotFound("El usuario no existe");
                await collection.Document(id).DeleteAsync();
                await collection.AddAsync(_mapper.Map<User>(user));
                return Ok("El usuario se actualizó");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteUserAsync(string id)
        {
            try
            {
                var collection = _firestore.Collection("users");
                var snapshot = await collection.Document(id).GetSnapshotAsync();
                if (!snapshot.Exists) return NotFound("No existe el usuario");
                await collection.Document(id).DeleteAsync();
                return Ok("El usuario se eliminó");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //Probando obtener las claims mediante un objeto
        // [HttpGet("probando")]
        // public ActionResult<User> GetAsync()
        // {
        //     var user = JsonSerializer.Deserialize<User>(User.FindFirst("usuario")?.Value);
        //     return Ok(user);
        // }
    }
}