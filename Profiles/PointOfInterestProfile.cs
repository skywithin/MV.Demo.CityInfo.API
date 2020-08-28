using AutoMapper;
using MV.Demo.CityInfo.API.Entities;
using MV.Demo.CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MV.Demo.CityInfo.API.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            // Entity > Dto
            CreateMap<PointOfInterestEntity, PointOfInterestDto>();
            CreateMap<PointOfInterestEntity, PointOfInterestForUpdateDto>().ReverseMap();

            //Dto > Entity
            CreateMap<PointOfInterestForCreationDto, PointOfInterestEntity>();
            //CreateMap<PointOfInterestForUpdateDto, PointOfInterestEntity>(); // Same as reverse map
            
        }
    }
}
