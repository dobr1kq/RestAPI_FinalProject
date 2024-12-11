using System.Net;
using System.Net.Http.Json;
using Api.Dtos;
using Domain.Publishers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Test.Common;
using Test.Data;
using Xunit;

namespace Api.Tests.Integration;

public class PublishersControllerTests : BaseIntegrationTest, IAsyncLifetime
{
    private readonly Publisher _mainPublisher;

    public PublishersControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
        _mainPublisher = PublisherData.MainPublisher;
    }

    [Fact]
    public async Task ShouldCreatePublisherSuccessfully()
    {
        var newPublisherName = "Test Publisher1";
        var newPublisherAddress = "123 Publisher St.";

        var newPublisherRequest = new PublisherDto(
            Id: Guid.NewGuid(),
            PublisherName: newPublisherName,
            PublisherAddress: newPublisherAddress
        );
        
        var createResponse = await Client.PostAsJsonAsync("publishers", newPublisherRequest);
        
        createResponse.IsSuccessStatusCode.Should().BeTrue();

        var createdPublisher = await createResponse.ToResponseModel<PublisherDto>();

        var publisherInDb = await Context.Publishers
            .FirstAsync(p => p.Id == new PublisherId(createdPublisher.Id));
        
        publisherInDb!.PublisherName.Should().Be(newPublisherName);
        publisherInDb.PublisherAddress.Should().Be(newPublisherAddress);
    }

    [Fact]
    public async Task ShouldUpdateExistingPublisher()
    {
        var updatedName = "Updated Publisher";
        var updatedAddress = "456 Updated St.";

        var updateRequest = new PublisherDto(
            Id: _mainPublisher.Id.Value,
            PublisherName: updatedName,
            PublisherAddress: updatedAddress
        );
        
        var updateResponse = await Client.PutAsJsonAsync("publishers", updateRequest);
        
        updateResponse.IsSuccessStatusCode.Should().BeTrue();

        var updatedPublisher = await updateResponse.ToResponseModel<PublisherDto>();

        var publisherInDb = await Context.Publishers
            .FirstAsync(p => p.Id == new PublisherId(updatedPublisher.Id));
        
        publisherInDb!.PublisherName.Should().Be(updatedName);
        publisherInDb.PublisherAddress.Should().Be(updatedAddress);
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenPublisherDoesNotExist()
    {
        var nonExistentPublisherName = "Nonexistent Publisher";
        var nonExistentPublisherId = Guid.NewGuid();
        
        var nonExistentPublisherRequest = new PublisherDto(
            Id: nonExistentPublisherId,
            PublisherName: nonExistentPublisherName,
            PublisherAddress: "Unknown Address"
        );
        
        var response = await Client.PutAsJsonAsync("publishers", nonExistentPublisherRequest);
        
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    public async Task InitializeAsync()
    {
        await Context.Publishers.AddAsync(_mainPublisher);

        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.Publishers.RemoveRange(Context.Publishers);

        await SaveChangesAsync();
    }
}

