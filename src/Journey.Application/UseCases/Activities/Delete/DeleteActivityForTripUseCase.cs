using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;

namespace Journey.Application.UseCases.Activities.Delete
{
    public class DeleteActivityForTripUseCase
    {
        public void Execute(Guid tripId, Guid activityId){
            var dbContext = new JourneyDbContext();
            var activity = dbContext.Activities.FirstOrDefault(activity => activity.Id == activityId && activity.TripId == tripId);

            if(activity is null){
                throw new NotFoundException("A atividade não foi encontrada!");
            }

            dbContext.Activities.Remove(activity);
            dbContext.SaveChanges();
        }
    }
}