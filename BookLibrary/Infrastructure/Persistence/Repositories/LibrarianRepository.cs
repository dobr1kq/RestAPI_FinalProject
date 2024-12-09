using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Librarians;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class LibrarianRepository(ApplicationDbContext context) : ILibrarianRepository, ILibrarianQueries
{
    public async Task<Option<Librarian>> GetById(LibrarianId id, CancellationToken cancellationToken)
    {
        var entity = await context.Librarians
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Librarian>() : Option.Some(entity);
    }

    public async Task<Option<Librarian>> GetByName(string firstName, string lastName, CancellationToken cancellationToken)
    {
        var entity = await context.Librarians
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.FirstName == firstName && x.LastName == lastName, cancellationToken);

        return entity == null ? Option.None<Librarian>() : Option.Some(entity);
    }

    public async Task<IReadOnlyList<Librarian>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Librarians
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Librarian> Add(Librarian librarian, CancellationToken cancellationToken)
    {
        await context.Librarians.AddAsync(librarian, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return librarian;
    }

    public async Task<Librarian> Update(Librarian librarian, CancellationToken cancellationToken)
    {
        context.Librarians.Update(librarian);
        await context.SaveChangesAsync(cancellationToken);

        return librarian;
    }

    public async Task<Librarian> Delete(Librarian librarian, CancellationToken cancellationToken)
    {
        context.Librarians.Remove(librarian);
        await context.SaveChangesAsync(cancellationToken);

        return librarian;
    }
}
