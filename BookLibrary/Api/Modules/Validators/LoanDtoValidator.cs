using Api.Dtos;
using FluentValidation;

namespace Api.Modules.Validators;

public class LoanDtoValidator : AbstractValidator<LoanDto>
{
    public LoanDtoValidator()
    {
        RuleFor(x => x.ReaderId).NotEmpty();
        RuleFor(x => x.BookId).NotEmpty();
        RuleFor(x => x.LibrarianId).NotEmpty();
        RuleFor(x => x.LoanDate).NotEmpty().LessThan(DateTime.UtcNow).WithMessage("Loan date cannot be in the future.");
        RuleFor(x => x.ReturnDate).NotEmpty().GreaterThan(x => x.LoanDate).WithMessage("Return date must be after the loan date.");
    }
}
