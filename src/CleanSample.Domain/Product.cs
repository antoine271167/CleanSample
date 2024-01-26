namespace CleanSample.Domain;

public class Product(Guid id, string name) : AggregateRoot<Guid>(id)
{
    public string Name { get; } = name;
}