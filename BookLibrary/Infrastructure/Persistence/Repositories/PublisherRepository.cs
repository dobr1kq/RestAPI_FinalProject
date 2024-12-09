using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Publishers;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class PublisherRepository(ApplicationDbContext context) : IPublisherRepository, IPublisherQueries
{
    public async Task<Option<Publisher>> GetById(PublisherId id, CancellationToken cancellationToken)
    {
        var entity = await context.Publishers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Publisher>() : Option.Some(entity);
    }

    public async Task<Option<Publisher>> GetByName(string publisherName, CancellationToken cancellationToken)
    {
        var entity = await context.Publishers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.PublisherName == publisherName, cancellationToken);

        return entity == null ? Option.None<Publisher>() : Option.Some(entity);
    }

    public async Task<IReadOnlyList<Publisher>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Publishers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Publisher> Add(Publisher publisher, CancellationToken cancellationToken)
    {
        await context.Publishers.AddAsync(publisher, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return publisher;
    }

    public async Task<Publisher> Update(Publisher publisher, CancellationToken cancellationToken)
    {
        context.Publishers.Update(publisher);
        await context.SaveChangesAsync(cancellationToken);

        return publisher;
    }

    public async Task<Publisher> Delete(Publisher publisher, CancellationToken cancellationToken)
    {
        context.Publishers.Remove(publisher);
        await context.SaveChangesAsync(cancellationToken);

        return publisher;
    }
}
