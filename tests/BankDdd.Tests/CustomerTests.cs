using System;
using BankDdd.Domain.BankCustomer;
using BankDdd.Domain.BankPhoneNumber;
using Xunit;

namespace BankDdd.Tests;

public class CustomerTests
{
    [Fact]
    public void Should_Be_True_When_CompareCustomer()
    {
        var customer1 = new Customer(new CustomerId(Guid.NewGuid()), "Ahmet", "KÜÇÜKOĞLU");
        var customer2 = new Customer(customer1.Id, "Ahmet", "KÜÇÜKOĞLU");
        var customer3 = new Customer(new CustomerId(Guid.NewGuid()), "Ahmet", "KÜÇÜKOĞLU");

        Assert.True(customer1.Equals(customer2));
        Assert.False(customer1.Equals(customer3));
    }
    
    [Fact]
    public void Should_Be_True_When_UpdateName()
    {
        var customer = new Customer(new CustomerId(Guid.NewGuid()), "Ahmet", "KÜÇÜKOĞLU");
        customer.UpdateName("Mehmet","KÜÇÜKOĞLU");

        Assert.Equal("Mehmet", customer.FirstName);
    }
    
    [Fact]
    public void Should_Be_True_When_UpdatePhoneNumber()
    {
        var customer = new Customer(new CustomerId(Guid.NewGuid()), "Ahmet", "KÜÇÜKOĞLU");
        customer.PhoneNumber = new PhoneNumber("905551112233");

        Assert.Equal("905551112233", customer.PhoneNumber.ToString());
    }
}