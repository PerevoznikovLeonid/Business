using Business.Core.Interfaces;

namespace Business.Core.Incomes;

/// <summary>
/// Класс для хранения информации об одном доходе
/// </summary>
public record Income: IEntity
{
    public Guid Id { get; init; }
    public decimal Amount { get; set; }
    public IncomeType Type { get; set; }
    public PayerType Payer { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public (int Year, int Month) YearMonth => (Date.Year, Date.Month);
    
    public Income(
        decimal amount,
        IncomeType type,
        PayerType payer,
        DateTime date,
        string description)
    {
        if (amount <= 0)
            throw new ArgumentException("Сумма дохода должна быть положительной.", nameof(amount));

        Id = Guid.CreateVersion7();
        Amount = amount;
        Type = type;
        Payer = payer;
        Date = date;
        Description = description;
    }
}