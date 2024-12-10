using FluentValidation;

namespace Application.Genres.Commands;

public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(x => x.GenreName)
            .NotEmpty().WithMessage("Genre name is required.")
            .MaximumLength(100).WithMessage("Genre name must not exceed 100 characters.")
            .MinimumLength(3).WithMessage("Genre name must be at least 3 characters long.");
    }
}
