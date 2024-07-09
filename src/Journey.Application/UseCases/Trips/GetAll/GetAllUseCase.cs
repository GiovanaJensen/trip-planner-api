using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Journey.Communication.Responses;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.GetAll
{
    public class GetAllUseCase
    {
        public ResponseTripsJson Execute(){
            var dbContext = new JourneyDbContext();

            var trips = dbContext.Trips.ToList();

            return new ResponseTripsJson{
                Trips = trips.Select(trip => new ResponseShortTripJson{
                    Name = trip.Name,
                    Id = trip.Id,
                    StartDate = trip.StartDate,
                    EndDate = trip.EndDate
                }).ToList()
            };
        }
    }
}