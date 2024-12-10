using Domain.Librarians;

namespace Application.Librarians.Exceptions;

public class LibrarianException : Exception
{
    public LibrarianId LibrarianId { get; }
    
    protected LibrarianException(LibrarianId librarianId, string message, Exception? innerException = null)
        : base(message)
    {
        LibrarianId = librarianId;
    }
}

public class LibrarianAlreadyExistsException : LibrarianException
{
    public LibrarianAlreadyExistsException(LibrarianId librarianId)
        : base(librarianId, $"Librarian with ID {librarianId} already exists.")
    {
    }
}

public class LibrarianNotFoundException : LibrarianException
{
    public LibrarianNotFoundException(LibrarianId librarianId)
        : base(librarianId, $"Librarian with ID {librarianId} not found.")
    {
    }
}

public class LibrarianUnknownException : LibrarianException
{
    public LibrarianUnknownException(LibrarianId librarianId, Exception innerException)
        : base(librarianId, $"An unknown error occurred for librarian with ID {librarianId}.", innerException)
    {
    }
}

public class InvalidLibrarianDetailsException : LibrarianException
{
    public InvalidLibrarianDetailsException(string message)
        : base(LibrarianId.Empty(), message)
    {
    }
}
