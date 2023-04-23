using System.Linq.Expressions;
using Learn.Blazor.Net6.Pag.Models;

namespace Learn.Blazor.Net6.Pag.Data.Repositories;

public interface IRepository<T, TKey> where T : class, IEntity<TKey>
{
    Task<T?> FindAsync(object? id, CancellationToken token);

    Task<IEnumerable<T>> GetAllAsync(CancellationToken token, 
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string? include = null,
        bool isTracking = true);

    public IAsyncEnumerable<T> GetAsyncEnumerable(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string? include = null,
        bool isTracking = true);

    Task<T?> FirstOrDefaultAsync(CancellationToken token,
        Expression<Func<T, bool>>? filter = null,
        string? include = null,
        bool isTracking = true);

    Task AddAsync(T entity, CancellationToken token);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);

    void Update(T entity);
    Task UpdateUntrackedAsync(T untrackedEntity, CancellationToken token, params string[] properties);

    Task SaveChangesAsync(CancellationToken token);
}