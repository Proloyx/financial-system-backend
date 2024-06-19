using AutoMapper;
using FinancialSystem.Models.DB.DBModels;
using FinancialSystem.Models.UserModels;

namespace FinancialSystem.Models
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile(){
            CreateMap<User,UserList>();
            CreateMap<UserRegisterAdmin,User>();
            CreateMap<User,UserRet>();
            CreateMap<Role,RoleRet>();
        }
    }
}