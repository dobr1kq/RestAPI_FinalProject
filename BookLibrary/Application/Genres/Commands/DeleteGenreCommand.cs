using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Genres.Exceptions;
using Domain.Genres;
using MediatR;

namespace Application.Genres.Commands;

public record DeleteGenreCommand : IRequest<Result<Genre, GenreException>>
{
    public required Guid GenreId { get; init; }
}
public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, Result<Genre, GenreException>>
{
    private readonly IGenreRepository genreRepository;

    public DeleteGenreCommandHandler(IGenreRepository genreRepository)
    {
        this.genreRepository = genreRepository;
    }

    public async Task<Result<Genre, GenreException>> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        var genreId = new GenreId(request.GenreId);

        var existingGenre = await genreRepository.GetById(genreId, cancellationToken);

        return await existingGenre.Match(
            async g => await DeleteEntity(g, cancellationToken),
            () => Task.FromResult<Result<Genre, GenreException>>(new GenreNotFoundException(genreId))
        );
    }

    private async Task<Result<Genre, GenreException>> DeleteEntity(Genre genre, CancellationToken cancellationToken)
    {
        try
        {
            return await genreRepository.Delete(genre, cancellationToken);
        }
        catch (Exception ex)
        {
            return new GenreUnknownException(genre.Id, ex);
        }
    }
}
