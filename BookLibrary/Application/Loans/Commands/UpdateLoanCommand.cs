using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Loans.Exceptions;
using Domain.Loans;
using MediatR;

namespace Application.Loans.Commands;

public record UpdateLoanCommand : IRequest<Result<Loan, LoanException>>
{
    public required Guid LoanId { get; init; }
    public required DateTime LoanDate { get; init; }
    public required DateTime ReturnDate { get; init; }
}
public class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, Result<Loan, LoanException>>
{
    private readonly ILoanRepository loanRepository;

    public UpdateLoanCommandHandler(ILoanRepository loanRepository)
    {
        this.loanRepository = loanRepository;
    }

    public async Task<Result<Loan, LoanException>> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
    {
        var loanId = new LoanId(request.LoanId);

        var existingLoan = await loanRepository.GetById(loanId, cancellationToken);

        return await existingLoan.Match(
            async loan => await UpdateEntity(loan, request.LoanDate, request.ReturnDate, cancellationToken),
            () => Task.FromResult<Result<Loan, LoanException>>(new LoanNotFoundException(loanId))
        );
    }

    private async Task<Result<Loan, LoanException>> UpdateEntity(
        Loan loan,
        DateTime loanDate,
        DateTime returnDate,
        CancellationToken cancellationToken)
    {
        try
        {
            loan.UpdateDetails(loanDate, returnDate);
            return await loanRepository.Update(loan, cancellationToken);
        }
        catch (LoanInvalidReturnDateException ex)
        {
            return ex;
        }
        catch (Exception ex)
        {
            return new LoanUnknownException(loan.Id, ex);
        }
    }
}
