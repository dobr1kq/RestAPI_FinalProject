using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Loans.Exceptions;
using Domain.Books;
using Domain.Librarians;
using Domain.Loans;
using Domain.Readers;
using MediatR;

namespace Application.Loans.Commands;

public record CreateLoanCommand : IRequest<Result<Loan, LoanException>>
{
    public required Guid ReaderId { get; init; }
    public required Guid BookId { get; init; }
    public required Guid LibrarianId { get; init; }
    public required DateTime LoanDate { get; init; }
    public required DateTime ReturnDate { get; init; }
}
public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, Result<Loan, LoanException>>
{
    private readonly ILoanRepository loanRepository;

    public CreateLoanCommandHandler(ILoanRepository loanRepository)
    {
        this.loanRepository = loanRepository;
    }

    public async Task<Result<Loan, LoanException>> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        var readerId = new ReaderId(request.ReaderId);
        var bookId = new BookId(request.BookId);
        var librarianId = new LibrarianId(request.LibrarianId);
        
        var existingLoan = await loanRepository.GetByReaderAndBook(readerId, bookId, cancellationToken);

        return await existingLoan.Match<Task<Result<Loan, LoanException>>>(
            loan => Task.FromResult<Result<Loan, LoanException>>(new LoanAlreadyExistsException(loan.Id)),
            async () => await CreateEntity(readerId, bookId, librarianId, request.LoanDate, request.ReturnDate, cancellationToken)
        );
    }

    private async Task<Result<Loan, LoanException>> CreateEntity(ReaderId readerId, BookId bookId, LibrarianId librarianId, DateTime loanDate, DateTime returnDate, CancellationToken cancellationToken)
    {
        try
        {
            var loanId = LoanId.New();
            var loan = Loan.Create(loanId, readerId, bookId, librarianId, loanDate, returnDate);

            return await loanRepository.Add(loan, cancellationToken);
        }
        catch (Exception ex)
        {
            return new LoanUnknownException(LoanId.Empty(), ex);
        }
    }
}
