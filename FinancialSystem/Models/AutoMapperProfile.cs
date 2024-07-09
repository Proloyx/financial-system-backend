using System.Net.NetworkInformation;
using AutoMapper;
using FinancialSystem.Models.DB.AppDBContext;
using FinancialSystem.Models.UserModels;
using FinancialSystem.Models.ObservationModels;
using FinancialSystem.Models.SearchModels;
using FinancialSystem.Models.RoleModels;

namespace FinancialSystem.Models
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile(){
            CreateMap<User,UserList>();
            CreateMap<UserRegister,User>();
            CreateMap<User,UserRet>();
            CreateMap<Role,RoleRet>();
            CreateMap<Observation,ObservationRet>();
            CreateMap<Seriess,SeriessRet>();
            CreateMap<PingReply,PingReplyDTO>();
        }
    }
}