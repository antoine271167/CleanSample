using CleanSample.Application.Products.Requests;
using FluentValidation;

namespace CleanSample.Application.Products.Validators;

public class GetProductRequestValidator : AbstractValidator<GetProductRequest>
{
    public GetProductRequestValidator()
    {
        RuleFor(e => e.ProductId).NotEmpty();
    }
}