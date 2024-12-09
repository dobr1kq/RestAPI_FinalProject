using Domain.Authors;

namespace Api.Dtos;

public record AuthorDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Country,
    DateTime DateOfBirth)
{
    public static AuthorDto FromDomainModel(Author author)
        => new(
            Id: author.Id.Value,
            FirstName: author.FirstName,
            LastName: author.LastName,
            Country: author.Country,
            DateOfBirth: author.DateOfBirth);
}