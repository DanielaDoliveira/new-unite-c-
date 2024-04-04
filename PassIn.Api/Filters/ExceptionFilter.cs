
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using PassIn.Communication.Responses;
using PassIn.Exceptions;

namespace PassIn.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var result = context.Exception is PassInException;
        if (result)
        {
            HandleProjectException();

        }
        else
        {
            ThrowUnknownError(context);
        }

    }

    private void HandleProjectException()
    {

    }
    private void ThrowUnknownError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //context.Result = new ObjectResult(new ResponseErrorJson(" Unknown Error. "));
    }
}