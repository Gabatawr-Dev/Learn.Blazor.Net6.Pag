namespace Learn.Blazor.Net6.Pag.Server.Data.Entities;

public interface IEntity<TKey>
{
    TKey Id { get; set; }
}