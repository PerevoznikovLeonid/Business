using Business.Core.Models.Incomes;
using Business.Core.Models.Interfaces;
using Business.Core.Models.Tools;

namespace Business.Core.Models.Repositories;

/// <summary>
/// Класс Json репозитория для доходов
/// </summary>
public class IncomeJsonRepository : IIncomeJsonRepository<Income>
{
    private readonly string _filePath;
    private readonly List<Income> _incomes;
    private bool _disposed;
    
    public IncomeJsonRepository(string filePath = "incomes.json")
    {
        _filePath = filePath;
        _incomes = StorageManager.ReadFromFile<Income>(filePath).ToList();
    }

    public Income Create(Income entity)
    {
        _incomes.Add(entity);
        StorageManager.WriteToFile(_incomes, _filePath);
        return entity;
    }

    public Income Read(Guid id)
    {
        return _incomes.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException("Income not found");
    }

    public IEnumerable<Income> ReadAll()
    {
        return _incomes.ToList();
    }

    public IEnumerable<Income> ReadByDateRange(DateTime startDate, DateTime endDate)
    {
        return _incomes.Where(i => i.Date >= startDate && i.Date <= endDate).ToList();
    }

    public void Update(Income entity)
    {
        var toUpdate = Read(entity.Id);
        if (toUpdate == null)
            throw new KeyNotFoundException("Income not found");
        
        toUpdate.Amount = entity.Amount;
        toUpdate.Payer = entity.Payer;
        toUpdate.Date = entity.Date;
        toUpdate.Description = entity.Description;
    }

    public void Delete(Guid id)
    {
        var toDelete = Read(id);
        if (toDelete == null)
            throw new KeyNotFoundException("Income not found");
        
        _incomes.Remove(toDelete);
        
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        StorageManager.WriteToFile(_incomes, _filePath);
        _disposed = true;
    }
}