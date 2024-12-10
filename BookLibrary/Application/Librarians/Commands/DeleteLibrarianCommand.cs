using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Librarians.Exceptions;
using Domain.Librarians;
using MediatR;

namespace Application.Librarians.Commands;

public record DeleteLibrarianCommand : IRequest<Result<Librarian, LibrarianException>>
{
    public required LibrarianId LibrarianId { get; init; }
}
public class DeleteLibrarianCommandHandler : IRequestHandler<DeleteLibrarianCommand, Result<Librarian, LibrarianException>>
{
    private readonly ILibrarianRepository librarianRepository;

    public DeleteLibrarianCommandHandler(ILibrarianRepository librarianRepository)
    {
        this.librarianRepository = librarianRepository;
    }

    public async Task<Result<Librarian, LibrarianException>> Handle(DeleteLibrarianCommand request, CancellationToken cancellationToken)
    {
        var existingLibrarian = await librarianRepository.GetById(request.LibrarianId, cancellationToken);

        return await existingLibrarian.Match<Task<Result<Librarian, LibrarianException>>>(
            async librarian =>
            {
                try
                {
                    return await librarianRepository.Delete(librarian, cancellationToken);
                }
                catch (Exception ex)
                {
                    return new LibrarianUnknownException(librarian.Id, ex);
                }
            },
            () => Task.FromResult<Result<Librarian, LibrarianException>>(new LibrarianNotFoundException(request.LibrarianId))
        );
    }
}
