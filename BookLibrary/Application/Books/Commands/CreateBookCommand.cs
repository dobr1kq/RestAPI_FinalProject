using Application.Common;
using Application.Books.Exceptions;
using Application.Common.Interfaces.Repositories;
using Domain.Books;
using Domain.Genres;
using Domain.Authors;
using Domain.Publishers;
using MediatR;

namespace Application.Books.Commands;

public record CreateBookCommand : IRequest<Result<Book, BookException>>
{
    public required string Name { get; init; }
    public required DateTime Date { get; init; }
    public required Guid GenreId { get; init; }
    public required Guid AuthorId { get; init; }
    public required Guid PublisherId { get; init; }
}
public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<Book, BookException>>
{
    private readonly IBookRepository bookRepository;

    public CreateBookCommandHandler(IBookRepository bookRepository)
    {
        this.bookRepository = bookRepository;
    }

    public async Task<Result<Book, BookException>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var existingBook = await bookRepository.GetByName(request.Name, cancellationToken);
        var genreId = new GenreId(request.GenreId);
        var authorId = new AuthorId(request.AuthorId);
        var pudlisherId = new PublisherId(request.PublisherId);

        return await existingBook.Match<Task<Result<Book, BookException>>>( 
            b => Task.FromResult<Result<Book, BookException>>(new BookAlreadyExistsException(b.Id)),
            async () => await CreateEntity(request.Name, request.Date, genreId, authorId, pudlisherId, cancellationToken)
        );
    }

    private async Task<Result<Book, BookException>> CreateEntity(string name, DateTime date, GenreId genreId, AuthorId authorId,
        PublisherId publisherId, CancellationToken cancellationToken)
    {
        try
        {
            var bookId = BookId.New();
            var book = Book.New(bookId, name, date,genreId, authorId, publisherId);    
            return await bookRepository.Add(book, cancellationToken);
        }
        catch (Exception ex)
        {
            return new BookUnknownException(BookId.Empty(), ex);
        }
    }
}
