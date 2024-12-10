using Api.Dtos;
using Api.Modules.Errors;
using Application.Authors.Commands;
using Application.Common.Interfaces.Queries;
using Domain.Authors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("authors")]
[ApiController]
public class AuthorsController(ISender sender, IAuthorQueries authorQueries) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AuthorDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await authorQueries.GetAll(cancellationToken);
        return entities.Select(AuthorDto.FromDomainModel).ToList();
    }

    [HttpGet("{authorId:guid}")]
    public async Task<ActionResult<AuthorDto>> Get([FromRoute] Guid authorId, CancellationToken cancellationToken)
    {
        var entity = await authorQueries.GetById(new AuthorId(authorId), cancellationToken);

        return entity.Match<ActionResult<AuthorDto>>(
            a => AuthorDto.FromDomainModel(a),
            () => NotFound());
    }

    [HttpPost]
    public async Task<ActionResult<AuthorDto>> Create([FromBody] AuthorDto request, CancellationToken cancellationToken)
    {
        var input = new CreateAuthorCommand
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Country = request.Country,
            DateOfBirth = request.DateOfBirth
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<AuthorDto>>(
            a => AuthorDto.FromDomainModel(a),
            e => e.ToObjectResult());
    }

    [HttpPut]
    public async Task<ActionResult<AuthorDto>> Update([FromBody] AuthorDto request, CancellationToken cancellationToken)
    {
        var input = new UpdateAuthorCommand
        {
            AuthorId = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Country = request.Country,
            DateOfBirth = request.DateOfBirth
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<AuthorDto>>(
            a => AuthorDto.FromDomainModel(a),
            e => e.ToObjectResult());
    }

    [HttpDelete("{authorId:guid}")]
    public async Task<ActionResult<AuthorDto>> Delete([FromRoute] Guid authorId, CancellationToken cancellationToken)
    {
        var input = new DeleteAuthorCommand
        {
            AuthorId = authorId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<AuthorDto>>(
            a => AuthorDto.FromDomainModel(a),
            e => e.ToObjectResult());
    }
}