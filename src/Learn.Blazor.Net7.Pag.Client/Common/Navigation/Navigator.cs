using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Learn.Blazor.Net7.Pag.Client.Common.Navigation;

public class Navigator : INavigator
{
    public IStateContainer StateContainer { get; }

    private readonly IHistoryManager _historyManager;
    private readonly NavigationManager _navigator;
    public Navigator(NavigationManager navigator, IHistoryManager historyManager, IStateContainer stateContainer)
    {
        _navigator = navigator;
        _historyManager = historyManager;
        StateContainer = stateContainer;
    }

    public INavigator SaveState<T>(T? stateObject) where T : class
    {
        if (stateObject != null)
            StateContainer.Add(stateObject);
        return this;
    }

    public void NavigateToBack()
    {
        var uri = _historyManager.GetPrevious();
        NavigateTo(uri, true);
    }

    #region NavigationManager


    public event EventHandler<LocationChangedEventArgs>? LocationChanged;
    public string BaseUri => _navigator.BaseUri;
    public string Uri => _navigator.Uri;
    public string? HistoryEntryState => _navigator.HistoryEntryState;
    public void NavigateTo(string uri, bool forceLoad)
    {
        _historyManager.Add(uri);
        _navigator.NavigateTo(uri, forceLoad);
    }

    public void NavigateTo(string uri, bool forceLoad = false, bool replace = false)
    {
        _historyManager.Add(uri);
        _navigator.NavigateTo(uri, forceLoad, replace);
    }

    public void NavigateTo(string uri, NavigationOptions options)
    {
        _historyManager.Add(uri);
        _navigator.NavigateTo(uri, options);
    }

    public Uri ToAbsoluteUri(string relativeUri) => 
        _navigator.ToAbsoluteUri(relativeUri);

    public string ToBaseRelativePath(string uri) => 
        _navigator.ToBaseRelativePath(uri);

    public IDisposable RegisterLocationChangingHandler(Func<LocationChangingContext, ValueTask> locationChangingHandler) =>
        _navigator.RegisterLocationChangingHandler(locationChangingHandler);

    #endregion NavigationManager
}