using System.Linq.Expressions;
using Learn.Blazor.Net7.Pag.Server.Data.Entities;

namespace Learn.Blazor.Net7.Pag.Server.Data.Repositories;

public interface IRepository<T, TKey> where T : class, IEntity<TKey>
{
    int MaxQuantityPerRequest { get; }
    
    Task<T?> FindAsync(object? id, CancellationToken token);

    Task<IEnumerable<T>> GetAsync(int quantity, int offset, CancellationToken token,
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string? include = null,
        bool isTracking = true);

    public IAsyncEnumerable<T> GetAsyncEnumerable(int quantity = 0, int offset = 0,
        Expression<Func<T, bool>>? filter = null,
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

    Task<int> GetCountAsync(CancellationToken token);
}