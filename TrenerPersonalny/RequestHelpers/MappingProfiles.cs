using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Models;

namespace TrenerPersonalny.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateExcerciseDTO, Excercises>();
            CreateMap<UpdateExcerciseDTO, Excercises>();
        }
    }
}
