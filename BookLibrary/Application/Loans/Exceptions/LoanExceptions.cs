using Domain.Loans;

namespace Application.Loans.Exceptions;

public abstract class LoanException : Exception
{
    public LoanId LoanId { get; }

    protected LoanException(LoanId loanId, string message, Exception? innerException = null)
        : base(message, innerException)
    {
        LoanId = loanId;
    }
}

public class LoanAlreadyExistsException : LoanException
{
    public LoanAlreadyExistsException(LoanId loanId) 
        : base(loanId, $"Loan with ID '{loanId}' already exists.") { }
}

public class LoanUnknownException : LoanException
{
    public LoanUnknownException(LoanId loanId, Exception innerException)
        : base(loanId, $"An unknown error occurred for the loan with ID '{loanId}'", innerException) { }
}

public class LoanNotFoundException : LoanException
{
    public LoanNotFoundException(LoanId loanId)
        : base(loanId, $"Loan with ID '{loanId}' not found.") { }
}

public class LoanInvalidReturnDateException : LoanException
{
    public LoanInvalidReturnDateException(LoanId loanId)
        : base(loanId, $"Loan with ID '{loanId}' has an invalid return date.") { }
}


