namespace Learn.Blazor.Net6.Pag.Data.Entities;

public interface IEntity<TKey>
{
    TKey Id { get; set; }
}