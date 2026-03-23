namespace Business.Core.Interfaces;

/// <summary>
/// Интерфейс Json репозитория для выполнения CRUD-операций
/// </summary>
public interface IJsonRepository<TEntity>: IDisposable where TEntity : IEntity
{
    public TEntity Create(TEntity entity);
    public TEntity? Read(Guid id);
    public IEnumerable<TEntity> ReadAll();
    public void Update(TEntity entity);
    public void Delete(Guid id);
}