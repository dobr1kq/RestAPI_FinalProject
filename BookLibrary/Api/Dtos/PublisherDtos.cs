using Domain.Publishers;

namespace Api.Dtos;

public record PublisherDto(
    Guid Id,
    string PublisherName,
    string PublisherAddress)
{
    public static PublisherDto FromDomainModel(Publisher publisher)
        => new(
            Id: publisher.Id.Value,
            PublisherName: publisher.PublisherName,
            PublisherAddress: publisher.PublisherAddress);
}