using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinancialSystem.Models.ObservationModels;
using FinancialSystem.Models.SearchModels;
using FinancialSystem.Models.UserModels;

namespace FinancialSystem.Models
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile(){
            CreateMap<Observation,ObservationRet>();
            CreateMap<Seriess,SeriessRet>();
            CreateMap<User,UserRet>();
            CreateMap<UserRegister,User>();
            CreateMap<User,UserRegister>();
        }
    }
}