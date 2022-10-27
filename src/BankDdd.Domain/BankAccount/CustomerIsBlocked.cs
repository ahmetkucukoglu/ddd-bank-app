namespace BankDdd.Domain.BankAccount;

public class CustomerIsBlocked : Exception
{
    public CustomerIsBlocked() : base("The customer is blocked.")
    {
    }
}