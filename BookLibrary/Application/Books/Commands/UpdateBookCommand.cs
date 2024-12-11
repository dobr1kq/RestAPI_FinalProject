using Application.Books.Exceptions;
using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Authors;
using Domain.Books;
using Domain.Genres;
using Domain.Publishers;
using MediatR;

namespace Application.Books.Commands;

public record UpdateBookCommand : IRequest<Result<Book, BookException>>
{
    public required Guid BookId { get; init; }
    public required string Name { get; init; }
    public required DateTime Date { get; init; }
    public required Guid GenreId { get; init; }
    public required Guid AuthorId { get; init; }
    public required Guid PublisherId { get; init; }
}
public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Result<Book, BookException>>
{
    private readonly IBookRepository bookRepository;

    public UpdateBookCommandHandler(IBookRepository bookRepository)
    {
        this.bookRepository = bookRepository;
    }

    public async Task<Result<Book, BookException>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var bookId = new BookId(request.BookId);
        var genreId = new GenreId(request.GenreId);
        var authorId = new AuthorId(request.AuthorId);
        var publisherId = new PublisherId(request.PublisherId);

        var existingBook = await bookRepository.GetById(bookId, cancellationToken);

        return await existingBook.Match(
            async b => await UpdateEntity(b, request.Name, request.Date, genreId, authorId, publisherId, cancellationToken),
            () => Task.FromResult<Result<Book, BookException>>(new BookNotFoundException(bookId)));
    }

    private async Task<Result<Book, BookException>> UpdateEntity(Book book, string name, DateTime date,
        GenreId genreId, AuthorId authorId, PublisherId publisherId, CancellationToken cancellationToken)
    {
        try
        {
            book.UpdateBookDetails(name, date, genreId, authorId, publisherId);

            return await bookRepository.Update(book, cancellationToken);
        }
        catch (Exception exception)
        {
            return new BookUnknownException(book.Id, exception);
        }
    }
}
