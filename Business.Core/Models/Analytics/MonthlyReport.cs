using Business.Core.Interfaces;

namespace Business.Core.Analytics;

/// <summary>
/// Класс для хранения аналитики по доходам за месяц
/// </summary>
public class MonthlyReport: IEntity
{
    public Guid Id { get; init; }
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal IncomeIndividuals { get; set; }
    public decimal IncomeLegalEntities { get; set; }
    public decimal IncomeTotal { get; set; }
    public decimal TaxIndividuals { get; set; }
    public decimal TaxLegalEntities { get; set; }
    public decimal TaxTotal { get; set; }
    public decimal Profit { get; set; }
    
    public MonthlyReport(
        int year, 
        int month, 
        decimal incomeIndividuals, 
        decimal incomeLegalEntities,
        decimal incomeTotal,
        decimal taxIndividuals, 
        decimal taxLegalEntities,
        decimal taxTotal,
        decimal profit)
    {
        Id = Guid.CreateVersion7();
        Year = year;
        Month = month;
        IncomeIndividuals = incomeIndividuals;
        IncomeLegalEntities = incomeLegalEntities;
        IncomeTotal = incomeTotal;
        TaxIndividuals = taxIndividuals;
        TaxLegalEntities = taxLegalEntities;
        TaxTotal = taxTotal;
        Profit = profit;
    }

    public override string ToString()
    {
        return $"Месяц: {Month:D2}.{Year}\n" +
               $"Общий доход: {IncomeTotal:C2}\n" +
               $"Доход от физ. лиц: {IncomeIndividuals:C2}\n" +
               $"Доход от юр. лиц: {IncomeLegalEntities:C2}\n" +
               $"Налог с физ. лиц (4%): {TaxIndividuals:C2}\n" +
               $"Налог с юр. лиц (6%): {TaxLegalEntities:C2}\n" +
               $"Общий налог: {TaxTotal:C2}\n" +
               $"Прибыль: {Profit:C2}\n";
    }
}