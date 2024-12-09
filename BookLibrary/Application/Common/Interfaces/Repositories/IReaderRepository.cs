using Domain.Readers;

namespace Application.Common.Interfaces.Repositories;

public interface IReaderRepository
{
    Task<Reader> Add(Reader reader, CancellationToken cancellationToken);
    Task<Reader> Update(Reader reader, CancellationToken cancellationToken);
    Task<Reader> Delete(Reader reader, CancellationToken cancellationToken);
}