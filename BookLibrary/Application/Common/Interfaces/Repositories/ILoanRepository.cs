using Domain.Loans;

namespace Application.Common.Interfaces.Repositories;

public interface ILoanRepository
{
    Task<Loan> Add(Loan loan, CancellationToken cancellationToken);
    Task<Loan> Update(Loan loan, CancellationToken cancellationToken);
    Task<Loan> Delete(Loan loan, CancellationToken cancellationToken);
}