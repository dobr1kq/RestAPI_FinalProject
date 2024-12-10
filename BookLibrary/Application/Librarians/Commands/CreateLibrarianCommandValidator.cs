using FluentValidation;

namespace Application.Librarians.Commands;

public class CreateLibrarianCommandValidator : AbstractValidator<CreateLibrarianCommand>
{
    public CreateLibrarianCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.")
            .MaximumLength(255).WithMessage("First name must not exceed 255 characters.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(255).WithMessage("Last name must not exceed 255 characters.");
        RuleFor(x => x.TelephoneNumber).NotEmpty().WithMessage("Telephone number is required.")
            .Matches(@"^\+?\d{1,4}[\s-]?\(?\d{1,3}\)?[\s-]?\d{1,3}[\s-]?\d{1,4}$")
            .WithMessage("Invalid telephone number format.");
    }
}
