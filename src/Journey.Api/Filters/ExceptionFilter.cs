using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Journey.Communication.Responses;
using FluentValidation.Validators;

namespace Journey.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context){
            if(context.Exception is JourneyException){
                var journeyException = (JourneyException)context.Exception;

                context.HttpContext.Response.StatusCode = (int)journeyException.GetStatusCode();
                var responseJson = new ResponseErrorsJson(journeyException.GetErrorMessages());
                context.Result = new ObjectResult(responseJson);
            }else{
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var responseJson = new ResponseErrorsJson(new List<string> {"Erro Desconhecido"});
                context.Result = new ObjectResult(responseJson);
            }
        }
    }
}