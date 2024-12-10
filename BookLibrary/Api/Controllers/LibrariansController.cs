using Api.Dtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Librarians.Commands;
using Domain.Librarians;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("librarians")]
[ApiController]
public class LibrariansController : ControllerBase
{
    private readonly ISender sender;
    private readonly ILibrarianQueries librarianQueries;

    public LibrariansController(ISender sender, ILibrarianQueries librarianQueries)
    {
        this.sender = sender;
        this.librarianQueries = librarianQueries;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LibrarianDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await librarianQueries.GetAll(cancellationToken);
        return entities.Select(LibrarianDto.FromDomainModel).ToList();
    }

    [HttpGet("{librarianId:guid}")]
    public async Task<ActionResult<LibrarianDto>> Get([FromRoute] Guid librarianId, CancellationToken cancellationToken)
    {
        var entity = await librarianQueries.GetById(new LibrarianId(librarianId), cancellationToken);

        return entity.Match<ActionResult<LibrarianDto>>(
            l => LibrarianDto.FromDomainModel(l),
            () => NotFound());
    }

    [HttpPost]
    public async Task<ActionResult<LibrarianDto>> Create([FromBody] LibrarianDto request, CancellationToken cancellationToken)
    {
        var input = new CreateLibrarianCommand
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            TelephoneNumber = request.TelephoneNumber
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<LibrarianDto>>(
            l => LibrarianDto.FromDomainModel(l),
            e => e.ToObjectResult());
    }

    [HttpPut]
    public async Task<ActionResult<LibrarianDto>> Update([FromBody] LibrarianDto request, CancellationToken cancellationToken)
    {
        var input = new UpdateLibrarianCommand
        {
            LibrarianId = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            TelephoneNumber = request.TelephoneNumber
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<LibrarianDto>>(
            l => LibrarianDto.FromDomainModel(l),
            e => e.ToObjectResult());
    }

    [HttpDelete("{librarianId:guid}")]
    public async Task<ActionResult<LibrarianDto>> Delete([FromRoute] Guid librarianId, CancellationToken cancellationToken)
    {
        var input = new DeleteLibrarianCommand
        {
            LibrarianId = librarianId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<LibrarianDto>>(
            l => LibrarianDto.FromDomainModel(l),
            e => e.ToObjectResult());
    }
}
