using Domain.Books;

namespace Api.Dtos;

public record BookDto(
    Guid Id,
    string Name,
    DateTime Date)
{
    public static BookDto FromDomainModel(Book book)
        => new(
            Id: book.Id.Value,
            Name: book.Name,
            Date: book.Date);
}