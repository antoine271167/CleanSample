using CleanSample.Application;
using CleanSample.Infrastructure;

namespace CleanSample.Presentation.AppService;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }

    internal static void ConfigureServices(IServiceCollection services) =>
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddInfrastructureDependencies()
            .AddApplicationDependencies()
            .AddControllers();
}