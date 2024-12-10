using FluentValidation;

namespace Application.Librarians.Commands;

public class DeleteLibrarianCommandValidator : AbstractValidator<DeleteLibrarianCommand>
{
    public DeleteLibrarianCommandValidator()
    {
        RuleFor(x => x.LibrarianId).NotEmpty().WithMessage("Librarian ID is required.");
    }
}
