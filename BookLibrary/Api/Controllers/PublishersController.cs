using Api.Dtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Publishers.Commands;
using Domain.Publishers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("publishers")]
[ApiController]
public class PublishersController : ControllerBase
{
    private readonly ISender sender;
    private readonly IPublisherQueries publisherQueries;

    public PublishersController(ISender sender, IPublisherQueries publisherQueries)
    {
        this.sender = sender;
        this.publisherQueries = publisherQueries;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PublisherDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await publisherQueries.GetAll(cancellationToken);
        return entities.Select(PublisherDto.FromDomainModel).ToList();
    }

    [HttpGet("{publisherId:guid}")]
    public async Task<ActionResult<PublisherDto>> Get([FromRoute] Guid publisherId, CancellationToken cancellationToken)
    {
        var entity = await publisherQueries.GetById(new PublisherId(publisherId), cancellationToken);

        return entity.Match<ActionResult<PublisherDto>>(
            p => PublisherDto.FromDomainModel(p),
            () => NotFound());
    }

    [HttpPost]
    public async Task<ActionResult<PublisherDto>> Create([FromBody] PublisherDto request, CancellationToken cancellationToken)
    {
        var input = new CreatePublisherCommand
        {
            PublisherName = request.PublisherName,
            PublisherAddress = request.PublisherAddress
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<PublisherDto>>(
            p => PublisherDto.FromDomainModel(p),
            e => e.ToObjectResult());
    }

    [HttpPut]
    public async Task<ActionResult<PublisherDto>> Update([FromBody] PublisherDto request, CancellationToken cancellationToken)
    {
        var input = new UpdatePublisherCommand
        {
            PublisherId = request.Id,
            PublisherName = request.PublisherName,
            PublisherAddress = request.PublisherAddress
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<PublisherDto>>(
            p => PublisherDto.FromDomainModel(p),
            e => e.ToObjectResult());
    }

    [HttpDelete("{publisherId:guid}")]
    public async Task<ActionResult<PublisherDto>> Delete([FromRoute] Guid publisherId, CancellationToken cancellationToken)
    {
        var input = new DeletePublisherCommand
        {
            PublisherId = publisherId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<PublisherDto>>(
            p => PublisherDto.FromDomainModel(p),
            e => e.ToObjectResult());
    }
}
