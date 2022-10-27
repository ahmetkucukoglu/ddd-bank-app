using BankDdd.Domain.BankMoney;
using Xunit;

namespace BankDdd.Tests;

public class MoneyTests
{

    [Fact]
    public void Should_Be_True_When_ConvertMoneyToString()
    {
        var money = new Money(100, Currency.TL);
        
        Assert.Equal("100₺", money.ToString());
    }

    [Fact]
    public void Should_Be_True_When_CompareCurrency()
    {
        var money1 = new Money(100M, new Currency("TL", "₺"));
        var money2 = new Money(200M, new Currency("TL", "₺"));
        var money3 = new Money(100M, new Currency("USD", "$"));
        var money4 = new Money(100M, new Currency("TL", "₺"));

        Assert.True(money1 < money2);
        Assert.True(money2 > money1);
        Assert.True(money1 == money4);
        Assert.Throws<CurrencyIsNotValid>(() => money1 > money3);
        Assert.Throws<CurrencyIsNotValid>(() => money1 < money3);
    }
}