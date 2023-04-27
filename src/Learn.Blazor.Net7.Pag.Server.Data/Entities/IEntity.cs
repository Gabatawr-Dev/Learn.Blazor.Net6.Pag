namespace Learn.Blazor.Net7.Pag.Server.Data.Entities;

public interface IEntity<TKey>
{
    TKey Id { get; set; }
}