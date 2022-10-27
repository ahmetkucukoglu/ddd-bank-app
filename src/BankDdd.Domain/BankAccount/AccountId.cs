namespace BankDdd.Domain.BankAccount;

public record AccountId
{
    public Guid Id { get; }

    public AccountId(Guid id)
    {
        ArgumentNullException.ThrowIfNull(id);
        
        Id = id;
    }
}