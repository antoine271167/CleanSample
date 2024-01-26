using CleanSample.Application.Products.Requests;
using FluentValidation;

namespace CleanSample.Application.Products.Validators;

public class AddProductRequestValidator : AbstractValidator<AddProductRequest>
{
    public AddProductRequestValidator()
    {
        RuleFor(e => e.Product).NotNull();
        RuleFor(e => e.Product.Id).NotEmpty();
        RuleFor(e => e.Product.Name).NotEmpty();
    }
}