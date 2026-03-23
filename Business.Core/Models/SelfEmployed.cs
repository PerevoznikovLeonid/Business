using Business.Core.Analytics;
using Business.Core.Incomes;
using Business.Core.Models.Repositories;

namespace Business.Core.Models;

public class SelfEmployed
{
    private const decimal IndividualTaxRate = 0.04M;
    private const decimal LegalEntityTaxRate = 0.06M;
    private readonly IncomeJsonRepository _incomeRepo;
    
    public SelfEmployed(IncomeJsonRepository repository)
    {
        _incomeRepo = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public Income AddIncome(
        decimal amount,
        IncomeType incomeType,
        PayerType payerType,
        DateTime date,
        string description)
    {
        var entry = new Income(amount, incomeType, payerType, date, description);
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
}