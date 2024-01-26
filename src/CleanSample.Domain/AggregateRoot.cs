namespace CleanSample.Domain;

public abstract class AggregateRoot<TIdentity>(TIdentity id)
{
    public TIdentity Id { get; } = id;
}