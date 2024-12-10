using FluentValidation;

namespace Application.Loans.Commands;

public class DeleteLoanCommandValidator : AbstractValidator<DeleteLoanCommand>
{
    public DeleteLoanCommandValidator()
    {
        RuleFor(x => x.LoanId).NotEmpty().WithMessage("LoanId is required.");
    }
}
