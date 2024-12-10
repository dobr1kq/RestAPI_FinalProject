using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Publishers.Exceptions;
using Domain.Publishers;
using MediatR;

namespace Application.Publishers.Commands;

public record DeletePublisherCommand : IRequest<Result<Publisher, PublisherException>>
{
    public required Guid PublisherId { get; init; }
}

public class
    DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand, Result<Publisher, PublisherException>>
{
    private readonly IPublisherRepository publisherRepository;

    public DeletePublisherCommandHandler(IPublisherRepository publisherRepository)
    {
        this.publisherRepository = publisherRepository;
    }

    public async Task<Result<Publisher, PublisherException>> Handle(DeletePublisherCommand request,
        CancellationToken cancellationToken)
    {
        var publisherId = new PublisherId(request.PublisherId);

        var existingPublisher = await publisherRepository.GetById(publisherId, cancellationToken);

        return await existingPublisher.Match<Task<Result<Publisher, PublisherException>>>(
            async publisher => await DeleteEntity(publisher, cancellationToken),
            () => Task.FromResult<Result<Publisher, PublisherException>>(new PublisherNotFoundException(publisherId))
        );
    }

    private async Task<Result<Publisher, PublisherException>> DeleteEntity(Publisher publisher,
        CancellationToken cancellationToken)
    {
        try
        {
            return await publisherRepository.Delete(publisher, cancellationToken);
        }
        catch (Exception ex)
        {
            return new PublisherUnknownException(publisher.Id, ex);
        }
    }
}