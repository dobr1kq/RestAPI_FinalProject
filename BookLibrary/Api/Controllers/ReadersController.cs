using Api.Dtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Readers.Commands;
using Domain.Readers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("readers")]
[ApiController]
public class ReadersController : ControllerBase
{
    private readonly ISender sender;
    private readonly IReaderQueries readerQueries;

    public ReadersController(ISender sender, IReaderQueries readerQueries)
    {
        this.sender = sender;
        this.readerQueries = readerQueries;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ReaderDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await readerQueries.GetAll(cancellationToken);
        return entities.Select(ReaderDto.FromDomainModel).ToList();
    }

    [HttpGet("{readerId:guid}")]
    public async Task<ActionResult<ReaderDto>> Get([FromRoute] Guid readerId, CancellationToken cancellationToken)
    {
        var entity = await readerQueries.GetById(new ReaderId(readerId), cancellationToken);

        return entity.Match<ActionResult<ReaderDto>>(
            r => ReaderDto.FromDomainModel(r),
            () => NotFound());
    }

    [HttpPost]
    public async Task<ActionResult<ReaderDto>> Create([FromBody] ReaderDto request, CancellationToken cancellationToken)
    {
        var input = new CreateReaderCommand
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            TelephoneNumber = request.TelephoneNumber
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ReaderDto>>(
            r => ReaderDto.FromDomainModel(r),
            e => e.ToObjectResult());
    }

    [HttpPut]
    public async Task<ActionResult<ReaderDto>> Update([FromBody] ReaderDto request, CancellationToken cancellationToken)
    {
        var input = new UpdateReaderCommand
        {
            ReaderId = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            TelephoneNumber = request.TelephoneNumber
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ReaderDto>>(
            r => ReaderDto.FromDomainModel(r),
            e => e.ToObjectResult());
    }

    [HttpDelete("{readerId:guid}")]
    public async Task<ActionResult<ReaderDto>> Delete([FromRoute] Guid readerId, CancellationToken cancellationToken)
    {
        var input = new DeleteReaderCommand
        {
            ReaderId = readerId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ReaderDto>>(
            r => ReaderDto.FromDomainModel(r),
            e => e.ToObjectResult());
    }
}
