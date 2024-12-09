namespace Domain.Librarians;

public record LibrarianId(Guid Value)
{
    public static LibrarianId Empty() => new(Guid.Empty);
    public static LibrarianId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}