using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Books;
using Domain.Loans;
using Domain.Readers;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class LoanRepository(ApplicationDbContext context) : ILoanRepository, ILoanQueries
{
    public async Task<Option<Loan>> GetById(LoanId id, CancellationToken cancellationToken)
    {
        var entity = await context.Loans
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Loan>() : Option.Some(entity);
    }
    
    public async Task<IReadOnlyList<Loan>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Loans
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<Option<Loan>> GetByReaderAndBook(ReaderId readerId, BookId bookId, CancellationToken cancellationToken)
    {
        var entity = await context.Loans
            .AsNoTracking()
            .FirstOrDefaultAsync(
                l => l.ReaderId == readerId && l.BookId == bookId,
                cancellationToken);

        return entity == null ? Option.None<Loan>() : Option.Some(entity);
    }

    public async Task<Loan> Add(Loan loan, CancellationToken cancellationToken)
    {
        await context.Loans.AddAsync(loan, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return loan;
    }

    public async Task<Loan> Update(Loan loan, CancellationToken cancellationToken)
    {
        context.Loans.Update(loan);
        await context.SaveChangesAsync(cancellationToken);

        return loan;
    }

    public async Task<Loan> Delete(Loan loan, CancellationToken cancellationToken)
    {
        context.Loans.Remove(loan);
        await context.SaveChangesAsync(cancellationToken);

        return loan;
    }
}
