namespace Domain.Authors;

public record AuthorId(Guid Value)
{
    public static AuthorId Empty() => new(Guid.Empty);
    public static AuthorId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}