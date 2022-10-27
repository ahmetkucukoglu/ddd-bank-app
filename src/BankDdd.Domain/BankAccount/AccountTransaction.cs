using BankDdd.Domain.BankMoney;

namespace BankDdd.Domain.BankAccount;

public class AccountTransaction
{
    public AccountTransactionId Id { get; }
    public Money Money { get; }
    public AccountTransactionType Type { get; }
    public DateTime CreatedAt { get; }

    public AccountTransaction(AccountTransactionId id, Money money, AccountTransactionType type)
    {
        Id = id;
        Money = money;
        Type = type;
        CreatedAt = DateTime.Now;
    }
}