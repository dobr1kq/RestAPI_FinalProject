using Domain.Librarians;

namespace Application.Common.Interfaces.Repositories;

public interface ILibrarianRepository
{
    Task<Librarian> Add(Librarian librarian, CancellationToken cancellationToken);
    Task<Librarian> Update(Librarian librarian, CancellationToken cancellationToken);
    Task<Librarian> Delete(Librarian librarian, CancellationToken cancellationToken);
}