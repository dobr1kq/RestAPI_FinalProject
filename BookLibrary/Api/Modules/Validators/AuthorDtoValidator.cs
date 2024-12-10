using Api.Dtos;
using FluentValidation;

namespace Api.Modules.Validators;

public class AuthorDtoValidator : AbstractValidator<AuthorDto>
{
    public AuthorDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(255).MinimumLength(3);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(255).MinimumLength(3);
        RuleFor(x => x.Country).NotEmpty().MaximumLength(255);
        RuleFor(x => x.DateOfBirth).NotEmpty().LessThan(DateTime.UtcNow).WithMessage("Date of birth cannot be in the future.");
    }
}
