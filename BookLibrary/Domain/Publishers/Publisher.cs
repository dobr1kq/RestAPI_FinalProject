namespace Domain.Publishers;

public class Publisher
{
    public PublisherId Id { get; }
    public string PublisherName { get; private set; }
    public string PublisherAddress { get; private set; }
}