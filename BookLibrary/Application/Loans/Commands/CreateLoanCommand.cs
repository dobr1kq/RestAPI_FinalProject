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
    public required ReaderId ReaderId { get; init; }
    public required BookId BookId { get; init; }
    public required LibrarianId LibrarianId { get; init; }
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
        var existingLoan = await loanRepository.GetByReaderAndBook(request.ReaderId, request.BookId, cancellationToken);

        return await existingLoan.Match<Task<Result<Loan, LoanException>>>(
            loan => Task.FromResult<Result<Loan, LoanException>>(new LoanAlreadyExistsException(loan.Id)),
            async () => await CreateEntity(request.ReaderId, request.BookId, request.LibrarianId, request.LoanDate, request.ReturnDate, cancellationToken)
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
