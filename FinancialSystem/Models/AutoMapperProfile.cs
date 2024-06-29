using System.Net.NetworkInformation;
using AutoMapper;
using FinancialSystem.Models.DB.DBModels;
using FinancialSystem.Models.ObservationModels;
using FinancialSystem.Models.SearchModels;
using FinancialSystem.Models.UserModels;

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