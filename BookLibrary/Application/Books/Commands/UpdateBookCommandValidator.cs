using FluentValidation;

namespace Application.Books.Commands;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.BookId).NotEmpty().WithMessage("BookId is required.");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Book name is required.")
            .MaximumLength(255).WithMessage("Book name must not exceed 255 characters.")
            .MinimumLength(3).WithMessage("Book name must be at least 3 characters long.");

        RuleFor(x => x.Date)
            .LessThan(DateTime.UtcNow).WithMessage("Book date must not be in the future.");
        RuleFor(x => x.GenreId)
            .NotEmpty().WithMessage("GenreId cannot be empty.");

        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("AuthorId cannot be empty.");
        
        RuleFor(x => x.PublisherId)
            .NotEmpty().WithMessage("Publisher ID is required.");
    }
}
