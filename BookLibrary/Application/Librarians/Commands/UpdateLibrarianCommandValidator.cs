using FluentValidation;

namespace Application.Librarians.Commands;

public class UpdateLibrarianCommandValidator : AbstractValidator<UpdateLibrarianCommand>
{
    public UpdateLibrarianCommandValidator()
    {
        RuleFor(x => x.LibrarianId).NotEmpty().WithMessage("Librarian ID is required.");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.TelephoneNumber).NotEmpty().WithMessage("Telephone number is required.");
    }
}
