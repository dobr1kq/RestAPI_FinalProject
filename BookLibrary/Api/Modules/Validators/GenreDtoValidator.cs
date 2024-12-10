using Api.Dtos;
using FluentValidation;

namespace Api.Modules.Validators;

public class GenreDtoValidator : AbstractValidator<GenreDto>
{
    public GenreDtoValidator()
    {
        RuleFor(x => x.GenreName).NotEmpty().MaximumLength(255).MinimumLength(3);
    }
}
