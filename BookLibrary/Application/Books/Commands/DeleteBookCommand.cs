using Application.Books.Exceptions;
using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Books;
using MediatR;

namespace Application.Books.Commands;

public record DeleteBookCommand : IRequest<Result<Book, BookException>>
{
    public required Guid BookId { get; init; }
}
public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result<Book, BookException>>
{
    private readonly IBookRepository bookRepository;

    public DeleteBookCommandHandler(IBookRepository bookRepository)
    {
        this.bookRepository = bookRepository;
    }

    public async Task<Result<Book, BookException>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var bookId = new BookId(request.BookId);

        var existingBook = await bookRepository.GetById(bookId, cancellationToken);

        return await existingBook.Match(
            async b => await DeleteEntity(b, cancellationToken),
            () => Task.FromResult<Result<Book, BookException>>(new BookNotFoundException(bookId)));
    }

    private async Task<Result<Book, BookException>> DeleteEntity(Book book, CancellationToken cancellationToken)
    {
        try
        {
            return await bookRepository.Delete(book, cancellationToken);
        }
        catch (Exception exception)
        {
            return new BookUnknownException(book.Id, exception);
        }
    }
}
