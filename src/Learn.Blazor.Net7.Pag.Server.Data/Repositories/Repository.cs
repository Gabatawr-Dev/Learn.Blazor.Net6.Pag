using System.Linq.Expressions;
using Learn.Blazor.Net7.Pag.Server.Data.Contexts;
using Learn.Blazor.Net7.Pag.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Learn.Blazor.Net7.Pag.Server.Data.Repositories;

public abstract class Repository<T, TKey> : IRepository<T, TKey> where T : class, IEntity<TKey>
{
    public int MaxQuantityPerRequest { get; protected set; } = int.MaxValue;

    protected readonly ApplicationDbContext Context;
    protected readonly DbSet<T> Set;

    protected Repository(ApplicationDbContext context)
    {
        Context = context;
        Set = Context.Set<T>();
    }

    public async Task<T?> FindAsync(object? id, CancellationToken token) =>
        await Set.FindAsync(id, token);

    public async Task<IEnumerable<T>> GetAsync(int quantity, int offset, CancellationToken token,
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string? include = null,
        bool isTracking = true)
    {
        var query = GetQuery(filter, orderBy, include, isTracking)
            .Skip(offset);
        
        if (quantity > 0)
            query = query.Take(quantity > MaxQuantityPerRequest ? MaxQuantityPerRequest : quantity);
        else return Enumerable.Empty<T>();
        
        return await query.ToListAsync(token);
    }

    public IAsyncEnumerable<T> GetAsyncEnumerable(int quantity = 0, int offset = 0, 
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string? include = null,
        bool isTracking = true)
    {
        var query = GetQuery(filter, orderBy, include, isTracking)
            .Skip(offset);
        
        if (quantity > 0)
            query = query.Take(quantity);
        
        return query.AsAsyncEnumerable();
    }

    public async Task<T?> FirstOrDefaultAsync(CancellationToken token,
        Expression<Func<T, bool>>? filter = null,
        string? include = null,
        bool isTracking = true)
    {
        return await GetQuery(filter, null, include, isTracking)
            .FirstOrDefaultAsync(token);
    }

    private IQueryable<T> GetQuery(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string? include = null,
        bool isTracking = true)
    {
        IQueryable<T> query = Set;

        if (filter is not null)
            query = query.Where(filter);
        
        if (string.IsNullOrWhiteSpace(include) is false)
            query = include.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, property) => current.Include(property.Trim(' ')));
        
        if (isTracking is false)
            query = query.AsNoTracking();
        
        if (orderBy is not null)
            query = orderBy(query);

        return query;
    }

    public async Task AddAsync(T entity, CancellationToken token) =>
        await Set.AddAsync(entity, token);

    public void Remove(T entity) => Set.Remove(entity);
    public void RemoveRange(IEnumerable<T> entities) => Set.RemoveRange(entities);

    public void Update(T entity) => Set.Update(entity);

    /// <exception cref="NullReferenceException"></exception>
    public async Task UpdateUntrackedAsync(T untrackedEntity, CancellationToken token, params string[] properties)
    {
        var trackedEntity = await FindAsync(untrackedEntity.Id, token);
        if (trackedEntity is null)
            throw new NullReferenceException($"Not found entity by id: {untrackedEntity.Id} in database");

        foreach (var property in properties)
        {
            token.ThrowIfCancellationRequested();
            UpdateProperty(trackedEntity, untrackedEntity, property);
        }

        token.ThrowIfCancellationRequested();
        Set.Update(trackedEntity);
    }

    private static void UpdateProperty(T setEntity, T getEntity, string propertyName)
    {
        var type = typeof(T);
        var propertyInfo = type.GetProperty(propertyName);
        propertyInfo?.SetValue(setEntity, propertyInfo.GetValue(getEntity));
    }

    public async Task SaveChangesAsync(CancellationToken token) =>
        await Context.SaveChangesAsync(token);

    public async Task<int> GetCountAsync(CancellationToken token) => 
       await Set.CountAsync(token);
}
