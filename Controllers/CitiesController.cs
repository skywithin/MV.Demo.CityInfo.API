using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MV.Demo.CityInfo.API.Models;
using MV.Demo.CityInfo.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MV.Demo.CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICityInfoRepository _repo;

        public CitiesController(
            IMapper mapper,
            ICityInfoRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var cities = _repo.GetAllCities();
            var result = _mapper.Map<IEnumerable<CityDto>>(cities);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest = false)
        {
            var city = _repo.GetCity(id, includePointsOfInterest);

            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                var resultCityWithPoi = _mapper.Map<CityWithPointsOfInterestDto>(city);
                return Ok(resultCityWithPoi);
            }
            else
            {
                var resultCity = _mapper.Map<CityDto>(city);
                return Ok(resultCity);
            }
        }
    }
}
