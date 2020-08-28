using Microsoft.EntityFrameworkCore;
using MV.Demo.CityInfo.API.Contexts;
using MV.Demo.CityInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MV.Demo.CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context;
        }

        public IEnumerable<CityEntity> GetAllCities()
        {
            return _context.Cities.OrderBy(c => c.Name).ToList();
        }

        public CityEntity GetCity(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return _context.Cities
                               .Include(c => c.PointsOfInterest)
                               .Where(c => c.Id == cityId)
                               .FirstOrDefault();
            }

            return _context.Cities
                           .Where(c => c.Id == cityId)
                           .FirstOrDefault();
        }

        public IEnumerable<PointOfInterestEntity> GetAllPointsOfInterestForCity(int cityId)
        {
            return _context.PointsOfInterest
                           .Where(poi => poi.CityId == cityId)
                           .ToList();
        }

        public PointOfInterestEntity GetPointOfInterestForCity(int cityId, int pointOfInterestId)
        {
            return _context.PointsOfInterest
                           .Where(poi => poi.CityId == cityId && poi.Id == pointOfInterestId)
                           .FirstOrDefault();
        }

        public bool IsCityExists(int cityId)
        {
            return _context.Cities.Any(c => c.Id == cityId);
        }

        public void AddPointOfInterestForCity(int cityId, PointOfInterestEntity pointOfInterest)
        {
            var city = GetCity(cityId, includePointsOfInterest: false);
            city.PointsOfInterest.Add(pointOfInterest);
        }

        public void UpdatePointOfInterestForCity(int cityId, PointOfInterestEntity pointOfInterest)
        {
            //
        }
        
        public void DeletePointOfInterest(PointOfInterestEntity pointOfInterest)
        {
            _context.Remove(pointOfInterest);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

    }
}
