using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialSystem.Models;
using FinancialSystem.Models.DB.AppDBContext;
using FinancialSystem.Models.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace FinancialSystem.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<UserList>> GetUsersAsync();
        public Task<UserRet> GetUserByIdAsync(int id);
        public Task<int> AddUserAsync(UserRegister user);
        public Task<int> PutUserAsync(int id,UserRegister userupdated);
        public Task<int> DeleteUserAsync(int id);
        public Task<int> SetUserAdminAsync(int id);
        public Task<int> QuitarUserAdminAsync(int id);
        public Task<int> PutMassiveAsync(Massiveupdt mass);
        public Task<int> DeleteMassiveAsync(string name);
    }
}