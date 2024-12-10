using Api.Dtos;
using FluentValidation;

namespace Api.Modules.Validators;

public class BookDtoValidator : AbstractValidator<BookDto>
{
    public BookDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255).MinimumLength(3);
        RuleFor(x => x.Date).NotEmpty().LessThan(DateTime.UtcNow).WithMessage("Book date cannot be in the future.");
    }
}
