using MV.Demo.CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MV.Demo.CityInfo.API
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public List<CityWithPointsOfInterestDto> Cities { get; set; }

        public CitiesDataStore()
        {
            // init dummy data
            Cities = new List<CityWithPointsOfInterestDto>
            {
                new CityWithPointsOfInterestDto
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The one with that big park",
                    PointsOfInterest = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto { Id = 1, Name = "Centenial Park", Description = "The most visited urban park in the US" },
                        new PointOfInterestDto { Id = 2, Name = "Empire State Building", Description = "A 102-story skyscraper" },
                    }
                },
                new CityWithPointsOfInterestDto
                {
                    Id = 2,
                    Name = "Sydney",
                    Description = "The one with the Opera House",
                    PointsOfInterest = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto { Id = 3, Name = "Opera House", Description = "Where fat lady sings" },
                        new PointOfInterestDto { Id = 4, Name = "Harbour Bridge", Description = "Australian heritage-listed steel through arch bridge across Sydney Harbour" },
                    }
                },
                new CityWithPointsOfInterestDto
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "The one with an Eiffel Tower",
                    PointsOfInterest = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto { Id = 5, Name = "Eiffel Tower", Description = "A wrought iron lattice tower" },
                        new PointOfInterestDto { Id = 6, Name = "The Louvre", Description = "The world's largest museum" },
                    }
                },
            };
        }
    }
}
