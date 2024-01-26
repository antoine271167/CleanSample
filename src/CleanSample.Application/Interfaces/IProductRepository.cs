using CleanSample.Domain;

namespace CleanSample.Application.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetProductAsync(Guid id, CancellationToken cancellationToken);
    Task AddProductAsync(Product product, CancellationToken cancellationToken);
}