namespace Business.Core.Models.Interfaces;

/// <summary>
/// Интерфейс Json репозитория для доходов с CRUD-операциями
/// </summary>
public interface IIncomeJsonRepository<TEntity>: IDisposable where TEntity : IEntity
{
    public TEntity Create(TEntity entity);
    public TEntity? Read(Guid id);
    public IEnumerable<TEntity> ReadAll();
    public void Update(TEntity entity);
    public void Delete(Guid id);
}