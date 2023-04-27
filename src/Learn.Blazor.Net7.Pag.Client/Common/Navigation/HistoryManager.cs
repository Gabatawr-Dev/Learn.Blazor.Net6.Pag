using System.Collections.Concurrent;

namespace Learn.Blazor.Net7.Pag.Client.Common.Navigation;

public class HistoryManager : IHistoryManager
{
    private const string DEFAULT_URI = "/";
    private readonly ConcurrentStack<string> _history = new();
    
    public HistoryManager() => Add(DEFAULT_URI);
    
    public void Add(string uri) =>
        _history.Push(uri);

    public string GetPrevious() => _history.Count > 1
        ? _history.ElementAt(_history.Count - 2)
        : _history.Single();

    public string GetOnce() => _history.Count > 1 
        ? _history.TryPop(out var result) 
            ? result : DEFAULT_URI 
        : DEFAULT_URI;
}