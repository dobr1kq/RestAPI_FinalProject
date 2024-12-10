using FluentValidation;

namespace Application.Publishers.Commands;

public class CreatePublisherCommandValidator : AbstractValidator<CreatePublisherCommand>
{
    public CreatePublisherCommandValidator()
    {
        RuleFor(x => x.PublisherName)
            .NotEmpty()
            .WithMessage("Publisher name is required.")
            .MaximumLength(255)
            .WithMessage("Publisher name must not exceed 255 characters.");

        RuleFor(x => x.PublisherAddress)
            .NotEmpty()
            .WithMessage("Publisher address is required.")
            .MaximumLength(500)
            .WithMessage("Publisher address must not exceed 500 characters.");
    }
}
