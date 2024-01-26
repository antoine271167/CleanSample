using CleanSample.Application.Interfaces;
using CleanSample.Infrastructure.Products;
using Microsoft.Extensions.DependencyInjection;

namespace CleanSample.Infrastructure;

public static class DependencyExtensions
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services) =>
        services.AddScoped<IProductRepository, ProductRepository>();
}