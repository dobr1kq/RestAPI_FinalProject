using System.Net;
using System.Net.Http.Json;
using Api.Dtos;
using Domain.Authors;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Test.Common;
using Test.Data;
using Xunit;

namespace Api.Tests.Integration;

public class AuthorsControllerTests : BaseIntegrationTest, IAsyncLifetime
{
    private readonly Author _mainAuthor;

    public AuthorsControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
        _mainAuthor = AuthorData.MainAuthor;
    }

    [Fact]
    public async Task ShouldCreateAuthorSuccessfully()
    {
        var newAuthorFirstName = "Jane";
        var newAuthorLastName = "Doe";
        var newAuthorCountry = "UK";
        var newAuthorDateOfBirth = new DateTime(1975, 5, 15, 0, 0, 0, DateTimeKind.Utc);

        var newAuthorRequest = new AuthorDto(
            Id: Guid.NewGuid(),
            FirstName: newAuthorFirstName,
            LastName: newAuthorLastName,
            Country: newAuthorCountry,
            DateOfBirth: newAuthorDateOfBirth
        );
    
        var createResponse = await Client.PostAsJsonAsync("authors", newAuthorRequest);
    
        createResponse.IsSuccessStatusCode.Should().BeTrue();

        var createdAuthor = await createResponse.ToResponseModel<AuthorDto>();

        var authorInDb = await Context.Authors
            .FirstAsync(a => a.Id == new AuthorId(createdAuthor.Id));
    
        authorInDb!.FirstName.Should().Be(newAuthorFirstName);
        authorInDb.LastName.Should().Be(newAuthorLastName);
        authorInDb.Country.Should().Be(newAuthorCountry);
        
        authorInDb.DateOfBirth.ToUniversalTime().Should().Be(newAuthorDateOfBirth);
    }
    
    [Fact]
    public async Task ShouldUpdateExistingAuthor()
    {
        var updatedFirstName = "Updated FirstName";
        var updatedLastName = "Updated LastName";
        var updatedCountry = "Canada";
        var updatedDateOfBirth = new DateTime(1985, 12, 25, 0, 0, 0, DateTimeKind.Utc);

        var updateRequest = new AuthorDto(
            Id: _mainAuthor.Id.Value,
            FirstName: updatedFirstName,
            LastName: updatedLastName,
            Country: updatedCountry,
            DateOfBirth: updatedDateOfBirth
        );
    
        var updateResponse = await Client.PutAsJsonAsync("authors", updateRequest);
    
        updateResponse.IsSuccessStatusCode.Should().BeTrue();

        var updatedAuthor = await updateResponse.ToResponseModel<AuthorDto>();

        var authorInDb = await Context.Authors
            .FirstAsync(a => a.Id == new AuthorId(updatedAuthor.Id));
    
        authorInDb!.FirstName.Should().Be(updatedFirstName);
        authorInDb.LastName.Should().Be(updatedLastName);
        authorInDb.Country.Should().Be(updatedCountry);
        
        authorInDb.DateOfBirth.ToUniversalTime().Should().Be(updatedDateOfBirth);
    }


    [Fact]
    public async Task ShouldReturnNotFoundWhenAuthorDoesNotExist()
    {
        var nonExistentFirstName = "Nonexistent Author";
        var nonExistentAuthorId = Guid.NewGuid();
        var updatedDateOfBirth = new DateTime(2000, 1, 1);
        
        var nonExistentAuthorRequest = new AuthorDto(
            Id: nonExistentAuthorId,
            FirstName: nonExistentFirstName,
            LastName: "Unknown",
            Country: "Nowhere",
            DateOfBirth: updatedDateOfBirth
        );
        
        var response = await Client.PutAsJsonAsync("authors", nonExistentAuthorRequest);
        
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    public async Task InitializeAsync()
    {
        await Context.Authors.AddAsync(_mainAuthor);

        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.Authors.RemoveRange(Context.Authors);

        await SaveChangesAsync();
    }
}