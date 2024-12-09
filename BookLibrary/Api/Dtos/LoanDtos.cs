using Domain.Loans;

namespace Api.Dtos;

public record LoanDto(
    Guid Id,
    Guid ReaderId,
    Guid BookId,
    Guid LibrarianId,
    DateTime LoanDate,
    DateTime ReturnDate)
{
    public static LoanDto FromDomainModel(Loan loan)
        => new(
            Id: loan.Id.Value,
            ReaderId: loan.ReaderId.Value,
            BookId: loan.BookId.Value,
            LibrarianId: loan.LibrarianId.Value,
            LoanDate: loan.LoanDate,
            ReturnDate: loan.ReturnDate);
}