using Domain.Publishers;

namespace Application.Common.Interfaces.Repositories;

public interface IPublisherRepository
{
    Task<Publisher> Add(Publisher publisher, CancellationToken cancellationToken);
    Task<Publisher> Update(Publisher publisher, CancellationToken cancellationToken);
    Task<Publisher> Delete(Publisher publisher, CancellationToken cancellationToken);
}