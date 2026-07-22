using Application.DTOs.Product;
using FluentValidation;

namespace Application.Validators.Product;

public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.ModifiedBy)
            .NotEmpty()
            .MaximumLength(100);
    }
}