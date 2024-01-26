using CleanSample.Application.Products.Dto;
using MediatR;

namespace CleanSample.Application.Products.Requests;

public class AddProductRequest(ProductDto product) : IRequest
{
    public ProductDto Product { get; } = product;
}