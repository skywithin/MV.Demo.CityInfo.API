using Microsoft.EntityFrameworkCore;
using MV.Demo.CityInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MV.Demo.CityInfo.API.Contexts
{
    public class CityInfoContext : DbContext
    {
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<PointOfInterestEntity> PointsOfInterest { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CityEntity>()
                        .HasData(
                            new CityEntity
                            {
                                Id = 1,
                                Name = "New York City",
                                Description = "The one with that big park"
                            },
                            new CityEntity
                            {
                                Id = 2,
                                Name = "Sydney",
                                Description = "The one with the Opera House"
                            },
                            new CityEntity
                            {
                                Id = 3,
                                Name = "Paris",
                                Description = "The one with an Eiffel Tower"
                            }
                        );

            modelBuilder.Entity<PointOfInterestEntity>()
                        .HasData(
                            new PointOfInterestEntity
                            {
                                Id  = 1,
                                CityId = 1,
                                Name = "Centenial Park",
                                Description = "The most visited urban park in the US"
                            },
                            new PointOfInterestEntity
                            {
                                Id = 2,
                                CityId = 1,
                                Name = "Empire State Building",
                                Description = "A 102-story skyscraper"
                            },
                            new PointOfInterestEntity
                            {
                                Id = 3,
                                CityId = 2,
                                Name = "Opera House",
                                Description = "Where fat lady sings"
                            },
                            new PointOfInterestEntity
                            {
                                Id = 4,
                                CityId = 2,
                                Name = "Harbour Bridge",
                                Description = "Australian heritage-listed steel through arch bridge across Sydney Harbour"
                            },
                            new PointOfInterestEntity
                            {
                                Id = 5,
                                CityId = 3,
                                Name = "Eiffel Tower",
                                Description = "A wrought iron lattice tower"
                            },
                            new PointOfInterestEntity
                            {
                                Id = 6,
                                CityId = 3,
                                Name = "The Louvre",
                                Description = "The world's largest museum"
                            }
                        );

            base.OnModelCreating(modelBuilder);
        }
    }
}
