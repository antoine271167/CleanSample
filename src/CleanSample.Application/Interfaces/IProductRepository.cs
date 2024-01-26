using CleanSample.Domain;

namespace CleanSample.Application.Interfaces;

public interface IProductRepository
{
    Task<Product> GetProductAsync(Guid id);
    Task AddProductAsync(Product product);
}