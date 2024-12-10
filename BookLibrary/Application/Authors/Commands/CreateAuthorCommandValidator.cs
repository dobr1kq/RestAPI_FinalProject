using Domain.Authors;
using FluentValidation;

namespace Application.Authors.Commands;

public class AuthorValidator : AbstractValidator<Author>
{
    public AuthorValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(255).MinimumLength(1);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(255).MinimumLength(1);
        RuleFor(x => x.Country).NotEmpty().MaximumLength(100).MinimumLength(1);
        RuleFor(x => x.DateOfBirth).NotEmpty().LessThan(DateTime.UtcNow).WithMessage("Date of birth cannot be in the future.");
    }
}