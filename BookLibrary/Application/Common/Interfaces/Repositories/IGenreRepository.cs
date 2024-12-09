using Domain.Genres;

namespace Application.Common.Interfaces.Repositories;

public interface IGenreRepository
{
    Task<Genre> Add(Genre genre, CancellationToken cancellationToken);
    Task<Genre> Update(Genre genre, CancellationToken cancellationToken);
    Task<Genre> Delete(Genre genre, CancellationToken cancellationToken);
}