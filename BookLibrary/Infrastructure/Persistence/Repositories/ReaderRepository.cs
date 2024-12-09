using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Readers;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class ReaderRepository(ApplicationDbContext context) : IReaderRepository, IReaderQueries
{
    public async Task<Option<Reader>> GetById(ReaderId id, CancellationToken cancellationToken)
    {
        var entity = await context.Readers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Reader>() : Option.Some(entity);
    }

    
    public async Task<Option<Reader>> GetByName(string firstName, string lastName, CancellationToken cancellationToken)
    {
        var entity = await context.Readers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.FirstName == firstName && x.LastName == lastName, cancellationToken);

        return entity == null ? Option.None<Reader>() : Option.Some(entity);
    }

    public async Task<IReadOnlyList<Reader>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Readers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Reader> Add(Reader reader, CancellationToken cancellationToken)
    {
        await context.Readers.AddAsync(reader, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return reader;
    }

    public async Task<Reader> Update(Reader reader, CancellationToken cancellationToken)
    {
        context.Readers.Update(reader);
        await context.SaveChangesAsync(cancellationToken);

        return reader;
    }

    public async Task<Reader> Delete(Reader reader, CancellationToken cancellationToken)
    {
        context.Readers.Remove(reader);
        await context.SaveChangesAsync(cancellationToken);

        return reader;
    }
}
