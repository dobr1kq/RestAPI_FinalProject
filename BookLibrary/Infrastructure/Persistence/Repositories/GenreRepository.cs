using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Genres;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class GenreRepository(ApplicationDbContext context) : IGenreRepository, IGenreQueries
{
    public async Task<Option<Genre>> GetById(GenreId id, CancellationToken cancellationToken)
    {
        var entity = await context.Genres
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Genre>() : Option.Some(entity);
    }

    public async Task<Option<Genre>> GetByName(string genreName, CancellationToken cancellationToken)
    {
        var entity = await context.Genres
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.GenreName == genreName, cancellationToken);

        return entity == null ? Option.None<Genre>() : Option.Some(entity);
    }

    public async Task<IReadOnlyList<Genre>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Genres
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Genre> Add(Genre genre, CancellationToken cancellationToken)
    {
        await context.Genres.AddAsync(genre, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return genre;
    }

    public async Task<Genre> Update(Genre genre, CancellationToken cancellationToken)
    {
        context.Genres.Update(genre);
        await context.SaveChangesAsync(cancellationToken);

        return genre;
    }

    public async Task<Genre> Delete(Genre genre, CancellationToken cancellationToken)
    {
        context.Genres.Remove(genre);
        await context.SaveChangesAsync(cancellationToken);

        return genre;
    }
}