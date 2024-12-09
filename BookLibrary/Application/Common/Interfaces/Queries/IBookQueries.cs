using Domain.Books;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IBookQueries
{
    Task<Option<Book>> GetById(BookId id, CancellationToken cancellationToken);
    Task<Option<Book>> GetByName(string Name, CancellationToken cancellationToken);
    Task<IReadOnlyList<Book>> GetAll(CancellationToken cancellationToken);
}