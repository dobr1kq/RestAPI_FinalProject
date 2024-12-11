using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Genres.Exceptions;
using Domain.Genres;
using MediatR;

namespace Application.Genres.Commands;

public record CreateGenreCommand : IRequest<Result<Genre, GenreException>>
{
    public required string GenreName { get; init; }
}

public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, Result<Genre, GenreException>>
{
    private readonly IGenreRepository genreRepository;

    public CreateGenreCommandHandler(IGenreRepository genreRepository)
    {
        this.genreRepository = genreRepository;
    }

    public async Task<Result<Genre, GenreException>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var existingGenre = await genreRepository.GetByName(request.GenreName, cancellationToken);

        return await existingGenre.Match<Task<Result<Genre, GenreException>>>( 
            g => Task.FromResult<Result<Genre, GenreException>>(new GenreAlreadyExistsException(g.Id)),
            async () => await CreateEntity(request.GenreName, cancellationToken)
        );
    }

    private async Task<Result<Genre, GenreException>> CreateEntity(string genreName, CancellationToken cancellationToken)
    {
        try
        {
            var genreId = GenreId.New();
            var genre = Genre.New(genreId, genreName);    
            return await genreRepository.Add(genre, cancellationToken);
        }
        catch (Exception ex)
        {
            return new GenreUnknownException(GenreId.Empty(), ex);
        }
    }
}

