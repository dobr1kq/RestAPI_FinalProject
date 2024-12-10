using Domain.Books;

namespace Application.Books.Exceptions;

public abstract class BookException : Exception
{
    public BookId BookId { get; }

    protected BookException(BookId bookId, string message, Exception? innerException = null)
        : base(message, innerException)
    {
        BookId = bookId;
    }
}

public class BookAlreadyExistsException : BookException
{
    public BookAlreadyExistsException(BookId bookId) 
        : base(bookId, $"Book with ID '{bookId}' already exists.") { }
}

public class BookUnknownException : BookException
{
    public BookUnknownException(BookId bookId, Exception innerException)
        : base(bookId, $"An unknown error occurred for the book with ID '{bookId}'", innerException) { }
}

public class BookNotFoundException : BookException
{
    public BookNotFoundException(BookId bookId)
        : base(bookId, $"Book with ID {bookId} not found.")
    {
    }
}
