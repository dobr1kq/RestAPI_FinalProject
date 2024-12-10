using Application.Authors.Exceptions;
using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Authors;
using MediatR;

namespace Application.Authors.Commands;

public record UpdateAuthorCommand : IRequest<Result<Author, AuthorException>>
{
    public required Guid AuthorId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Country { get; init; }
    public required DateTime DateOfBirth { get; init; }
}
public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Result<Author, AuthorException>>
{
    private readonly IAuthorRepository authorRepository;

    public UpdateAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        this.authorRepository = authorRepository;
    }

    public async Task<Result<Author, AuthorException>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorId = new AuthorId(request.AuthorId);

        var existingAuthor = await authorRepository.GetById(authorId, cancellationToken);

        return await existingAuthor.Match(
            async a => await UpdateEntity(a, request.FirstName, request.LastName, request.Country, request.DateOfBirth, cancellationToken),
            () => Task.FromResult<Result<Author, AuthorException>>(new AuthorNotFoundException(authorId)));
    }

    private async Task<Result<Author, AuthorException>> UpdateEntity(
        Author author,
        string firstName,
        string lastName,
        string country,
        DateTime dateOfBirth,
        CancellationToken cancellationToken)
    {
        try
        {
            author.UpdateAuthorDetails(firstName, lastName, country, dateOfBirth);

            return await authorRepository.Update(author, cancellationToken);
        }
        catch (Exception exception)
        {
            return new AuthorUnknownException(author.Id, exception);
        }
    }
}
