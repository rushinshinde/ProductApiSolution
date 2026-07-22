using Application.DTOs.Product;
using FluentValidation;

namespace Application.Validators.Product;

public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty()
            .WithMessage("Product Name is required.")
            .MaximumLength(255);

        RuleFor(x => x.CreatedBy)
            .NotEmpty()
            .WithMessage("Created By is required.")
            .MaximumLength(100);
    }
}