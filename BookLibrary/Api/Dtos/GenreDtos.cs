using Domain.Genres;

namespace Api.Dtos;

public record GenreDto(
    Guid Id,
    string GenreName)
{
    public static GenreDto FromDomainModel(Genre genre)
        => new(
            Id: genre.Id.Value,
            GenreName: genre.GenreName);
}