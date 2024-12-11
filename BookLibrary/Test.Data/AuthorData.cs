using Domain.Authors;

namespace Test.Data;

public static class AuthorData
{
    public static Author MainAuthor => Author.New(
        AuthorId.New(), 
        "John", 
        "Doe", 
        "USA", 
        new DateTime(1980, 1, 1)
    );
}