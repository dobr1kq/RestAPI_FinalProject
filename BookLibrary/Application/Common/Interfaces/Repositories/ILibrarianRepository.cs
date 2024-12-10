using Domain.Librarians;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface ILibrarianRepository
{
    Task<Librarian> Add(Librarian librarian, CancellationToken cancellationToken);
    Task<Librarian> Update(Librarian librarian, CancellationToken cancellationToken);
    Task<Librarian> Delete(Librarian librarian, CancellationToken cancellationToken);
    Task<Option<Librarian>> GetById(LibrarianId id, CancellationToken cancellationToken);
    Task<Option<Librarian>> GetByName(string FirstName, string LastName, CancellationToken cancellationToken);
}