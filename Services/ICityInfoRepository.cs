using MV.Demo.CityInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MV.Demo.CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        IEnumerable<CityEntity> GetAllCities();

        CityEntity GetCity(int cityId, bool includePointsOfInterest);

        IEnumerable<PointOfInterestEntity> GetAllPointsOfInterestForCity(int cityId);

        PointOfInterestEntity GetPointOfInterestForCity(int cityId, int pointOfInterestId);

        bool IsCityExists(int cityId);

        void AddPointOfInterestForCity(int cityId, PointOfInterestEntity pointOfInterest);

        void UpdatePointOfInterestForCity(int cityId, PointOfInterestEntity pointOfInterest);

        void DeletePointOfInterest(PointOfInterestEntity pointOfInterest);

        bool Save();
    }
}
