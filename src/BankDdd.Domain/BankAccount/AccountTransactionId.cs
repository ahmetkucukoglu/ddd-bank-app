namespace BankDdd.Domain.BankAccount;

public record AccountTransactionId
{
    public Guid Id { get; }

    public AccountTransactionId(Guid id)
    {
        ArgumentNullException.ThrowIfNull(id);
        
        Id = id;
    }
}