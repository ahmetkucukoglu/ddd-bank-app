namespace BankDdd.Domain.BankMoney;

public record Currency
{
    public static Currency TL = new("TL", "₺");
    public static Currency USD = new("USD", "$");

    public Currency(string name, string symbol)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(symbol);

        Name = name;
        Symbol = symbol;
    }

    public string Name { get; }
    public string Symbol { get; }

    public override string ToString() => Symbol;

    public static implicit operator string(Currency currency) => currency.ToString();
}