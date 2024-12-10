using Domain.Authors;

namespace Application.Authors.Exceptions;

public abstract class AuthorException : Exception
{
    protected AuthorException(string message, Exception? innerException = null) 
        : base(message, innerException) { }
}


public class AuthorAlreadyExistsException : AuthorException
{
    public AuthorId AuthorId { get; }
    public AuthorAlreadyExistsException(AuthorId authorId)
        : base($"Author with ID '{authorId}' already exists.")
    {
        AuthorId = authorId;
    }
}

public class InvalidDateOfBirthException : AuthorException
{
    public InvalidDateOfBirthException()
        : base("Date of birth cannot be in the future.") { }
}

public class AuthorUnknownException : AuthorException
{
    public AuthorId AuthorId { get; }
    public AuthorUnknownException(AuthorId authorId, Exception innerException)
        : base($"An unknown error occurred for author with ID '{authorId}'.", innerException)
    {
        AuthorId = authorId;
    }
}

public class AuthorNotFoundException : AuthorException
{
    public AuthorNotFoundException(AuthorId authorId)
        : base($"Author with ID '{authorId}' not found.")
    {
    }
}

