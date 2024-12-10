using FluentValidation;

namespace Application.Publishers.Commands;

public class DeletePublisherCommandValidator : AbstractValidator<DeletePublisherCommand>
{
    public DeletePublisherCommandValidator()
    {
        RuleFor(x => x.PublisherId)
            .NotEmpty()
            .WithMessage("Publisher ID is required.");
    }
}
