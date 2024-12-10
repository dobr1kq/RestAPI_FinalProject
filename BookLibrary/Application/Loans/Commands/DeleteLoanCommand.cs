using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Loans.Exceptions;
using Domain.Loans;
using MediatR;

namespace Application.Loans.Commands;

public record DeleteLoanCommand : IRequest<Result<Loan, LoanException>>
{
    public required Guid LoanId { get; init; }
}
public class DeleteLoanCommandHandler : IRequestHandler<DeleteLoanCommand, Result<Loan, LoanException>>
{
    private readonly ILoanRepository loanRepository;

    public DeleteLoanCommandHandler(ILoanRepository loanRepository)
    {
        this.loanRepository = loanRepository;
    }

    public async Task<Result<Loan, LoanException>> Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
    {
        var loanId = new LoanId(request.LoanId);
        var existingLoan = await loanRepository.GetById(loanId, cancellationToken);

        return await existingLoan.Match<Task<Result<Loan, LoanException>>>(
            async loan => await DeleteEntity(loan, cancellationToken),
            () => Task.FromResult<Result<Loan, LoanException>>(new LoanNotFoundException(loanId)));
    }

    private async Task<Result<Loan, LoanException>> DeleteEntity(Loan loan, CancellationToken cancellationToken)
    {
        try
        {
            return await loanRepository.Delete(loan, cancellationToken);
        }
        catch (Exception ex)
        {
            return new LoanUnknownException(loan.Id, ex);
        }
    }
}
