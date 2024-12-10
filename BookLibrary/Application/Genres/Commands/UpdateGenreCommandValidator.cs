using FluentValidation;

namespace Application.Genres.Commands;

public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(x => x.GenreId).NotEmpty().WithMessage("GenreId is required.");
        RuleFor(x => x.GenreName).NotEmpty().WithMessage("Genre name is required.")
            .MaximumLength(255).WithMessage("Genre name must not exceed 255 characters.")
            .MinimumLength(3).WithMessage("Genre name must be at least 3 characters long.");
    }
}
