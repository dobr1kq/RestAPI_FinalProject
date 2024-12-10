using FluentValidation;

namespace Application.Authors.Commands;

public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(x => x.AuthorId).NotEmpty().WithMessage("AuthorId is required.");
    }
}
