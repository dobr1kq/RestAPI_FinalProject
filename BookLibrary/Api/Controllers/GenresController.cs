using Api.Dtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Genres.Commands;
using Domain.Genres;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("genres")]
[ApiController]
public class GenresController(ISender sender, IGenreQueries genreQueries) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<GenreDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await genreQueries.GetAll(cancellationToken);
        return entities.Select(GenreDto.FromDomainModel).ToList();
    }

    [HttpGet("{genreId:guid}")]
    public async Task<ActionResult<GenreDto>> Get([FromRoute] Guid genreId, CancellationToken cancellationToken)
    {
        var entity = await genreQueries.GetById(new GenreId(genreId), cancellationToken);

        return entity.Match<ActionResult<GenreDto>>(
            g => GenreDto.FromDomainModel(g),
            () => NotFound());
    }

    [HttpPost]
    public async Task<ActionResult<GenreDto>> Create([FromBody] GenreDto request, CancellationToken cancellationToken)
    {
        var input = new CreateGenreCommand
        {
            GenreName = request.GenreName
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<GenreDto>>(
            g => GenreDto.FromDomainModel(g),
            e => e.ToObjectResult());
    }

    [HttpPut]
    public async Task<ActionResult<GenreDto>> Update([FromBody] GenreDto request, CancellationToken cancellationToken)
    {
        var input = new UpdateGenreCommand
        {
            GenreId = request.Id,
            GenreName = request.GenreName
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<GenreDto>>(
            g => GenreDto.FromDomainModel(g),
            e => e.ToObjectResult());
    }

    [HttpDelete("{genreId:guid}")]
    public async Task<ActionResult<GenreDto>> Delete([FromRoute] Guid genreId, CancellationToken cancellationToken)
    {
        var input = new DeleteGenreCommand
        {
            GenreId = genreId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<GenreDto>>(
            g => GenreDto.FromDomainModel(g),
            e => e.ToObjectResult());
    }
}