namespace Learn.Blazor.Net6.Pag.Models;

public interface IEntity<TKey>
{
    TKey Id { get; set; }
}