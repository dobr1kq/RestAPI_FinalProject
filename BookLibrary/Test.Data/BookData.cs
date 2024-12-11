using Domain.Authors;
using Domain.Books;
using Domain.Genres;
using Domain.Publishers;
using Domain.Readers;

namespace Test.Data;

public static class BookData
{
    public static Book MainBook => Book.New(
        BookId.New(),
        "Test Book",
        DateTime.UtcNow.AddYears(-1),
        GenreData.MainGenre.Id,
        AuthorData.MainAuthor.Id,
        PublisherData.MainPublisher.Id
    );
}