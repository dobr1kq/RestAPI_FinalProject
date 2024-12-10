using FluentValidation;

namespace Application.Readers.Commands;

public class CreateReaderCommandValidator : AbstractValidator<CreateReaderCommand>
{
    public CreateReaderCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .MaximumLength(50)
            .WithMessage("First name must not exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MaximumLength(50)
            .WithMessage("Last name must not exceed 50 characters.");

        RuleFor(x => x.TelephoneNumber)
            .NotEmpty()
            .WithMessage("Telephone number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("Invalid telephone number format.");
    }
}
