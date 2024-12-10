using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Publishers.Exceptions;
using Domain.Publishers;
using MediatR;

namespace Application.Publishers.Commands;

public record UpdatePublisherCommand : IRequest<Result<Publisher, PublisherException>>
{
    public required Guid PublisherId { get; init; }
    public required string PublisherName { get; init; }
    public required string PublisherAddress { get; init; }
}
public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand, Result<Publisher, PublisherException>>
{
    private readonly IPublisherRepository publisherRepository;

    public UpdatePublisherCommandHandler(IPublisherRepository publisherRepository)
    {
        this.publisherRepository = publisherRepository;
    }

    public async Task<Result<Publisher, PublisherException>> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisherId = new PublisherId(request.PublisherId);

        var existingPublisher = await publisherRepository.GetById(publisherId, cancellationToken);

        return await existingPublisher.Match<Task<Result<Publisher, PublisherException>>>(
            async publisher => await UpdateEntity(publisher, request.PublisherName, request.PublisherAddress, cancellationToken),
            () => Task.FromResult<Result<Publisher, PublisherException>>(new PublisherNotFoundException(publisherId))
        );
    }

    private async Task<Result<Publisher, PublisherException>> UpdateEntity(
        Publisher publisher,
        string publisherName,
        string publisherAddress,
        CancellationToken cancellationToken)
    {
        try
        {
            publisher.UpdateDetails(publisherName, publisherAddress);

            return await publisherRepository.Update(publisher, cancellationToken);
        }
        catch (Exception ex)
        {
            return new PublisherUnknownException(publisher.Id, ex);
        }
    }
}
