using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Journey.Communication.Requests;
using Journey.Application.UseCases.Trips.Register;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Exception.ExceptionsBase;

namespace Journey.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Register([FromBody] RequestRegisterTripJson request)
        {
            try{
                var useCase = new RegisterTripUseCase();
                var response  = useCase.Execute(request);
                return Created(string.Empty, response);
            }
            catch (JourneyException ex){
                return BadRequest(ex.Message);
            }
            catch{
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro desconhecido");
            }
        }

        [HttpGet]
        public IActionResult GetAll(){
            var useCase = new GetAllUseCase();
            var result = useCase.Execute();
            return Ok(result);
        }
    }
}