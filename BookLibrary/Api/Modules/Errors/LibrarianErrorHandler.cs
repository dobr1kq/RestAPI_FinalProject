using Application.Librarians.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class LibrarianErrorHandler
{
    public static ObjectResult ToObjectResult(this LibrarianException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                LibrarianNotFoundException => StatusCodes.Status404NotFound,
                LibrarianAlreadyExistsException => StatusCodes.Status409Conflict,
                LibrarianUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Librarian error handler does not implemented")
            }
        };
    }
}
