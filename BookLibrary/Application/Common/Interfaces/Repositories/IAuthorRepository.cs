using Domain.Authors;

namespace Application.Common.Interfaces.Repositories;

public interface IAuthorRepository
{
    
    Task<Author> Add(Author author, CancellationToken cancellationToken);
    Task<Author> Update(Author author, CancellationToken cancellationToken);
    Task<Author> Delete(Author author, CancellationToken cancellationToken);
}