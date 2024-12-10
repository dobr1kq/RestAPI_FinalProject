using Application.Publishers.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class PublisherErrorHandler
{
    public static ObjectResult ToObjectResult(this PublisherException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                PublisherNotFoundException => StatusCodes.Status404NotFound,
                PublisherAlreadyExistsException => StatusCodes.Status409Conflict,
                PublisherUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Publisher error handler does not implemented")
            }
        };
    }
}
