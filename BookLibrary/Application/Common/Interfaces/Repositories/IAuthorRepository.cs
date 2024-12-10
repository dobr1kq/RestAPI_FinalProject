using Domain.Authors;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IAuthorRepository
{
    
    Task<Author> Add(Author author, CancellationToken cancellationToken);
    Task<Author> Update(Author author, CancellationToken cancellationToken);
    Task<Author> Delete(Author author, CancellationToken cancellationToken);
    Task<Option<Author>> GetByName(string firstName, string lastName, CancellationToken cancellationToken);
    Task<Option<Author>> GetById(AuthorId id, CancellationToken cancellationToken);
}