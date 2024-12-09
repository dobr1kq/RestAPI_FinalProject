using Domain.Librarians;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface ILibrarianQueries
{
    Task<Option<Librarian>> GetById(LibrarianId id, CancellationToken cancellationToken);
    Task<Option<Librarian>> GetByName(string FirstName, string LastName, CancellationToken cancellationToken);
    Task<IReadOnlyList<Librarian>> GetAll(CancellationToken cancellationToken);
}