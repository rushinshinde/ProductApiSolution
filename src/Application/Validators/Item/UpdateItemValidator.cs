using Application.DTOs.Item;
using FluentValidation;

namespace Application.Validators.Item;

public class UpdateItemValidator : AbstractValidator<UpdateItemDto>
{
    public UpdateItemValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}