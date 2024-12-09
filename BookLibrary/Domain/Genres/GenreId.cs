namespace Domain.Genres;

public record GenreId(Guid Value)
{
    public static GenreId Empty() => new(Guid.Empty);
    public static GenreId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}