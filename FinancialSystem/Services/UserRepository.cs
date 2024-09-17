using AutoMapper;
using FinancialSystem.Interfaces;
using FinancialSystem.Models;
using FinancialSystem.Models.DB.AppDBContext;
using FinancialSystem.Models.UserModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FinancialSystem.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public UserRepository(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<UserList>> GetUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            var ret = _mapper.Map<List<UserList>>(users);
            return ret;
        }

        public async Task<UserRet> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.UserId == id );
            var ret = _mapper.Map<UserRet>(user);
            return ret;
        }

        public async Task<int> AddUserAsync(UserRegister user)
        {
            var email = _context.Users.Any(u => u.Email == user.Email);
            if (email) return -1;
            await _context.AddAsync(_mapper.Map<User>(user));
            var ret = await _context.SaveChangesAsync();
            return ret;
        }

        public async Task<int> PutUserAsync(int id, UserRegister userupdated)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return -1;
            user.UserName = userupdated.UserName;
            user.Email = userupdated.Email;
            user.Password = userupdated.Password;
            var ret = await _context.SaveChangesAsync();
            return ret;
        }

        public async Task<int> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return -1;
            _context.Users.Remove(user);
            var ret = await _context.SaveChangesAsync();
            return ret;
        }

        public async Task<int> SetUserAdminAsync(int id)
        {
            var role = await _context.Roles.FindAsync(2);
            if (role == null) return -1;
            var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.UserId == id );
            if (user == null) return -2;
            if (user.Roles.Contains(role)) return -3;
            user.Roles.Add(role);
            var ret = await _context.SaveChangesAsync();
            return ret;
        }

        public async Task<int> QuitarUserAdminAsync(int id)
        {
            var role = await _context.Roles.FindAsync(2);
            if (role == null) return -1;
            var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.UserId == id );
            if (user == null) return -2;
            if (!user.Roles.Contains(role)) return -3;
            
            user.Roles.Remove(role);
            var ret = await _context.SaveChangesAsync();
            return ret;
        }

        public async Task<int> PutMassiveAsync(Massiveupdt mass)
        {
            var ret = await _context.Users
                .Where(u => u.Password == mass.oldpass)
                .ExecuteUpdateAsync(u => u.SetProperty(p => p.Password, t => mass.newpass));
            return ret;
        }

        public async Task<int> DeleteMassiveAsync(string name)
        {
            var ret = await _context.Users
                .Where(u => u.UserName == name)
                .ExecuteDeleteAsync();
            return ret;
        }
    }
}