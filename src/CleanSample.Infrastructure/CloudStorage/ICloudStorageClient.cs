namespace CleanSample.Infrastructure.CloudStorage;

public interface ICloudStorageClient<TEntity, in TIdentity> where TEntity : class
{
    public Task<TEntity?> GetEntityAsync(TIdentity identifier, CancellationToken cancellationToken);
    public Task AddEntityAsync(TEntity entity, CancellationToken cancellationToken);
}