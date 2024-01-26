using CleanSample.Application.Products.Dto;
using MediatR;

namespace CleanSample.Application.Products.Requests;

public class GetProductRequest(Guid productId) : IRequest<ProductDto>
{
    public Guid ProductId { get; } = productId;
}