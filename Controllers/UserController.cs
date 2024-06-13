using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FinancialSystem.Models.UserModels;
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

        [HttpGet("get")]
        public async Task<ActionResult<List<UserRet>>> GetUsersAsync()
        {
            try
            {
                var collection = _firestore.Collection("users");
                var snapshot = await collection.GetSnapshotAsync();
                var users = snapshot.Documents.Select(s => s.ConvertTo<User>()).ToList();
                var ret = _mapper.Map<List<UserRet>>(users);
                return users.Count != 0 ? Ok(ret) : NotFound("No existen usuarios");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("getid/{id}")]
        public async Task<ActionResult<User>> GetUserByIdAsync(string id)
        {
            try
            {
                var collection = _firestore.Collection("users");
                var snapshot = await collection.Document(id).GetSnapshotAsync();
                var ret = snapshot.ConvertTo<User>();
                return ret != null ? Ok(ret) : NotFound("No se encuentra al usuario");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddUserAsync(UserRegister user)
        {
            try
            {
                var collection = _firestore.Collection("users");
                var ret = await collection.AddAsync(_mapper.Map<User>(user));
                return ret.Id != null ? Created() : BadRequest("No se pudo crear al usuario");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("put/{id}")]
        public async Task<ActionResult> PutUserAsync(string id, UserRegister user)
        {
            try
            {
                var collection = _firestore.Collection("users");
                var snapshot = await collection.Document(id).GetSnapshotAsync();
                if (!snapshot.Exists) return NotFound("No existe el usuario");
                await collection.Document(id).DeleteAsync();
                await collection.AddAsync(_mapper.Map<User>(user));
                return NoContent();
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
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("probando")]
        public ActionResult<string> GetAsync(ClaimsPrincipal claim)
        {
            return Ok(claim.Identity?.Name);
        }
    }
}