namespace Business.Core.Models.Interfaces;

/// <summary>
/// Интерфейс любой сущности в контексте данной библиотеки
/// </summary>
public interface IEntity
{
    public Guid Id { get; init; }
}