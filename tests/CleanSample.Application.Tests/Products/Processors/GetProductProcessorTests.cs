using AutoMapper;
using CleanSample.Application.Products.Mapping;
using CleanSample.Application.Products.Processors;
using CleanSample.Application.Products.Queries;
using CleanSample.Application.Products.Requests;
using CleanSample.Application.Products.Validators;
using CleanSample.Domain;
using FluentValidation;
using MediatR;
using NSubstitute;

namespace CleanSample.Application.Tests.Products.Processors;

public class GetProductProcessorTests
{
    private readonly Fixture _fixture = new();

    private readonly IMapper _mapper =
        new Mapper(new MapperConfiguration(c => c.AddProfile(typeof(ProductMappingProfile))));

    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly GetProductRequestValidator _validator = new();

    [Fact]
    public async Task Handle_ShouldCallMediator()
    {
        // Arrange
        var name = _fixture.Create<string>();
        var request = _fixture.Create<GetProductRequest>();

        _mediator.Send(Arg.Is<GetProductQuery>(query =>
            query.ProductId == request.ProductId)).Returns(new Product(request.ProductId, name));

        var sut = new GetProductProcessor(_mediator, _mapper, _validator);

        // Act
        var actual = await sut.Handle(request, CancellationToken.None);

        // Assert
        actual.Should().NotBeNull();
        actual.Id.Should().Be(request.ProductId);
        actual.Name.Should().Be(name);
        await _mediator.Received(1).Send(Arg.Is<GetProductQuery>(query =>
            query.ProductId == request.ProductId));
    }

    [Fact]
    public async Task Handle_WithInvalidInput_ShouldFail()
    {
        // Arrange
        var request = _fixture.Build<GetProductRequest>()
            .FromFactory(() => new GetProductRequest(Guid.Empty))
            .Create();

        var sut = new GetProductProcessor(_mediator, _mapper, _validator);

        // Act
        var actual = () => sut.Handle(request, CancellationToken.None);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }
}