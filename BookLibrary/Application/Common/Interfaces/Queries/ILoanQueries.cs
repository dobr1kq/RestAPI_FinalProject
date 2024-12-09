using Domain.Loans;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface ILoanQueries
{
    Task<Option<Loan>> GetById(LoanId id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Loan>> GetAll(CancellationToken cancellationToken);
}