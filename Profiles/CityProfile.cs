using AutoMapper;
using MV.Demo.CityInfo.API.Entities;
using MV.Demo.CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MV.Demo.CityInfo.API.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            //Entity > Dto
            CreateMap<CityEntity, CityDto>();
            CreateMap<CityEntity, CityWithPointsOfInterestDto>();
            
        }
    }
}
