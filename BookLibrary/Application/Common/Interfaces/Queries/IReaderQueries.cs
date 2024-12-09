using Domain.Books;
using Domain.Readers;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IReaderQueries
{
    Task<Option<Reader>> GetById(ReaderId id, CancellationToken cancellationToken);
    Task<Option<Reader>> GetByName(string FirstName, string LastName, CancellationToken cancellationToken);
    Task<IReadOnlyList<Reader>> GetAll(CancellationToken cancellationToken);
}