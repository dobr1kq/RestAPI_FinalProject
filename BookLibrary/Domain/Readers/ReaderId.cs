namespace Domain.Readers;

public record ReaderId(Guid Value)
{
    public static ReaderId Empty() => new(Guid.Empty);
    public static ReaderId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}