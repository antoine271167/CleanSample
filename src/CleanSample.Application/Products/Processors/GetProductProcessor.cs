using AutoMapper;
using CleanSample.Application.Products.Dto;
using CleanSample.Application.Products.Queries;
using CleanSample.Application.Products.Requests;
using FluentValidation;
using MediatR;
using ValidationException = FluentValidation.ValidationException;

namespace CleanSample.Application.Products.Processors;

public class GetProductProcessor(ISender mediator, IMapper mapper, IValidator<GetProductRequest> validator) :
    IRequestHandler<GetProductRequest, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var product = await mediator.Send(new GetProductQuery(request.ProductId), cancellationToken);
        return mapper.Map<ProductDto>(product);
    }
}