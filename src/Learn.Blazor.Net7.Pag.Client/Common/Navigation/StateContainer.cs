using System.Collections.Concurrent;

namespace Learn.Blazor.Net7.Pag.Client.Common.Navigation;

public class StateContainer : IStateContainer
{
    private readonly ConcurrentDictionary<Type, object> _states = new();

    public void Add<T>(T obj) where T : class =>
        _states.AddOrUpdate(typeof(T), _ => obj, (_, _) => obj);

    public T? Get<T>() where T : class =>
        _states.TryGetValue(typeof(T), out var obj) ? (T)obj : null;

    public T? GetOnce<T>() where T : class
    {
        if (_states.TryRemove(typeof(T), out var obj) && obj is T state)
            return state;
        return null;
    }
}