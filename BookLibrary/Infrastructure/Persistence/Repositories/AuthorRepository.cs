using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Authors;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class AuthorRepository(ApplicationDbContext context) : IAuthorRepository, IAuthorQueries
{
    public async Task<Option<Author>> GetById(AuthorId id, CancellationToken cancellationToken)
    {
        var entity = await context.Authors
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Author>() : Option.Some(entity);
    }

    public async Task<Option<Author>> GetByName(string firstName, string lastName, CancellationToken cancellationToken)
    {
        var entity = await context.Authors
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.FirstName == firstName && x.LastName == lastName, cancellationToken);

        return entity == null ? Option.None<Author>() : Option.Some(entity);
    }

    public async Task<IReadOnlyList<Author>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Authors
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Author> Add(Author author, CancellationToken cancellationToken)
    {
        await context.Authors.AddAsync(author, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return author;
    }

    public async Task<Author> Update(Author author, CancellationToken cancellationToken)
    {
        context.Authors.Update(author);
        await context.SaveChangesAsync(cancellationToken);

        return author;
    }

    public async Task<Author> Delete(Author author, CancellationToken cancellationToken)
    {
        context.Authors.Remove(author);
        await context.SaveChangesAsync(cancellationToken);

        return author;
    }
}
