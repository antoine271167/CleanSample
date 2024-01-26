namespace CleanSample.Application.Products.Dto;

public class ProductDto(Guid id, string name)
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
}