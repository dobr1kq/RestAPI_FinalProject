using Domain.Genres;

namespace Test.Data;

public static class GenreData
{
    public static Genre MainGenre => Genre.New(
        GenreId.New(),
        "Fiction"
    );
}