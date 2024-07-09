using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.Delete
{
    public class DeleteTripByIdUseCase
    {
        public void Execute(Guid id){
            var dbContext = new JourneyDbContext();
            var trip = dbContext.Trips.Include(trip => trip.Activities).FirstOrDefault(trip => trip.Id == id);

            if(trip is null){
                throw new NotFoundException("A viagem não foi encontrada!");
            }

            dbContext.Trips.Remove(trip);

            dbContext.SaveChanges();
        }
    }
}