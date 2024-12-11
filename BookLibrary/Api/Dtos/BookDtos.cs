using Application.Books.Commands;
using Domain.Books;

namespace Api.Dtos;

public record BookDto(
    Guid Id,
    string Name,
    DateTime Date,
    Guid GenreId,
    Guid AuthorId,
    Guid PublisherId)
{
    public static BookDto FromDomainModel(Book book)
        => new(
            Id: book.Id.Value,
            Name: book.Name,
            Date: book.Date,
            GenreId: book.GenreId.Value,
            AuthorId: book.AuthorId.Value,
            PublisherId: book.PublisherId.Value
            );
}