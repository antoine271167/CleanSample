using CleanSample.Application.Interfaces;
using CleanSample.Application.Products.Queries;
using CleanSample.Domain;
using NSubstitute;

namespace CleanSample.Application.Tests.Products.Queries;

public class GetProductQueryHandlerTests
{
    private readonly Fixture _fixture = new();
    private readonly IProductRepository _repository = Substitute.For<IProductRepository>();

    [Fact]
    public async Task Handle_ShouldCall_Repository()
    {
        // Arrange
        var query = _fixture.Create<GetProductQuery>();
        var expected = _fixture.Build<Product>()
            .FromFactory<string>(name => new Product(query.ProductId, name))
            .Create();
        _repository.GetProductAsync(Arg.Is<Guid>(id => id == query.ProductId), Arg.Any<CancellationToken>())
            .Returns(expected);

        var sut = new GetProductQueryHandler(_repository);

        // Act
        var actual = await sut.Handle(query, CancellationToken.None);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public async Task Handle_ProductNotFound_ShouldReturnNull()
    {
        // Arrange
        var query = _fixture.Create<GetProductQuery>();
        _repository.GetProductAsync(Arg.Is<Guid>(id => id == query.ProductId), Arg.Any<CancellationToken>())
            .Returns((Product?)null);

        var sut = new GetProductQueryHandler(_repository);

        // Act
        var actual = await sut.Handle(query, CancellationToken.None);

        // Assert
        actual.Should().BeNull();
    }
}