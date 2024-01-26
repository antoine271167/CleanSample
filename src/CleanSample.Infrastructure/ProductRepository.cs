using CleanSample.Application.Interfaces;
using CleanSample.Domain;

namespace CleanSample.Infrastructure;

public class ProductRepository : IProductRepository
{
    public Task<Product> GetProductAsync(Guid id) =>
        throw new NotImplementedException();

    public Task AddProductAsync(Product product) =>
        throw new NotImplementedException();
}