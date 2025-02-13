using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Journey.Exception.ExceptionsBase;

namespace Journey.Application.UseCases.Trips.GetById
{
    public class GetByIdUseCase
    {
        public ResponseTripJson Execute(Guid id){
            var dbContext = new JourneyDbContext();
            var trip = dbContext.Trips.Include(trip => trip.Activities).FirstOrDefault(trip => trip.Id == id);

            if(trip is null){
                throw new NotFoundException("A viagem não foi encontrada!");
            }

            return new ResponseTripJson{
                Id = trip.Id,
                Name = trip.Name,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Activities = trip.Activities.Select(activity => new ResponseActivityJson{
                    Id = activity.Id,
                    Name = activity.Name,
                    Date = activity.Date,
                    Status = (Communication.Enums.ActivityStatus)activity.Status
                }).ToList()
            };
        }
    }
}