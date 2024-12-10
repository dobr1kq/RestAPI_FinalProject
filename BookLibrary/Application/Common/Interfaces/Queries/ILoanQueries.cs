using Domain.Books;
using Domain.Loans;
using Domain.Readers;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface ILoanQueries
{
    Task<Option<Loan>> GetById(LoanId id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Loan>> GetAll(CancellationToken cancellationToken);
    Task<Option<Loan>> GetByReaderAndBook(ReaderId readerId, BookId bookId, CancellationToken cancellationToken);
}