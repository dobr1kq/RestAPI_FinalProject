using FluentValidation;

namespace Application.Readers.Commands;

public class UpdateReaderCommandValidator : AbstractValidator<UpdateReaderCommand>
{
    public UpdateReaderCommandValidator()
    {
        RuleFor(x => x.ReaderId).NotEmpty().WithMessage("ReaderId is required.");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.").MaximumLength(100).WithMessage("First name must not exceed 100 characters.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.").MaximumLength(100).WithMessage("Last name must not exceed 100 characters.");
        RuleFor(x => x.TelephoneNumber).NotEmpty().WithMessage("Telephone number is required.");
    }
}
