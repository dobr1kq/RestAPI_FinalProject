using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Librarians.Exceptions;
using Domain.Librarians;
using MediatR;

namespace Application.Librarians.Commands;

public record CreateLibrarianCommand : IRequest<Result<Librarian, LibrarianException>>
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string TelephoneNumber { get; init; }
}
public class CreateLibrarianCommandHandler : IRequestHandler<CreateLibrarianCommand, Result<Librarian, LibrarianException>>
{
    private readonly ILibrarianRepository librarianRepository;

    public CreateLibrarianCommandHandler(ILibrarianRepository librarianRepository)
    {
        this.librarianRepository = librarianRepository;
    }

    public async Task<Result<Librarian, LibrarianException>> Handle(CreateLibrarianCommand request, CancellationToken cancellationToken)
    {
        var existingLibrarian = await librarianRepository.GetByName(request.FirstName, request.LastName, cancellationToken);

        return await existingLibrarian.Match<Task<Result<Librarian, LibrarianException>>>(
            l => Task.FromResult<Result<Librarian, LibrarianException>>(new LibrarianAlreadyExistsException(l.Id)),
            async () => await CreateEntity(request.FirstName, request.LastName, request.TelephoneNumber, cancellationToken)
        );
    }

    private async Task<Result<Librarian, LibrarianException>> CreateEntity(string firstName, string lastName, string telephoneNumber,
        CancellationToken cancellationToken)
    {
        try
        {
            var librarianId = LibrarianId.New();
            var librarian = Librarian.New(librarianId, firstName, lastName, telephoneNumber);    

            return await librarianRepository.Add(librarian, cancellationToken);
        }
        catch (Exception ex)
        {
            return new LibrarianUnknownException(LibrarianId.Empty(), ex);
        }
    }
}
