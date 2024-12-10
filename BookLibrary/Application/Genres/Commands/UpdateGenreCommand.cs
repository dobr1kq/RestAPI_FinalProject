using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Genres.Exceptions;
using Domain.Genres;
using MediatR;

namespace Application.Genres.Commands;

public record UpdateGenreCommand : IRequest<Result<Genre, GenreException>>
{
    public required Guid GenreId { get; init; }
    public required string GenreName { get; init; }
}
public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, Result<Genre, GenreException>>
{
    private readonly IGenreRepository genreRepository;

    public UpdateGenreCommandHandler(IGenreRepository genreRepository)
    {
        this.genreRepository = genreRepository;
    }

    public async Task<Result<Genre, GenreException>> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var genreId = new GenreId(request.GenreId);

        var existingGenre = await genreRepository.GetById(genreId, cancellationToken);

        return await existingGenre.Match(
            async g => await UpdateEntity(g, request.GenreName, cancellationToken),
            () => Task.FromResult<Result<Genre, GenreException>>(new GenreNotFoundException(genreId))
        );
    }

    private async Task<Result<Genre, GenreException>> UpdateEntity(Genre genre, string genreName, CancellationToken cancellationToken)
    {
        try
        {
            genre.UpdateDetails(genreName);

            return await genreRepository.Update(genre, cancellationToken);
        }
        catch (Exception ex)
        {
            return new GenreUnknownException(genre.Id, ex);
        }
    }
}
