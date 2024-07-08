using CleanSample.Domain;
using MediatR;

namespace CleanSample.Application.Products.Queries;

public class GetProductQuery(Guid productId) : IRequest<Product?>
{
    public Guid ProductId { get; set; } = productId;
}