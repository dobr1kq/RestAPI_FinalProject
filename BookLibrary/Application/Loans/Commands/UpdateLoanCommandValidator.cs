using FluentValidation;

namespace Application.Loans.Commands;

public class UpdateLoanCommandValidator : AbstractValidator<UpdateLoanCommand>
{
    public UpdateLoanCommandValidator()
    {
        RuleFor(x => x.LoanId).NotEmpty().WithMessage("Loan ID is required.");
        RuleFor(x => x.LoanDate).NotEmpty().WithMessage("Loan date is required.");
        RuleFor(x => x.ReturnDate)
            .NotEmpty()
            .WithMessage("Return date is required.")
            .GreaterThan(x => x.LoanDate)
            .WithMessage("Return date must be after the loan date.");
    }
}
