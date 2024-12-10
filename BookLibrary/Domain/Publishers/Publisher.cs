namespace Domain.Publishers;

public class Publisher
{
    public PublisherId Id { get; }
    public string PublisherName { get; private set; }
    public string PublisherAddress { get; private set; }
    
    private Publisher(PublisherId id, string publisherName, string publisherAddress)
    {
        Id = id;
        PublisherName = publisherName;
        PublisherAddress = publisherAddress;
    }
    
    public static Publisher Create(PublisherId id, string publisherName, string publisherAddress)
    {
        return new Publisher(id, publisherName, publisherAddress);
    }
    
    public void UpdateDetails(string publisherName, string publisherAddress)
    {
        PublisherName = publisherName;
        PublisherAddress = publisherAddress;
    }
}