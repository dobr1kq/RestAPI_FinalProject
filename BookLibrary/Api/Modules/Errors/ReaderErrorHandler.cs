using Application.Common.Readers.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class ReaderErrorHandler
{
    public static ObjectResult ToObjectResult(this ReaderException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                ReaderNotFoundException => StatusCodes.Status404NotFound,
                ReaderAlreadyExistsException => StatusCodes.Status409Conflict,
                ReaderUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Reader error handler does not implemented")
            }
        };
    }
}
