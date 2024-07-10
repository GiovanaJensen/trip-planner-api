using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Activities.Register
{
    public class RegisterActivityForTripUseCase
    {
        public ResponseActivityJson Execute(Guid tripId, RequestRegisterActivityJson request){
            var dbContext = new JourneyDbContext();
            var trip = dbContext.Trips.Include(trip => trip.Activities).FirstOrDefault(trip => trip.Id == tripId);

            Validate(trip, request);

            var entity = new Activity{
                Name = request.Name,
                Date = request.Date
            };

            trip.Activities.Add(entity);
            dbContext.Trips.Update(trip);
            dbContext.SaveChanges();

            return new ResponseActivityJson{
                Date = entity.Date,
                Id = entity.Id,
                Name = entity.Name,
                Status = (Communication.Enums.ActivityStatus)entity.Status,
            };
        }

        private void Validate(Trip? trip, RequestRegisterActivityJson request){

            if (trip is null){
                throw new NotFoundException("A viagem não foi encontrada!");
            }

            var validator = new RegisterActivityValidator();
            var result = validator.Validate(request);

            if((request.Date >= trip.StartDate && request.Date <= trip.EndDate) == false){
                result.Errors.Add(new ValidationFailure("Date", "A data que você selecionou para viagem não está no período da viagem"));
            }

            if(result.IsValid == false){
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }  
}