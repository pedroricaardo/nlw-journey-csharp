using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Journey.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is JourneyException)
            {
                var journeyException = (JourneyException)context.Exception;

                context.HttpContext.Response.StatusCode = (int)journeyException.GetStatusCode();

                var responseJson = new ResponseErrosJson(journeyException.GetErrorsMessage());

                context.Result = new ObjectResult(responseJson);
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                //var list = new List<string>();
                //list.Add("Erro desconhecido.");

                var list = new List<string>
                {
                    "Erro desconhecido."
                };

                var responseJson = new ResponseErrosJson(list);

                //var responseJson = new ResponseErrosJson(new List<string> { "Erro desconhecido." });

                context.Result = new ObjectResult(responseJson);
            }
        }
    }
}
