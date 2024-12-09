using Domain.Genres;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IGenreQueries
{
    Task<Option<Genre>> GetById(GenreId id, CancellationToken cancellationToken);
    Task<Option<Genre>> GetByName(string GenreName, CancellationToken cancellationToken);
    Task<IReadOnlyList<Genre>> GetAll(CancellationToken cancellationToken);
}