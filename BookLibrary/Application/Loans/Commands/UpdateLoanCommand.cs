using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Loans.Exceptions;
using Domain.Books;
using Domain.Librarians;
using Domain.Loans;
using Domain.Readers;
using MediatR;

namespace Application.Loans.Commands;

public record UpdateLoanCommand : IRequest<Result<Loan, LoanException>>
{
    public required Guid LoanId { get; init; }
    public required Guid ReaderId { get; init; }
    public required Guid BookId { get; init; }
    public required Guid LibrarianId { get; init; }
    public required DateTime LoanDate { get; init; }
    public required DateTime ReturnDate { get; init; }
}
public class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, Result<Loan, LoanException>>
{
    private readonly ILoanRepository loanRepository;

    public UpdateLoanCommandHandler(ILoanRepository loanRepository)
    {
        this.loanRepository = loanRepository;
    }

    public async Task<Result<Loan, LoanException>> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
    {
        var loanId = new LoanId(request.LoanId);
        var readerId = new ReaderId(request.ReaderId);
        var bookId = new BookId(request.BookId);
        var librarianId = new LibrarianId(request.LibrarianId);

        var existingLoan = await loanRepository.GetById(loanId, cancellationToken);

        return await existingLoan.Match(
            async loan => await UpdateEntity(loan, request.LoanDate, request.ReturnDate, bookId, readerId, librarianId, cancellationToken),
            () => Task.FromResult<Result<Loan, LoanException>>(new LoanNotFoundException(loanId))
        );
    }

    private async Task<Result<Loan, LoanException>> UpdateEntity(
        Loan loan,
        DateTime loanDate,
        DateTime returnDate,
        BookId bookId,
        ReaderId readerId,
        LibrarianId librarianId,
        CancellationToken cancellationToken)
    {
        try
        {
            loan.UpdateDetails(loanDate, returnDate, readerId, bookId, librarianId);
            return await loanRepository.Update(loan, cancellationToken);
        }
        catch (LoanInvalidReturnDateException ex)
        {
            return ex;
        }
        catch (Exception ex)
        {
            return new LoanUnknownException(loan.Id, ex);
        }
    }
}
