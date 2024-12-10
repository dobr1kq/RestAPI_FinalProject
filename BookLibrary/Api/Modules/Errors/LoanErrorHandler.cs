using Application.Loans.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class LoanErrorHandler
{
    public static ObjectResult ToObjectResult(this LoanException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                LoanNotFoundException => StatusCodes.Status404NotFound,
                LoanAlreadyExistsException => StatusCodes.Status409Conflict,
                LoanUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Loan error handler does not implemented")
            }
        };
    }
}
