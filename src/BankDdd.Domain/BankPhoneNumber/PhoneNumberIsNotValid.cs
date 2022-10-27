namespace BankDdd.Domain.BankPhoneNumber;

public class PhoneNumberIsNotValid : Exception
{
    public PhoneNumberIsNotValid() : base("The phone number isn't valid.")
    {
    }
}