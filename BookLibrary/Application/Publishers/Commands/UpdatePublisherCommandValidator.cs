using FluentValidation;

namespace Application.Publishers.Commands;

public class UpdatePublisherCommandValidator : AbstractValidator<UpdatePublisherCommand>
{
    public UpdatePublisherCommandValidator()
    {
        RuleFor(x => x.PublisherId)
            .NotEmpty()
            .WithMessage("Publisher ID is required.");

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
