namespace Learn.Blazor.Net7.Pag.Client.Common.Navigation;

public interface IStateContainer
{
    void Add<T>(T obj) where T : class;
    T? Get<T>() where T : class;
    T? GetOnce<T>() where T : class;
}