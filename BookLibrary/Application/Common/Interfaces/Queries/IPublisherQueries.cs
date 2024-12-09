using Domain.Publishers;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IPublisherQueries
{
    Task<Option<Publisher>> GetById(PublisherId id, CancellationToken cancellationToken);
    Task<Option<Publisher>> GetByName(string PublisherName, CancellationToken cancellationToken);
    Task<IReadOnlyList<Publisher>> GetAll(CancellationToken cancellationToken);
}