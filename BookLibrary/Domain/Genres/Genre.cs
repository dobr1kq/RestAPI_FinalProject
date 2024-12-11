namespace Domain.Genres;

public class Genre
{
    public GenreId Id { get; }
    public string GenreName { get; set; }
    
    private Genre(GenreId id, string genreName)
    {
        Id = id;
        GenreName = genreName;
    }
    
    public static Genre New(GenreId id, string genreName)
    {
        return new Genre(id, genreName);
    }
    
    public void UpdateDetails(string genreName)
    {
        GenreName = genreName;
    }
}
