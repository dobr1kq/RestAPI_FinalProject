using System.Net;
using System.Net.Http.Json;
using Api.Dtos;
using Domain.Readers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Test.Common;
using Test.Data;
using Xunit;

namespace Api.Tests.Integration;

public class ReadersControllerTests : BaseIntegrationTest, IAsyncLifetime
{
    private readonly Reader _mainReader;

    public ReadersControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
        _mainReader = ReaderData.MainReader;
    }

    [Fact]
    public async Task ShouldCreateReaderSuccessfully()
    {
        var newFirstName = "Max";
        var newLastName = "dfdfd";
        var newTelephone = "+38987654111";
        
        var newReaderRequest = new ReaderDto(
            Id: Guid.NewGuid(),
            FirstName: newFirstName,
            LastName: newLastName,
            TelephoneNumber: newTelephone
        );
        
        var createResponse = await Client.PostAsJsonAsync("readers", newReaderRequest);

        createResponse.IsSuccessStatusCode.Should().BeTrue();

        var createdReader = await createResponse.ToResponseModel<ReaderDto>();

        var readerInDb = await Context.Readers
            .FirstAsync(r => r.Id == new ReaderId(createdReader.Id));
        
        readerInDb!.FirstName.Should().Be(newFirstName);
        readerInDb.LastName.Should().Be(newLastName);
        readerInDb.TelephoneNumber.Should().Be(newTelephone);
    }

    [Fact]
    public async Task ShouldUpdateExistingReader()
    {
        var updatedFirstName = "Updated Alice";
        var updatedLastName = "Updated Johnson";
        var updatedTelephone = "+38987654321";

        var updateRequest = new ReaderDto(
            Id: _mainReader.Id.Value,
            FirstName: updatedFirstName,
            LastName: updatedLastName,
            TelephoneNumber: updatedTelephone
        );
        
        var updateResponse = await Client.PutAsJsonAsync("readers", updateRequest);
        
        updateResponse.IsSuccessStatusCode.Should().BeTrue();

        var updatedReader = await updateResponse.ToResponseModel<ReaderDto>();

        var readerInDb = await Context.Readers
            .FirstAsync(r => r.Id == new ReaderId(updatedReader.Id));
        
        readerInDb!.FirstName.Should().Be(updatedFirstName);
        readerInDb.LastName.Should().Be(updatedLastName);
        readerInDb.TelephoneNumber.Should().Be(updatedTelephone);
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenReaderDoesNotExist()
    {
        var nonExistentReaderId = Guid.NewGuid();
        
        var nonExistentReaderRequest = new ReaderDto(
            Id: nonExistentReaderId,
            FirstName: "Nonexistent",
            LastName: "Reader",
            TelephoneNumber: "+38000000000"
        );
        
        var response = await Client.PutAsJsonAsync("readers", nonExistentReaderRequest);
        
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    public async Task InitializeAsync()
    {
        await Context.Readers.AddAsync(_mainReader);

        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.Readers.RemoveRange(Context.Readers);

        await SaveChangesAsync();
    }
}
