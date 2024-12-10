using Domain.Genres;

namespace Application.Genres.Exceptions;

public abstract class GenreException : Exception
{
    public GenreId GenreId { get; }

    protected GenreException(GenreId genreId, string message, Exception? innerException = null)
        : base(message, innerException)
    {
        GenreId = genreId;
    }
}

public class GenreAlreadyExistsException : GenreException
{
    public GenreAlreadyExistsException(GenreId genreId)
        : base(genreId, $"Genre with ID '{genreId}' already exists.") { }
}


public class GenreNotFoundException : GenreException
{
    public GenreNotFoundException(GenreId genreId)
        : base(genreId, $"Genre with ID '{genreId}' not found.") { }
}

public class GenreUnknownException : GenreException
{
    public GenreUnknownException(GenreId genreId, Exception innerException)
        : base(genreId, $"An unknown error occurred for genre with ID '{genreId}'.", innerException) { }
}

public class InvalidGenreNameException : GenreException
{
    public InvalidGenreNameException(GenreId genreId)
        : base(genreId, "Genre name is invalid or not allowed.") { }
}
