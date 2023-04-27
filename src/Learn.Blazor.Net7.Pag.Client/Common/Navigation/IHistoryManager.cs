namespace Learn.Blazor.Net7.Pag.Client.Common.Navigation;

public interface IHistoryManager
{
    void Add(string uri);
    string GetPrevious();
    string GetOnce();
}