using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MV.Demo.CityInfo.API.Models
{
    public class CityWithPointsOfInterestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPointsOfInterest => PointsOfInterest.Count;
        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; } = new List<PointOfInterestDto>();
    }
}
