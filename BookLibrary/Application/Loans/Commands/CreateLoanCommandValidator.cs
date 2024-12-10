using FluentValidation;

namespace Application.Loans.Commands;

public class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommand>
{
    public CreateLoanCommandValidator()
    {
        RuleFor(x => x.ReaderId).NotEmpty().WithMessage("Reader ID is required.");
        RuleFor(x => x.BookId).NotEmpty().WithMessage("Book ID is required.");
        RuleFor(x => x.LibrarianId).NotEmpty().WithMessage("Librarian ID is required.");
        RuleFor(x => x.LoanDate).LessThan(x => x.ReturnDate).WithMessage("Loan date must be earlier than return date.");
        RuleFor(x => x.ReturnDate).GreaterThan(x => x.LoanDate).WithMessage("Return date must be later than loan date.");
    }
}
