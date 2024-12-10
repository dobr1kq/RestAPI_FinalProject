using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Publishers.Exceptions;
using Domain.Publishers;
using MediatR;

namespace Application.Publishers.Commands;

public record CreatePublisherCommand : IRequest<Result<Publisher, PublisherException>>
{
    public required string PublisherName { get; init; }
    public required string PublisherAddress { get; init; }
}
public class CreatePublisherCommandHandler : IRequestHandler<CreatePublisherCommand, Result<Publisher, PublisherException>>
{
    private readonly IPublisherRepository publisherRepository;

    public CreatePublisherCommandHandler(IPublisherRepository publisherRepository)
    {
        this.publisherRepository = publisherRepository;
    }

    public async Task<Result<Publisher, PublisherException>> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
    {
        var existingPublisher = await publisherRepository.GetByName(request.PublisherName, cancellationToken);

        return await existingPublisher.Match<Task<Result<Publisher, PublisherException>>>(
            publisher => Task.FromResult<Result<Publisher, PublisherException>>(new PublisherAlreadyExistsException(publisher.Id)),
            async () => await CreateEntity(request.PublisherName, request.PublisherAddress, cancellationToken)
        );
    }

    private async Task<Result<Publisher, PublisherException>> CreateEntity(string name, string address, CancellationToken cancellationToken)
    {
        try
        {
            var publisherId = PublisherId.New();
            var publisher = Publisher.Create(publisherId, name, address);
            return await publisherRepository.Add(publisher, cancellationToken);
        }
        catch (Exception ex)
        {
            return new PublisherUnknownException(PublisherId.Empty(), ex);
        }
    }
}
