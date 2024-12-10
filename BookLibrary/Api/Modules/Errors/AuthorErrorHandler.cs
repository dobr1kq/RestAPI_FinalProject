using Application.Authors.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class AuthorErrorHandler
{
    public static ObjectResult ToObjectResult(this AuthorException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                AuthorNotFoundException => StatusCodes.Status404NotFound,
                AuthorAlreadyExistsException => StatusCodes.Status409Conflict,
                AuthorUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Author error handler does not implemented")
            }
        };
    }
}
