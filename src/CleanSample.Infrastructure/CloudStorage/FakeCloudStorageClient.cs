using CleanSample.Domain;

namespace CleanSample.Infrastructure.CloudStorage;

public class FakeCloudStorageClient : ICloudStorageClient<Product, Guid>
{
    private static readonly Dictionary<Guid, Product> _products = [];

    public Task<Product?> GetEntityAsync(Guid identifier, CancellationToken cancellationToken) =>
        Task.FromResult(_products.GetValueOrDefault(identifier));

    public Task AddEntityAsync(Product entity, CancellationToken cancellationToken)
    {
        _products[entity.Id] = entity;
        return Task.CompletedTask;
    }
}