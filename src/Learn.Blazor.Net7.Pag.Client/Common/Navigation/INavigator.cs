using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Learn.Blazor.Net7.Pag.Client.Common.Navigation;

public interface INavigator
{
    IStateContainer StateContainer { get; }
    
    INavigator SaveState<T>(T? stateObject) where T : class;

    void NavigateToBack();

    #region NavigationManager

    /// <summary>
    /// An event that fires when the navigation location has changed.
    /// </summary>
    event EventHandler<LocationChangedEventArgs> LocationChanged;

    /// <summary>
    /// Gets or sets the current base URI. The <see cref="BaseUri" /> is always represented as an absolute URI in string form with trailing slash.
    /// Typically this corresponds to the 'href' attribute on the document's &lt;base&gt; element.
    /// </summary>
    /// <remarks>
    /// Setting <see cref="BaseUri" /> will not trigger the <see cref="LocationChanged" /> event.
    /// </remarks>
    string BaseUri { get; }

    /// <summary>
    /// Gets or sets the current URI. The <see cref="Uri" /> is always represented as an absolute URI in string form.
    /// </summary>
    /// <remarks>
    /// Setting <see cref="Uri" /> will not trigger the <see cref="LocationChanged" /> event.
    /// </remarks>
    string Uri { get; }

    /// <summary>
    /// Gets or sets the state associated with the current navigation.
    /// </summary>
    /// <remarks>
    /// Setting <see cref="HistoryEntryState" /> will not trigger the <see cref="LocationChanged" /> event.
    /// </remarks>
    string? HistoryEntryState { get; }

    /// <summary>
    /// Navigates to the specified URI.
    /// </summary>
    /// <param name="uri">The destination URI. This can be absolute, or relative to the base URI
    /// (as returned by <see cref="NavigationManager.BaseUri"/>).</param>
    /// <param name="forceLoad">If true, bypasses client-side routing and forces the browser to load the new page from the server, whether or not the URI would normally be handled by the client-side router.</param>
    void NavigateTo(string uri, bool forceLoad) // This overload is for binary back-compat with < 6.0
        ;

    /// <summary>
    /// Navigates to the specified URI.
    /// </summary>
    /// <param name="uri">The destination URI. This can be absolute, or relative to the base URI
    /// (as returned by <see cref="NavigationManager.BaseUri"/>).</param>
    /// <param name="forceLoad">If true, bypasses client-side routing and forces the browser to load the new page from the server, whether or not the URI would normally be handled by the client-side router.</param>
    /// <param name="replace">If true, replaces the current entry in the history stack. If false, appends the new entry to the history stack.</param>
    void NavigateTo(string uri, bool forceLoad = false, bool replace = false);

    /// <summary>
    /// Navigates to the specified URI.
    /// </summary>
    /// <param name="uri">The destination URI. This can be absolute, or relative to the base URI
    /// (as returned by <see cref="NavigationManager.BaseUri"/>).</param>
    /// <param name="options">Provides additional <see cref="NavigationOptions"/>.</param>
    void NavigateTo(string uri, NavigationOptions options);

    /// <summary>
    /// Converts a relative URI into an absolute one (by resolving it
    /// relative to the current absolute URI).
    /// </summary>
    /// <param name="relativeUri">The relative URI.</param>
    /// <returns>The absolute URI.</returns>
    Uri ToAbsoluteUri(string relativeUri);

    /// <summary>
    /// Given a base URI (e.g., one previously returned by <see cref="NavigationManager.BaseUri"/>),
    /// converts an absolute URI into one relative to the base URI prefix.
    /// </summary>
    /// <param name="uri">An absolute URI that is within the space of the base URI.</param>
    /// <returns>A relative URI path.</returns>
    string ToBaseRelativePath(string uri);

    /// <summary>
    /// Registers a handler to process incoming navigation events.
    /// </summary>
    /// <param name="locationChangingHandler">The handler to process incoming navigation events.</param>
    /// <returns>An <see cref="IDisposable"/> that can be disposed to unregister the location changing handler.</returns>
    IDisposable RegisterLocationChangingHandler(Func<LocationChangingContext, ValueTask> locationChangingHandler);

    #endregion NavigationManager
}