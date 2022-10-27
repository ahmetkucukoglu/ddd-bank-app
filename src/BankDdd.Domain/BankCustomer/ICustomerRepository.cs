namespace BankDdd.Domain.BankCustomer;

public interface ICustomerRepository
{
    Customer GetCustomer(CustomerId customerId);
}