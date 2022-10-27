namespace BankDdd.Domain.BankCustomer;

public record CustomerId
{
    public Guid Id { get; }

    public CustomerId(Guid id)
    {
        ArgumentNullException.ThrowIfNull(id);
        
        Id = id;
    }
}