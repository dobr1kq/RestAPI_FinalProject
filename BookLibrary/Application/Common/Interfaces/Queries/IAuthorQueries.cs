using Domain.Authors;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IAuthorQueries
{
    Task<Option<Author>> GetById(AuthorId id, CancellationToken cancellationToken);
    Task<Option<Author>> GetByName(string firstName, string lastName, CancellationToken cancellationToken);
    Task<IReadOnlyList<Author>> GetAll(CancellationToken cancellationToken);
}