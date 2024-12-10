using FluentValidation;

namespace Application.Books.Commands;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Book name is required.")
            .MaximumLength(255).WithMessage("Book name must not exceed 255 characters.")
            .MinimumLength(3).WithMessage("Book name must be at least 3 characters long.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Date cannot be in the future.");
    }
}
