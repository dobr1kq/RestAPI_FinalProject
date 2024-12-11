using Api.Dtos;
using Api.Modules.Errors;
using Application.Books.Commands;
using Application.Common.Interfaces.Queries;
using Domain.Books;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("books")]
[ApiController]
public class BooksController(ISender sender, IBookQueries bookQueries) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BookDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await bookQueries.GetAll(cancellationToken);
        return entities.Select(BookDto.FromDomainModel).ToList();
    }

    [HttpGet("{bookId:guid}")]
    public async Task<ActionResult<BookDto>> Get([FromRoute] Guid bookId, CancellationToken cancellationToken)
    {
        var entity = await bookQueries.GetById(new BookId(bookId), cancellationToken);

        return entity.Match<ActionResult<BookDto>>(
            b => BookDto.FromDomainModel(b),
            () => NotFound());
    }

    [HttpPost]
    public async Task<ActionResult<BookDto>> Create([FromBody] BookDto request, CancellationToken cancellationToken)
    {
        var input = new CreateBookCommand
        {
            Name = request.Name,
            Date = request.Date,
            AuthorId = request.AuthorId,
            GenreId = request.GenreId,
            PublisherId = request.PublisherId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<BookDto>>(
            b => BookDto.FromDomainModel(b),
            e => e.ToObjectResult());
    }

    [HttpPut]
    public async Task<ActionResult<BookDto>> Update([FromBody] BookDto request, CancellationToken cancellationToken)
    {
        var input = new UpdateBookCommand
        {
            BookId = request.Id,
            Name = request.Name,
            Date = request.Date,
            AuthorId = request.AuthorId,
            GenreId = request.GenreId,
            PublisherId = request.PublisherId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<BookDto>>(
            b => BookDto.FromDomainModel(b),
            e => e.ToObjectResult());
    }

    [HttpDelete("{bookId:guid}")]
    public async Task<ActionResult<BookDto>> Delete([FromRoute] Guid bookId, CancellationToken cancellationToken)
    {
        var input = new DeleteBookCommand
        {
            BookId = bookId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<BookDto>>(
            b => BookDto.FromDomainModel(b),
            e => e.ToObjectResult());
    }
}
