using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MV.Demo.CityInfo.API.Entities;
using MV.Demo.CityInfo.API.Models;
using MV.Demo.CityInfo.API.Services;

namespace MV.Demo.CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointsofInterestController : ControllerBase
    {
        private readonly ILogger<PointsofInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _repo;
        private readonly IMapper _mapper;

        public PointsofInterestController(
            ILogger<PointsofInterestController> logger,
            IMailService mailService,
            ICityInfoRepository repo,
            IMapper mapper)
        {
            _logger = logger;
            _mailService = mailService;
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            try
            {
                if (_repo.IsCityExists(cityId) == false)
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest");
                    return NotFound();
                }

                var pointsOfInterest = _repo.GetAllPointsOfInterestForCity(cityId);
                var result = _mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting points of interest for city id {cityId}.", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

        [HttpGet("{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            if (_repo.IsCityExists(cityId) == false)
            {
                return NotFound();
            }

            var poi = _repo.GetPointOfInterestForCity(cityId, id);

            if (poi == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<PointOfInterestDto>(poi);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreatePointOfInterest(
            int cityId,
            [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError(
                    "Description",
                    "Provided description should be different from name");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_repo.IsCityExists(cityId) == false)
            {
                return NotFound();
            }

            var newPoi = _mapper.Map<PointOfInterestEntity>(pointOfInterest);

            _repo.AddPointOfInterestForCity(cityId, newPoi);
            _repo.Save();

            var poiDto = _mapper.Map<PointOfInterestDto>(newPoi);

            return CreatedAtRoute(
                "GetPointOfInterest",
                new { cityId, id = poiDto.Id },
                poiDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,
            [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError(
                    "Description",
                    "Provided description should be different from name");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_repo.IsCityExists(cityId) == false)
            {
                return NotFound();
            }

            var poiEntity = _repo.GetPointOfInterestForCity(cityId, id);
            if (poiEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(pointOfInterest, poiEntity);

            _repo.UpdatePointOfInterestForCity(cityId, poiEntity);
            _repo.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            if (_repo.IsCityExists(cityId) == false)
            {
                return NotFound();
            }

            var poiEntity = _repo.GetPointOfInterestForCity(cityId, id);

            if (poiEntity == null)
            {
                return NotFound();
            }

            var poiToPatchDto = _mapper.Map<PointOfInterestForUpdateDto>(poiEntity);

            patchDoc.ApplyTo(poiToPatchDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (poiToPatchDto.Name == poiToPatchDto.Description)
            {
                ModelState.AddModelError(
                   "Description",
                   "Provided description should be different from name");
            }

            if (!TryValidateModel(poiToPatchDto))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(poiToPatchDto, poiEntity);
            _repo.UpdatePointOfInterestForCity(cityId, poiEntity);
            _repo.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            if (_repo.IsCityExists(cityId) == false)
            {
                return NotFound();
            }

            var poiEntity = _repo.GetPointOfInterestForCity(cityId, id);

            if (poiEntity == null)
            {
                return NotFound();
            }

            _repo.DeletePointOfInterest(poiEntity);
            _repo.Save();

            _mailService.Send(
                "Point of interest deleted", 
                $"Point of interest {poiEntity.Name} with id {id} was deleted.");

            return NoContent();
        }
    }
}
