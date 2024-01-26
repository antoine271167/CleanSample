using CleanSample.Application.Interfaces;
using CleanSample.Domain;
using CleanSample.Infrastructure.CloudStorage;

namespace CleanSample.Infrastructure.Products;

public class ProductRepository(ICloudStorageClient<Product, Guid> client) : IProductRepository
{
    public Task<Product?> GetProductAsync(Guid id, CancellationToken cancellationToken) =>
        client.GetEntityAsync(id, cancellationToken);

    public Task AddProductAsync(Product product, CancellationToken cancellationToken) =>
        client.AddEntityAsync(product, cancellationToken);
}