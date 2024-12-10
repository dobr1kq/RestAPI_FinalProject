using Api.Dtos;
using FluentValidation;

namespace Api.Modules.Validators;

public class PublisherDtoValidator : AbstractValidator<PublisherDto>
{
    public PublisherDtoValidator()
    {
        RuleFor(x => x.PublisherName).NotEmpty().MaximumLength(255).MinimumLength(3);
        RuleFor(x => x.PublisherAddress).NotEmpty().MaximumLength(255);
    }
}
