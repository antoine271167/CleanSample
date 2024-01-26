using AutoMapper;
using CleanSample.Application.Interfaces;
using CleanSample.Domain;
using MediatR;

namespace CleanSample.Application.Products.Commands;

public class AddProductCommandHandler(IProductRepository productRepository, IMapperBase mapper) :
    IRequestHandler<AddProductCommand>
{
    public async Task Handle(AddProductCommand command, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(command.Product);
        await productRepository.AddProductAsync(product);
    }
}