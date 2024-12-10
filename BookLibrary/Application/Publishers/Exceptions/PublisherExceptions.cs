using Domain.Publishers;

namespace Application.Publishers.Exceptions;

public abstract class PublisherException : Exception
{
    public PublisherId PublisherId { get; }

    protected PublisherException(PublisherId publisherId, string message, Exception? innerException = null)
        : base(message, innerException)
    {
        PublisherId = publisherId;
    }
}

public class PublisherInvalidNameException : PublisherException
{
    public PublisherInvalidNameException(PublisherId publisherId)
        : base(publisherId, "Publisher name cannot be empty or whitespace.") { }
}

public class PublisherInvalidAddressException : PublisherException
{
    public PublisherInvalidAddressException(PublisherId publisherId)
        : base(publisherId, "Publisher address cannot be empty or whitespace.") { }
}

public class PublisherAlreadyExistsException : PublisherException
{
    public PublisherAlreadyExistsException(PublisherId publisherId)
        : base(publisherId, $"Publisher with ID '{publisherId}' already exists.") { }
}

public class PublisherUnknownException : PublisherException
{
    public PublisherUnknownException(PublisherId publisherId, Exception innerException)
        : base(publisherId, $"An unknown error occurred for the publisher with ID '{publisherId}'.", innerException) { }
}

public class PublisherNotFoundException : PublisherException
{
    public PublisherNotFoundException(PublisherId publisherId)
        : base(publisherId, $"Publisher with ID '{publisherId}' not found.") { }
}

