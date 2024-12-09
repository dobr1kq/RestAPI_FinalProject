using Domain.Books;

namespace Application.Common.Interfaces.Repositories;

public interface IBookRepository
{
    Task<Book> Add(Book book, CancellationToken cancellationToken);
    Task<Book> Update(Book book, CancellationToken cancellationToken);
    Task<Book> Delete(Book book, CancellationToken cancellationToken);
}