using Application.Books.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class BookErrorHandler
{
    public static ObjectResult ToObjectResult(this BookException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                BookNotFoundException => StatusCodes.Status404NotFound,
                BookAlreadyExistsException => StatusCodes.Status409Conflict,
                BookUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Book error handler does not implemented")
            }
        };
    }
}
