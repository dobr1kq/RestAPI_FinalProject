using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Books;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class BookRepository(ApplicationDbContext context) : IBookRepository, IBookQueries
{
    public async Task<Option<Book>> GetByName(string name, CancellationToken cancellationToken)
    {
        var entity = await context.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        return entity == null ? Option.None<Book>() : Option.Some(entity);
    }
    
    public async Task<Option<Book>> GetById(BookId id, CancellationToken cancellationToken)
    {
        var entity = await context.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Book>() : Option.Some(entity);
    }

    public async Task<IReadOnlyList<Book>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Books
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Book> Add(Book book, CancellationToken cancellationToken)
    {
        await context.Books.AddAsync(book, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return book;
    }

    public async Task<Book> Update(Book book, CancellationToken cancellationToken)
    {
        context.Books.Update(book);
        await context.SaveChangesAsync(cancellationToken);

        return book;
    }

    public async Task<Book> Delete(Book book, CancellationToken cancellationToken)
    {
        context.Books.Remove(book);
        await context.SaveChangesAsync(cancellationToken);

        return book;
    }
}