using CleanSample.Application.Products.Dto;
using MediatR;

namespace CleanSample.Application.Products.Commands;

public class AddProductCommand(ProductDto product) : IRequest
{
    public ProductDto Product { get; } = product;
}