using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinancialSystem.Models.ObservationModels;
using FinancialSystem.Models.SearchModels;

namespace FinancialSystem.Models
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile(){
            CreateMap<Observation,ObservRetDTO>();
            CreateMap<Seriess,SeriessRetDTO>();
        }
    }
}