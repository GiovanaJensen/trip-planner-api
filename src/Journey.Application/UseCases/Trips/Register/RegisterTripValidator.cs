using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Journey.Communication.Requests;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripValidator : AbstractValidator<RequestRegisterTripJson>
    {
        public RegisterTripValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage("Nome não pode ser vazio!");
            RuleFor(request => request.StartDate.Date).GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("A viagem não pode ser passada para uma data passada!");
            RuleFor(request => request).Must(request => request.EndDate.Date >= request.StartDate.Date).WithMessage("A viagem deve terminar após a data de início!");
        }
    }
}