using CleanSample.Presentation.AppService.Tests.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CleanSample.Presentation.AppService.Tests;

public class ProgramTests
{
    [Fact]
    public void TestDependencyInjection()
    {
        // Arrange
        var services = new ServiceCollection();

        var controllers = typeof(Program).Assembly.GetTypes()
            .Where(type => type.IsSubclassOf(typeof(ControllerBase))).ToList();

        controllers.ForEach(controller => services.AddScoped(controller));
        Program.ConfigureServices(services);

        // Act
        var actual = () => services.ValidateDependencyGraph();

        // Assert
        actual.Should().NotThrow();
    }
}