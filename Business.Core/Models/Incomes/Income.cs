using Business.Core.Models.Interfaces;

namespace Business.Core.Models.Incomes;

/// <summary>
/// Класс для хранения информации об одном доходе
/// </summary>
public record Income: IEntity
{
    public Guid Id { get; init; }
    public decimal Amount { get; set; }
    public PayerType Payer { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    
    public Income(
        decimal amount,
        PayerType payer,
        DateTime date,
        string description)
    {
        if (amount <= 0)
            throw new ArgumentException("Сумма дохода должна быть положительной.", nameof(amount));

        Id = Guid.CreateVersion7();
        Amount = amount;
        Payer = payer;
        Date = date;
        Description = description;
    }
}