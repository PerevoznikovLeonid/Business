using Business.Core.Models.Analytics;
using Business.Core.Models.Handlers;
using Business.Core.Models.Incomes;
using Business.Core.Models.Repositories;

namespace Business.Core.Models;

/// <summary>
/// Класс для упрощенной работы с методами доходов, аналитики и репозиториев
/// </summary>
public class SelfEmployed
{
    private readonly IncomeJsonRepository _incomeRepo;
    private readonly MonthlyReportHandler _monthlyReportHandler;
    
    public SelfEmployed(IncomeJsonRepository repository, MonthlyReportHandler monthlyReportRepo)
    {
        _incomeRepo = repository ?? throw new ArgumentNullException(nameof(repository));
        _monthlyReportHandler = monthlyReportRepo ?? throw new ArgumentNullException(nameof(monthlyReportRepo));
    }
    
    public Income AddIncome(
        decimal amount,
        PayerType payerType,
        DateTime date,
        string description)
    {
        var entry = new Income(amount, payerType, date, description);
        return _incomeRepo.Create(entry);
    }

    public Income? GetIncome(Guid id)
    {
        try
        {
            return _incomeRepo.Read(id);
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public IEnumerable<Income>? GetAllIncomes()
    {
        try
        {
            return _incomeRepo.ReadAll();
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public void UpdateIncome(Income income)
    {
        try
        {
            _incomeRepo.Update(income);
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void DeleteIncome(Guid id)
    {
        try
        {
            _incomeRepo.Delete(id);
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
    
    public MonthlyReport GetMonthlyReport(int year, int month)
    {
        return _monthlyReportHandler.Read(year, month);
    }

    public IEnumerable<MonthlyReport> GetAllMonthlyReports()
    {
        return _monthlyReportHandler.ReadAll();
    }

    public decimal TotalIncomeAllTime()
    {
        return _monthlyReportHandler.GetTotalIncome();
    }

    public MonthlyReport GetReportForPeriod(DateTime start, DateTime end)
    {
        return _monthlyReportHandler.GetReportForPeriod(start, end);
    }
}