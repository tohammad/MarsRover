using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace MarsRoverApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HandleException : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is Models.RoverException)
                context.Result = new JsonResult(context.Exception?.Message) { StatusCode = StatusCodes.Status400BadRequest };
            else
                context.Result = new JsonResult(context.Exception?.Message) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }
}