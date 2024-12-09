namespace Domain.Publishers;

public record PublisherId(Guid Value)
{
    public static PublisherId Empty() => new(Guid.Empty);
    public static PublisherId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}