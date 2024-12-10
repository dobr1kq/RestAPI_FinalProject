using Application.Common;
using Application.Books.Exceptions;
using Application.Common.Interfaces.Repositories;
using Domain.Books;
using MediatR;

namespace Application.Books.Commands;

public record CreateBookCommand : IRequest<Result<Book, BookException>>
{
    public required string Name { get; init; }
    public required DateTime Date { get; init; }
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

        return await existingBook.Match<Task<Result<Book, BookException>>>( 
            b => Task.FromResult<Result<Book, BookException>>(new BookAlreadyExistsException(b.Id)),
            async () => await CreateEntity(request.Name, request.Date, cancellationToken)
        );
    }

    private async Task<Result<Book, BookException>> CreateEntity(string name, DateTime date, CancellationToken cancellationToken)
    {
        try
        {
            var bookId = BookId.New();
            var book = Book.New(bookId, name, date);    
            return await bookRepository.Add(book, cancellationToken);
        }
        catch (Exception ex)
        {
            return new BookUnknownException(BookId.Empty(), ex);
        }
    }
}
