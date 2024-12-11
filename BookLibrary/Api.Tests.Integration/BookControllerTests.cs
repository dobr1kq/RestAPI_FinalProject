using System.Net;
using System.Net.Http.Json;
using Api.Dtos;
using Domain.Books;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Test.Common;
using Test.Data;
using Xunit;

namespace Api.Tests.Integration;

public class BooksControllerTests : BaseIntegrationTest, IAsyncLifetime
{
    private readonly Book _mainBook;

    public BooksControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
        _mainBook = BookData.MainBook;
    }

    [Fact]
    public async Task ShouldCreateBookSuccessfully()
    {
        var newBookTitle = "New Test Book";
        var newBookPublicationDate = new DateTime(2020, 5, 1, 0, 0, 0, DateTimeKind.Utc);
        var newBookGenreId = GenreData.MainGenre.Id.Value;
        var newBookAuthorId = AuthorData.MainAuthor.Id.Value;
        var newBookPublisherId = PublisherData.MainPublisher.Id.Value;

        var newBookRequest = new BookDto(
            Id: Guid.NewGuid(),
            Name: newBookTitle,
            Date: newBookPublicationDate,
            GenreId: newBookGenreId,
            AuthorId: newBookAuthorId,
            PublisherId: newBookPublisherId
        );
    
        var createResponse = await Client.PostAsJsonAsync("books", newBookRequest);
    
        createResponse.IsSuccessStatusCode.Should().BeTrue();

        var createdBook = await createResponse.ToResponseModel<BookDto>();

        var bookInDb = await Context.Books
            .FirstAsync(b => b.Id == new BookId(createdBook.Id));
        
        bookInDb!.Name.Should().Be(newBookTitle);
        bookInDb.Date.Date.Should().Be(newBookPublicationDate.Date);
        bookInDb.GenreId.Value.Should().Be(newBookGenreId);
        bookInDb.AuthorId.Value.Should().Be(newBookAuthorId);
        bookInDb.PublisherId.Value.Should().Be(newBookPublisherId);
        
        bookInDb.Date.ToUniversalTime().Should().Be(newBookPublicationDate);
    }

    [Fact]
    public async Task ShouldUpdateExistingBook()
    {
        var updatedTitle = "Updated Book Title";
        var updatedPublicationDate = new DateTime(2021, 6, 15, 0, 0, 0, DateTimeKind.Utc);
        var updatedGenreId = GenreData.MainGenre.Id.Value;
        var updatedAuthorId = AuthorData.MainAuthor.Id.Value;
        var updatedPublisherId = PublisherData.MainPublisher.Id.Value;

        var updateRequest = new BookDto(
            Id: _mainBook.Id.Value,
            Name: updatedTitle,
            Date: updatedPublicationDate,
            GenreId: updatedGenreId,
            AuthorId: updatedAuthorId,
            PublisherId: updatedPublisherId
        );
    
        var updateResponse = await Client.PutAsJsonAsync("books", updateRequest);
    
        updateResponse.IsSuccessStatusCode.Should().BeTrue();

        var updatedBook = await updateResponse.ToResponseModel<BookDto>();

        var bookInDb = await Context.Books
            .FirstAsync(b => b.Id == new BookId(updatedBook.Id));
        
        bookInDb!.Name.Should().Be(updatedTitle);
        bookInDb.Date.Date.Should().Be(updatedPublicationDate.Date);
        bookInDb.GenreId.Value.Should().Be(updatedGenreId);
        bookInDb.AuthorId.Value.Should().Be(updatedAuthorId);
        bookInDb.PublisherId.Value.Should().Be(updatedPublisherId);
        
        bookInDb.Date.ToUniversalTime().Should().Be(updatedPublicationDate);
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenBookDoesNotExist()
    {
        var nonExistentBookId = Guid.NewGuid();
        
        var nonExistentBookRequest = new BookDto(
            Id: nonExistentBookId,
            Name: "Nonexistent Book",
            Date: new DateTime(2022, 7, 1),
            GenreId: GenreData.MainGenre.Id.Value,
            AuthorId: AuthorData.MainAuthor.Id.Value,
            PublisherId: PublisherData.MainPublisher.Id.Value
        );
        
        var response = await Client.PutAsJsonAsync("books", nonExistentBookRequest);
        
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        
    }

    public async Task InitializeAsync()
    {
        await Context.Books.AddAsync(_mainBook);

        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.Books.RemoveRange(Context.Books);

        await SaveChangesAsync();
    }
}