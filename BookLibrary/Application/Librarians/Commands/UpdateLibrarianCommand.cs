using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Librarians.Exceptions;
using Domain.Librarians;
using MediatR;

namespace Application.Librarians.Commands;

public record UpdateLibrarianCommand : IRequest<Result<Librarian, LibrarianException>>
{
    public required Guid LibrarianId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string TelephoneNumber { get; init; }
}
public class UpdateLibrarianCommandHandler : IRequestHandler<UpdateLibrarianCommand, Result<Librarian, LibrarianException>>
{
    private readonly ILibrarianRepository librarianRepository;

    public UpdateLibrarianCommandHandler(ILibrarianRepository librarianRepository)
    {
        this.librarianRepository = librarianRepository;
    }

    public async Task<Result<Librarian, LibrarianException>> Handle(UpdateLibrarianCommand request, CancellationToken cancellationToken)
    {
        var librarianId = new LibrarianId(request.LibrarianId);
        
        var existingLibrarian = await librarianRepository.GetById(librarianId, cancellationToken);

        return await existingLibrarian.Match<Task<Result<Librarian, LibrarianException>>>(
            async librarian =>
            {
                librarian.UpdateDetails(request.FirstName, request.LastName, request.TelephoneNumber);
                return await librarianRepository.Update(librarian, cancellationToken);
            },
            () => Task.FromResult<Result<Librarian, LibrarianException>>(new LibrarianNotFoundException(librarianId))
        );
    }
}
