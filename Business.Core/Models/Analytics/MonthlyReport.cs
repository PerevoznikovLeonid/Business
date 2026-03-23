using Business.Core.Models.Interfaces;

namespace Business.Core.Models.Analytics;

/// <summary>
/// Класс для хранения аналитики по доходам за месяц
/// </summary>
public record MonthlyReport: IEntity
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
}