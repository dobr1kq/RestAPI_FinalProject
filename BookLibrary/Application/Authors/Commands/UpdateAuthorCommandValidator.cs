using FluentValidation;

namespace Application.Authors.Commands;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(x => x.AuthorId).NotEmpty().WithMessage("AuthorId is required.");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.")
            .MaximumLength(255).WithMessage("First name must not exceed 255 characters.")
            .MinimumLength(2).WithMessage("First name must be at least 2 characters long.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(255).WithMessage("Last name must not exceed 255 characters.")
            .MinimumLength(2).WithMessage("Last name must be at least 2 characters long.");
        RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required.")
            .MaximumLength(255).WithMessage("Country must not exceed 255 characters.");
        RuleFor(x => x.DateOfBirth).LessThan(DateTime.UtcNow).WithMessage("Date of birth must be in the past.");
    }
}
