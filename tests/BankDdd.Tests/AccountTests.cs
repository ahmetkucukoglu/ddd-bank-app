using System;
using System.Linq;
using BankDdd.Domain.BankAccount;
using BankDdd.Domain.BankCustomer;
using BankDdd.Domain.BankMoney;
using Moq;
using Xunit;

namespace BankDdd.Tests;

public class AccountTests
{
    [Fact]
    public void Should_Succeed_When_WithdrawMoney()
    {
        var accountManager = new AccountManager();

        var customer = new Customer(new CustomerId(Guid.NewGuid()), "Ahmet", "KÜÇÜKOĞLU");

        var bankAccount = new Account(new AccountId(Guid.NewGuid()), customer.Id, Currency.TL);
        bankAccount.Deposit(new Money(600M, Currency.TL));

        accountManager.Withdraw(bankAccount, customer, new Money(100M, Currency.TL));

        Assert.Equal(new Money(500M, Currency.TL), bankAccount.Balance);
    }

    [Fact]
    public void Should_Throw_BalanceIsInsufficient_When_AmountIsGreaterThanBalance()
    {
        var accountManager = new AccountManager();

        var customer = new Customer(new CustomerId(Guid.NewGuid()), "Ahmet", "KÜÇÜKOĞLU");

        var bankAccount = new Account(new AccountId(Guid.NewGuid()), customer.Id, Currency.TL);
        bankAccount.Deposit(new Money(600M, Currency.TL));

        Assert.Throws<BalanceIsInsufficient>(() =>
            accountManager.Withdraw(bankAccount, customer, new Money(705M, Currency.TL)));

        Assert.Equal(new Money(600M, Currency.TL), bankAccount.Balance);
    }

    [Fact]
    public void Should_Throw_CustomerIsBlocked_When_CustomerIsBlocked()
    {
        var accountManager = new AccountManager();

        var customer = new Customer(new CustomerId(Guid.NewGuid()), "Ahmet", "KÜÇÜKOĞLU");
        customer.Block("TEST");

        var bankAccount = new Account(new AccountId(Guid.NewGuid()), customer.Id, Currency.TL);
        bankAccount.Deposit(new Money(600M, Currency.TL));

        Assert.Throws<CustomerIsBlocked>(() =>
            accountManager.Withdraw(bankAccount, customer, new Money(705M, Currency.TL)));

        Assert.Equal(new Money(600M, Currency.TL), bankAccount.Balance);
    }

    [Fact]
    public void Should_Throw_CurrencyIsNotValid_When_CurrencyIsDifferentFromAccountCurrency()
    {
        var customer = new Customer(new CustomerId(Guid.NewGuid()), "Ahmet", "KÜÇÜKOĞLU");

        var bankAccount = new Account(new AccountId(Guid.NewGuid()), customer.Id, Currency.TL);

        var accountManager = new AccountManager();

        Assert.Throws<CurrencyIsNotValid>(() =>
            accountManager.Withdraw(bankAccount, customer, new Money(600M, Currency.USD)));

        Assert.Equal(new Money(0M, Currency.TL), bankAccount.Balance);
    }
    
    [Fact]
    public void Should_Be_True_When_GetAccountTransaction()
    {
        var accountManager = new AccountManager();

        var customer = new Customer(new CustomerId(Guid.NewGuid()), "Ahmet", "KÜÇÜKOĞLU");

        var bankAccount = new Account(new AccountId(Guid.NewGuid()), customer.Id, Currency.TL);
        bankAccount.Deposit(new Money(600M, Currency.TL));

        accountManager.Withdraw(bankAccount, customer, new Money(100M, Currency.TL));

        Assert.Equal(2, bankAccount.Transactions.Count);

        var firstTransaction = bankAccount.Transactions.FirstOrDefault();
        Assert.Equal(AccountTransactionType.Deposit, firstTransaction?.Type);
        Assert.Equal(new Money(600M, Currency.TL), firstTransaction?.Money);
        
        var secondTransaction = bankAccount.Transactions.ElementAt(1);
        Assert.Equal(AccountTransactionType.Withdraw, secondTransaction?.Type);
        Assert.Equal(new Money(100M, Currency.TL), secondTransaction?.Money);
    }
    
    [Fact]
    public void Should_Throw_CustomerIsBlocked_When_CustomerIsBlocked_ImpureDomainModel()
    {
        var customer = new Customer(new CustomerId(Guid.NewGuid()), "Ahmet", "KÜÇÜKOĞLU");
        customer.Block("TEST");

        var mockCustomerRepository = new Mock<ICustomerRepository>();
        mockCustomerRepository.Setup(r => r.GetCustomer(It.IsAny<CustomerId>())).Returns(customer);

        var bankAccount = new Account(new AccountId(Guid.NewGuid()), customer.Id, Currency.TL);
        bankAccount.Deposit(new Money(600M, Currency.TL));

        Assert.Throws<CustomerIsBlocked>(() =>
            bankAccount.Withdraw(new Money(705M, Currency.TL), mockCustomerRepository.Object));

        Assert.Equal(new Money(600M, Currency.TL), bankAccount.Balance);
    }
    
    [Fact]
    public void Should_Throw_CustomerIsBlocked_When_CustomerIsBlocked_PureDomainModel()
    {
        var customer = new Customer(new CustomerId(Guid.NewGuid()), "Ahmet", "KÜÇÜKOĞLU");
        customer.Block("TEST");

        var bankAccount = new Account(new AccountId(Guid.NewGuid()), customer.Id, Currency.TL);
        bankAccount.Deposit(new Money(600M, Currency.TL));

        Assert.Throws<CustomerIsBlocked>(() =>
            bankAccount.Withdraw(new Money(705M, Currency.TL), customer));

        Assert.Equal(new Money(600M, Currency.TL), bankAccount.Balance);
    }
}