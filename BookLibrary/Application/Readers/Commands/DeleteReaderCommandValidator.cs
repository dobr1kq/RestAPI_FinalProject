using FluentValidation;

namespace Application.Readers.Commands;

public class DeleteReaderCommandValidator : AbstractValidator<DeleteReaderCommand>
{
    public DeleteReaderCommandValidator()
    {
        RuleFor(x => x.ReaderId)
            .NotEmpty()
            .WithMessage("ReaderId is required.");
    }
}
