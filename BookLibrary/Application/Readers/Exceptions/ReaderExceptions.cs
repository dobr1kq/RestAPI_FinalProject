using Domain.Readers;

namespace Application.Common.Readers.Exceptions;

public abstract class ReaderException : Exception
{
    public ReaderId ReaderId { get; }

    protected ReaderException(ReaderId readerId, string message, Exception? innerException = null)
        : base(message, innerException)
    {
        ReaderId = readerId;
    }
}

public class ReaderAlreadyExistsException : ReaderException
{
    public ReaderAlreadyExistsException(ReaderId readerId)
        : base(readerId, $"Reader with ID '{readerId}' already exists.") { }
}

public class ReaderUnknownException : ReaderException
{
    public ReaderUnknownException(ReaderId readerId, Exception innerException)
        : base(readerId, $"An unknown error occurred for the reader with ID '{readerId}'", innerException) { }
}

public class InvalidReaderNameException : ReaderException
{
    public InvalidReaderNameException(string parameter, string value)
        : base(ReaderId.Empty(), $"Invalid {parameter}: '{value}'.") { }
}

public class InvalidReaderPhoneException : ReaderException
{
    public InvalidReaderPhoneException(string phone)
        : base(ReaderId.Empty(), $"Invalid phone number: '{phone}'.") { }
}
public class ReaderNotFoundException : ReaderException
{
    public ReaderNotFoundException(ReaderId readerId)
        : base(readerId, $"Reader with ID {readerId} not found.") { }
}
