using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanSample.Application;

public static class DependencyExtensions
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services) =>
        services
            .AddValidatorsFromAssemblyContaining(typeof(DependencyExtensions))
            .AddAutoMapper(typeof(DependencyExtensions))
            .AddMediatR(configuration =>
                configuration.RegisterServicesFromAssemblyContaining(typeof(DependencyExtensions)));
}