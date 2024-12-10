using Domain.Publishers;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IPublisherRepository
{
    Task<Publisher> Add(Publisher publisher, CancellationToken cancellationToken);
    Task<Publisher> Update(Publisher publisher, CancellationToken cancellationToken);
    Task<Publisher> Delete(Publisher publisher, CancellationToken cancellationToken);
    Task<Option<Publisher>> GetByName(string PublisherName, CancellationToken cancellationToken);
    Task<Option<Publisher>> GetById(PublisherId id, CancellationToken cancellationToken);
}