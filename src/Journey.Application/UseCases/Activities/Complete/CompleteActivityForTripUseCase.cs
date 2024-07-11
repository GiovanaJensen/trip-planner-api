using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Activities.Complete
{
    public class CompleteActivityForTripUseCase
    {
        public void Execute(Guid tripId, Guid activityId){
            var dbContext = new JourneyDbContext();

            var activity = dbContext.Activities.FirstOrDefault(activity => activity.Id == activityId && activity.TripId == tripId);

            if(activity is null){
                throw new NotFoundException("A atividade não foi encontrada!");
            }

            activity.Status = ActivityStatus.Done;
            dbContext.SaveChanges();   
        }
    }
}