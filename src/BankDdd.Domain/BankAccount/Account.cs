using BankDdd.Domain.BankCustomer;
using BankDdd.Domain.BankMoney;

namespace BankDdd.Domain.BankAccount;

public class Account
{
    public AccountId Id { get; }
    public CustomerId CustomerId { get; }
    public Money Balance { get; private set; }
    private List<AccountTransaction> _transactions = new();
    public IReadOnlyCollection<AccountTransaction> Transactions => _transactions.AsReadOnly();

    public Account(AccountId id, CustomerId customerId, Currency currency)
    {
        Id = id;
        CustomerId = customerId;
        Balance = Money.Zero(currency);
    }

    public void Deposit(Money money)
    {
        Balance += money;

        _transactions.Add(new AccountTransaction(new AccountTransactionId(Guid.NewGuid()), money,
            AccountTransactionType.Deposit));
    }

    //First way for domain service
    public void Withdraw(Money money, ICustomerRepository customerRepository)
    {
        var customer = customerRepository.GetCustomer(CustomerId);
        
        if (customer.IsBlocked)
        {
            throw new CustomerIsBlocked();
        }
        
        if (Balance - money < Money.Zero(Balance.Currency)) throw new BalanceIsInsufficient();

        Balance -= money;

        _transactions.Add(new AccountTransaction(new AccountTransactionId(Guid.NewGuid()), money,
            AccountTransactionType.Withdraw));
    }
    
    //Second way for domain service
    public void Withdraw(Money money, Customer customer)
    {
        if (customer.IsBlocked)
        {
            throw new CustomerIsBlocked();
        }

        if (Balance - money < Money.Zero(Balance.Currency)) throw new BalanceIsInsufficient();

        Balance -= money;

        _transactions.Add(new AccountTransaction(new AccountTransactionId(Guid.NewGuid()), money,
            AccountTransactionType.Withdraw));
    }

    //The best way for domain service
    internal void Withdraw(Money money)
    {
        if (Balance - money < Money.Zero(Balance.Currency)) throw new BalanceIsInsufficient();

        Balance -= money;

        _transactions.Add(new AccountTransaction(new AccountTransactionId(Guid.NewGuid()), money,
            AccountTransactionType.Withdraw));
    }
}