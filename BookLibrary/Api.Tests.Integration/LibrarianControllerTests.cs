using System.Net;
using System.Net.Http.Json;
using Api.Dtos;
using Domain.Librarians;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Test.Common;
using Test.Data;
using Xunit;

namespace Api.Tests.Integration;

public class LibrarianControllerTests : BaseIntegrationTest, IAsyncLifetime
{
    private readonly Librarian _mainLibrarian;

    public LibrarianControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
        _mainLibrarian = LibrarianData.MainLibrarian;
    }

    [Fact]
    public async Task ShouldCreateLibrarianSuccessfully()
    {
        var newLibrarianFirstName = "John";
        var newLibrarianLastName = "Doe";
        var newLibrarianPhone = "+380987112896";

        var newLibrarianRequest = new LibrarianDto(
            Id: Guid.NewGuid(),
            FirstName: newLibrarianFirstName,
            LastName: newLibrarianLastName,
            TelephoneNumber: newLibrarianPhone
        );
        
        var createResponse = await Client.PostAsJsonAsync("librarians", newLibrarianRequest);
        
        createResponse.IsSuccessStatusCode.Should().BeTrue();

        var createdLibrarian = await createResponse.ToResponseModel<LibrarianDto>();

        var librarianInDb = await Context.Librarians
            .FirstAsync(l => l.Id == new LibrarianId(createdLibrarian.Id));
        
        librarianInDb!.FirstName.Should().Be(newLibrarianFirstName);
        librarianInDb.LastName.Should().Be(newLibrarianLastName);
        librarianInDb.TelephoneNumber.Should().Be(newLibrarianPhone);
    }
    [Fact]
    public async Task ShouldUpdateExistingLibrarian()
    {
        var updatedFirstName = "Updated FirstName";
        var updatedLastName = "Updated LastName";
        var updatedPhone = "+380987112891";

        var updateRequest = new LibrarianDto(
            Id: _mainLibrarian.Id.Value,
            FirstName: updatedFirstName,
            LastName: updatedLastName,
            TelephoneNumber: updatedPhone
        );
        
        var updateResponse = await Client.PutAsJsonAsync("librarians", updateRequest);
        
        updateResponse.IsSuccessStatusCode.Should().BeTrue();

        var updatedLibrarian = await updateResponse.ToResponseModel<LibrarianDto>();

        var librarianInDb = await Context.Librarians
            .FirstAsync(l => l.Id == new LibrarianId(updatedLibrarian.Id));
        
        librarianInDb!.FirstName.Should().Be(updatedFirstName);
        librarianInDb.LastName.Should().Be(updatedLastName);
        librarianInDb.TelephoneNumber.Should().Be(updatedPhone);
    }
    [Fact]
    public async Task ShouldReturnNotFoundWhenLibrarianDoesNotExist()
    {
        var nonExistentFirstName = "Nonexistent";
        var nonExistentLibrarianId = Guid.NewGuid();
        var updatedPhone = "+00000000000";
    
        var nonExistentLibrarianRequest = new LibrarianDto(
            Id: nonExistentLibrarianId,
            FirstName: nonExistentFirstName,
            LastName: "Unknown",
            TelephoneNumber: updatedPhone
        );

        var response = await Client.PutAsJsonAsync("librarian", nonExistentLibrarianRequest);
        
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    public async Task InitializeAsync()
    {
        await Context.Librarians.AddAsync(_mainLibrarian);

        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.Librarians.RemoveRange(Context.Librarians);

        await SaveChangesAsync();
    }
}