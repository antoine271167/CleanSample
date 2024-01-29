using Microsoft.Extensions.DependencyInjection;

namespace CleanSample.Presentation.Tests.Extensions;

public static class DependencyInjectionExtension
{
    public static void ValidateDependencyGraph(this IServiceCollection services)
    {
        var firstNameSpaceSegment = $"{typeof(DependencyInjectionExtension).Namespace!.Split('.')[0]}.";

        var provider = services.BuildServiceProvider();
        foreach (var serviceDescriptor in services)
        {
            // Skip open generics
            if (serviceDescriptor.ServiceType.IsGenericTypeDefinition)
            {
                continue;
            }

            // Only our own services.
            if (serviceDescriptor.ServiceType.Namespace != null &&
                !serviceDescriptor.ServiceType.Namespace.StartsWith(firstNameSpaceSegment))
            {
                continue;
            }

            // Skip singletons that have already been instantiated
            if (provider.GetService(serviceDescriptor.ServiceType) is IDisposable disposable)
            {
#pragma warning disable S3966
                disposable.Dispose();
#pragma warning restore S3966
            }
            else
            {
                // Try get the service.
                provider.GetRequiredService(serviceDescriptor.ServiceType);
            }
        }
    }
}