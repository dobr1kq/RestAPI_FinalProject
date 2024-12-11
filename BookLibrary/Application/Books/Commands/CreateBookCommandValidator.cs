using FluentValidation;

namespace Application.Books.Commands;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Book name cannot be empty.")
            .MaximumLength(255).WithMessage("Book name cannot exceed 255 characters.")
            .MinimumLength(3).WithMessage("Book name must be at least 3 characters long.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Book date cannot be empty.");

        RuleFor(x => x.GenreId)
            .NotEmpty().WithMessage("GenreId cannot be empty.");

        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("AuthorId cannot be empty.");
        
        RuleFor(x => x.PublisherId)
            .NotEmpty().WithMessage("Publisher ID is required.");
    }
}
