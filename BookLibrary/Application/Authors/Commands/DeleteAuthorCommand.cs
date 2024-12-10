using Application.Authors.Exceptions;
using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Authors;
using MediatR;

namespace Application.Authors.Commands;

public record DeleteAuthorCommand : IRequest<Result<Author, AuthorException>>
{
    public required Guid AuthorId { get; init; }
}
public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Result<Author, AuthorException>>
{
    private readonly IAuthorRepository authorRepository;

    public DeleteAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        this.authorRepository = authorRepository;
    }

    public async Task<Result<Author, AuthorException>> Handle(
        DeleteAuthorCommand request,
        CancellationToken cancellationToken)
    {
        var authorId = new AuthorId(request.AuthorId);

        var existingAuthor = await authorRepository.GetById(authorId, cancellationToken);

        return await existingAuthor.Match<Task<Result<Author, AuthorException>>>( 
            async a => await DeleteEntity(a, cancellationToken),
            () => Task.FromResult<Result<Author, AuthorException>>(new AuthorNotFoundException(authorId)));
    }

    public async Task<Result<Author, AuthorException>> DeleteEntity(Author author, CancellationToken cancellationToken)
    {
        try
        {
            return await authorRepository.Delete(author, cancellationToken);
        }
        catch (Exception exception)
        {
            return new AuthorUnknownException(author.Id, exception);
        }
    }
}
