using AutoMapper;
using CleanSample.Application.Interfaces;
using CleanSample.Application.Products.Commands;
using CleanSample.Application.Products.Mapping;
using CleanSample.Domain;
using NSubstitute;

namespace CleanSample.Application.Tests.Products.Commands;

public class AddProductCommandHandlerTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task Handle_ShouldCallRepository()
    {
        // Arrange
        var repository = Substitute.For<IProductRepository>();
        var mapper = new Mapper(new MapperConfiguration(c => c.AddProfile(typeof(ProductMappingProfile))));
        var command = _fixture.Create<AddProductCommand>();

        var sut = new AddProductCommandHandler(repository, mapper);

        // Act
        await sut.Handle(command, CancellationToken.None);

        // Assert
        await repository.Received(1).AddProductAsync(Arg.Is<Product>(product =>
            product.Id == command.Product.Id && product.Name == command.Product.Name), Arg.Any<CancellationToken>());
    }
}