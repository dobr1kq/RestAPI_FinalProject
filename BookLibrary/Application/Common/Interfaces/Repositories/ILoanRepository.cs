using Domain.Books;
using Domain.Loans;
using Domain.Readers;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface ILoanRepository
{
    Task<Loan> Add(Loan loan, CancellationToken cancellationToken);
    Task<Loan> Update(Loan loan, CancellationToken cancellationToken);
    Task<Loan> Delete(Loan loan, CancellationToken cancellationToken);
    Task<Option<Loan>> GetById(LoanId id, CancellationToken cancellationToken);
    Task<Option<Loan>> GetByReaderAndBook(ReaderId readerId, BookId bookId, CancellationToken cancellationToken);
}