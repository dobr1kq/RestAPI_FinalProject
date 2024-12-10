using Domain.Readers;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IReaderRepository
{
    Task<Reader> Add(Reader reader, CancellationToken cancellationToken);
    Task<Reader> Update(Reader reader, CancellationToken cancellationToken);
    Task<Reader> Delete(Reader reader, CancellationToken cancellationToken);
    Task<Option<Reader>> GetByName(string FirstName, string LastName, CancellationToken cancellationToken);
    Task<Option<Reader>> GetById(ReaderId id, CancellationToken cancellationToken);
}