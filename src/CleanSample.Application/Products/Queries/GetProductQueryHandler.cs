using CleanSample.Application.Interfaces;
using CleanSample.Domain;
using MediatR;

namespace CleanSample.Application.Products.Queries;

public class GetProductQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductQuery, Product?>
{
    public Task<Product?> Handle(GetProductQuery query, CancellationToken cancellationToken) =>
        productRepository.GetProductAsync(query.ProductId, cancellationToken);
}