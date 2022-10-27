using BankDdd.Domain.BankPhoneNumber;
using Xunit;

namespace BankDdd.Tests;

public class PhoneNumberTests
{
    [Fact]
    public void Should_Be_True_When_GetTheLastFourDigits()
    {
        var phoneNumber = new PhoneNumber("905551112233");

        Assert.Equal("2233", phoneNumber.TheLastFourDigits);
    }

    [Fact]
    public void Should_Be_True_When_ComparePhoneNumbers()
    {
        var phoneNumber1 = new PhoneNumber("905551112233");
        var phoneNumber2 = new PhoneNumber("905551112233");
        var phoneNumber3 = new PhoneNumber("905551112234");

        Assert.True(phoneNumber1 == phoneNumber2);
        Assert.False(phoneNumber1 == phoneNumber3);
    }
    
    [Fact]
    public void Should_Throw_Exception_When_PhoneNumberIsNotValid()
    {
        Assert.Throws<PhoneNumberIsNotValid>(() => new PhoneNumber("1453"));
    }
}