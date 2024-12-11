using System.Net;
using System.Net.Http.Json;
using Api.Dtos;
using Domain.Genres;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Test.Common;
using Test.Data;
using Xunit;

namespace Api.Tests.Integration;

public class GenresControllerTests : BaseIntegrationTest, IAsyncLifetime
{
    private readonly Genre _mainGenre;

    public GenresControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
        _mainGenre = GenreData.MainGenre;
    }

    [Fact]
    public async Task ShouldCreateGenreSuccessfully()
    {
        var newGenreName = "Fantasy";

        var newGenreRequest = new GenreDto(
            Id: Guid.NewGuid(),
            GenreName: newGenreName
        );
        
        var createResponse = await Client.PostAsJsonAsync("genres", newGenreRequest);
        
        createResponse.IsSuccessStatusCode.Should().BeTrue();

        var createdGenre = await createResponse.ToResponseModel<GenreDto>();

        var genreInDb = await Context.Genres
            .FirstAsync(g => g.Id == new GenreId(createdGenre.Id));
        
        genreInDb!.GenreName.Should().Be(newGenreName);
    }

    [Fact]
    public async Task ShouldUpdateExistingGenre()
    {
        var updatedName = "Updated Fantasy";

        var updateRequest = new GenreDto(
            Id: _mainGenre.Id.Value,
            GenreName: updatedName
        );
        
        var updateResponse = await Client.PutAsJsonAsync("genres", updateRequest);
        
        updateResponse.IsSuccessStatusCode.Should().BeTrue();

        var updatedGenre = await updateResponse.ToResponseModel<GenreDto>();

        var genreInDb = await Context.Genres
            .FirstAsync(g => g.Id == new GenreId(updatedGenre.Id));
        
        genreInDb!.GenreName.Should().Be(updatedName);
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenGenreDoesNotExist()
    {
        var nonExistentGenreName = "Nonexistent Genre";
        var nonExistentGenreId = Guid.NewGuid();
        
        var nonExistentGenreRequest = new GenreDto(
            Id: nonExistentGenreId,
            GenreName: nonExistentGenreName
        );
        
        var response = await Client.PutAsJsonAsync("genres", nonExistentGenreRequest);
        
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    public async Task InitializeAsync()
    {
        await Context.Genres.AddAsync(_mainGenre);

        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.Genres.RemoveRange(Context.Genres);

        await SaveChangesAsync();
    }
}
