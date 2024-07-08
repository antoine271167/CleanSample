using CleanSample.Domain;

namespace CleanSample.Infrastructure.CloudStorage;

public class FakeCloudStorageClient : ICloudStorageClient<Product, Guid>
{
    private static readonly Dictionary<Guid, Product> _products = [];

    public Task<Product?> GetEntityAsync(Guid identifier, CancellationToken cancellationToken) =>
        Task.FromResult(_products.GetValueOrDefault(identifier));

    public Task AddEntityAsync(Product entity, CancellationToken cancellationToken)
    {
        if (!_products.TryAdd(entity.Id, entity))
        {
            throw new ArgumentException("Entity already exists in store.");
        }

        return Task.CompletedTask;
    }
}