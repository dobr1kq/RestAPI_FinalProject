using Api.Dtos;
using FluentValidation;

namespace Api.Modules.Validators;

public class ReaderDtoValidator : AbstractValidator<ReaderDto>
{
    public ReaderDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(255).MinimumLength(3);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(255).MinimumLength(3);
        RuleFor(x => x.TelephoneNumber).NotEmpty().Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");
    }
}
