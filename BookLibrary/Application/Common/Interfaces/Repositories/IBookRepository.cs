using Domain.Books;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IBookRepository
{
    Task<Book> Add(Book book, CancellationToken cancellationToken);
    Task<Book> Update(Book book, CancellationToken cancellationToken);
    Task<Book> Delete(Book book, CancellationToken cancellationToken);
    Task<Option<Book>> GetByName(string Name, CancellationToken cancellationToken);
    Task<Option<Book>> GetById(BookId id, CancellationToken cancellationToken);
}