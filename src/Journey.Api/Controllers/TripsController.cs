using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Application.UseCases.Trips.Register;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetById;
using Journey.Application.UseCases.Trips.Delete;
using Journey.Exception.ExceptionsBase;
using Journey.Application.UseCases.Activities.Register;
using Journey.Application.UseCases.Activities.Complete;
using Journey.Application.UseCases.Activities.Delete;

namespace Journey.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RequestRegisterTripJson request)
        {
            var useCase = new RegisterTripUseCase();
            var response  = useCase.Execute(request);
            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
        public IActionResult GetAll(){
            var useCase = new GetAllUseCase();
            var result = useCase.Execute();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        public IActionResult GetById([FromRoute] Guid id){
           var useCase = new GetByIdUseCase();
           var response = useCase.Execute(id);
           return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent )]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        public IActionResult deleteById([FromRoute] Guid id){
           var useCase = new DeleteTripByIdUseCase();
           useCase.Execute(id);
           return NoContent();
        }

        [HttpPost]
        [Route("{tripId}/activity")]
        [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        public IActionResult RegisterActivity([FromRoute] Guid tripId, [FromBody] RequestRegisterActivityJson request){
            var useCase = new RegisterActivityForTripUseCase();
            var response = useCase.Execute(tripId, request);
            return Created(string.Empty, response);
        }

        [HttpPut]
        [Route("{tripId}/activity/{activityId}/complete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public IActionResult CompleteActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId){
            var useCase = new CompleteActivityForTripUseCase();
            useCase.Execute(tripId, activityId);
            return NoContent();
        }

        [HttpDelete]
        [Route("{tripId}/activity/{activityId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public IActionResult DeleteActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId){
            var useCase = new DeleteActivityForTripUseCase();
            useCase.Execute(tripId, activityId);
            return NoContent();
        }
    }
} 