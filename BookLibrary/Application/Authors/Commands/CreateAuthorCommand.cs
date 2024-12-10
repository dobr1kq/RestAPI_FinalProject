using Application.Common;
using Application.Authors.Exceptions;
using Application.Common.Interfaces.Repositories;
using Domain.Authors;
using MediatR;

namespace Application.Authors.Commands;

public record CreateAuthorCommand : IRequest<Result<Author, AuthorException>>
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Country { get; init; }
    public required DateTime DateOfBirth { get; init; }
}

public class CreateAuthorCommandHandler(
    IAuthorRepository authorRepository)
    : IRequestHandler<CreateAuthorCommand, Result<Author, AuthorException>>
{
    public async Task<Result<Author, AuthorException>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var existingAuthor = await authorRepository.GetByName(request.FirstName, request.LastName, cancellationToken);

        return await existingAuthor.Match<Task<Result<Author, AuthorException>>>(
            a => Task.FromResult<Result<Author, AuthorException>>(new AuthorAlreadyExistsException(a.Id)),
            async () => await CreateEntity(
                request.FirstName,
                request.LastName,
                request.Country,
                request.DateOfBirth,
                cancellationToken));
    }

    private async Task<Result<Author, AuthorException>> CreateEntity(
        string firstName,
        string lastName,
        string country,
        DateTime dateOfBirth,
        CancellationToken cancellationToken)
    {
        if (dateOfBirth > DateTime.UtcNow)
        {
            return new InvalidDateOfBirthException();
        }

        try
        {
            var entity = Author.New(AuthorId.New(), firstName, lastName, country, dateOfBirth);
            
            return await authorRepository.Add(entity, cancellationToken);
        }
        catch (Exception exception)
        {
            return new AuthorUnknownException(AuthorId.Empty(), exception);
        }
    }
}
