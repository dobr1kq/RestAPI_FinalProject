using System.Net;
using System.Net.Http.Json;
using Api.Dtos;
using Domain.Librarians;
using Domain.Loans;
using Domain.Readers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Test.Common;
using Test.Data;
using Xunit;

namespace Api.Tests.Integration;

public class LoansControllerTests : BaseIntegrationTest, IAsyncLifetime
{
    private readonly Loan _mainLoan;

    public LoansControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
        _mainLoan = LoanData.MainLoan;
    }

    [Fact]
    public async Task ShouldCreateLoanSuccessfully()
    {
        var newLoanDate = DateTime.UtcNow;
        var newReturnDate = DateTime.UtcNow.AddMonths(1);
        var newBookId = Guid.NewGuid();
        var newReaderId = Guid.NewGuid();
        var newLibrarianId = Guid.NewGuid();

        var newLoanRequest = new LoanDto(
            Id: Guid.NewGuid(),
            LoanDate: newLoanDate,
            ReturnDate: newReturnDate,
            BookId: newBookId,
            ReaderId: newReaderId,
            LibrarianId: newLibrarianId
        );
        
        var createResponse = await Client.PostAsJsonAsync("loans", newLoanRequest);
        
        createResponse.IsSuccessStatusCode.Should().BeTrue();

        var createdLoan = await createResponse.ToResponseModel<LoanDto>();

        var loanInDb = await Context.Loans
            .FirstAsync(l => l.Id == new LoanId(createdLoan.Id));
        
        loanInDb!.LoanDate.ToString("yyyy-MM-dd HH:mm")
            .Should().Be(newLoanDate.ToString("yyyy-MM-dd HH:mm"));
        loanInDb.ReturnDate.ToString("yyyy-MM-dd HH:mm")
            .Should().Be(newReturnDate.ToString("yyyy-MM-dd HH:mm"));
    }

    [Fact]
    public async Task ShouldUpdateExistingLoan()
    {
        var updatedLoanDate = DateTime.UtcNow;
        var updatedReturnDate = DateTime.UtcNow.AddMonths(3);
        var newLoanBookId = BookData.MainBook.Id.Value;
        var newLoanReaderId = ReaderData.MainReader.Id.Value;
        var newLoanLibrarianId = LibrarianData.MainLibrarian.Id.Value;

        var updateRequest = new LoanDto(
            Id: _mainLoan.Id.Value,
            LoanDate: updatedLoanDate,
            ReturnDate: updatedReturnDate,
            BookId: newLoanBookId,
            ReaderId: newLoanReaderId,
            LibrarianId: newLoanLibrarianId
        );
        
        var updateResponse = await Client.PutAsJsonAsync("loans", updateRequest);
        
        updateResponse.IsSuccessStatusCode.Should().BeTrue();

        var updatedLoan = await updateResponse.ToResponseModel<LoanDto>();

        var loanInDb = await Context.Loans
            .FirstAsync(l => l.Id == new LoanId(updatedLoan.Id));
        
        loanInDb!.LoanDate.ToString("yyyy-MM-dd HH:mm")
            .Should().Be(updatedLoanDate.ToString("yyyy-MM-dd HH:mm"));
        loanInDb.ReturnDate.ToString("yyyy-MM-dd HH:mm")
            .Should().Be(updatedReturnDate.ToString("yyyy-MM-dd HH:mm"));
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenLoanDoesNotExist()
    {
        var nonExistentLoanId = Guid.NewGuid();
        
        var nonExistentLoanRequest = new LoanDto(
            Id: nonExistentLoanId,
            LoanDate: DateTime.UtcNow,
            ReturnDate: DateTime.UtcNow.AddMonths(1),
            BookId: Guid.NewGuid(),
            ReaderId: Guid.NewGuid(),
            LibrarianId: Guid.NewGuid()
        );
        
        var response = await Client.PutAsJsonAsync("loans", nonExistentLoanRequest);
        
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    public async Task InitializeAsync()
    {
        await Context.Loans.AddAsync(_mainLoan);

        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.Loans.RemoveRange(Context.Loans);

        await SaveChangesAsync();
    }
}
