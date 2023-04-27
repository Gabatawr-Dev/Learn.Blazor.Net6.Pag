using Grpc.Core;

namespace Learn.Blazor.Net7.Pag.Client.Components.Product;

public partial class ProductCollection
{
    private async Task Loading(Func<Task> command)
    {
        UpdateButton(_cancelButtonContext);
        try
        {
            await command.Invoke();
        }
        catch (RpcException rpcException)
        {
            if (rpcException.Status.StatusCode is StatusCode.Cancelled)
                Console.WriteLine(rpcException.Status.Detail);
            else throw;
        }
        finally
        {
            UpdateButton(_streamButtonContext);
        }
    }

    private async Task PageLoadingCommand() => await Loading(async () =>
    {
        var token = GetToken();
        await foreach (var model in _service
                           .GetAsync(QUANTITY_PER_PAGE, _currentPage * QUANTITY_PER_PAGE, token))
        {
            Collection.Add(model); // TODO;
        }

        _currentPage++;
    });

    private async Task NextPageLoadingCommand() => await Loading(async () =>
    {
        var stream = _service
            .GetStreamAsync(QUANTITY_PER_PAGE - _lastLoadedQuantity,
                _currentPage * QUANTITY_PER_PAGE + _lastLoadedQuantity);

        var token = GetToken();
        await foreach (var model in stream.WithCancellation(token))
        {
            Collection.Add(model);
            StateHasChanged();
            _lastLoadedQuantity++;

            await Task.Delay(1000, token); // TODO;
        }

        if (_lastLoadedQuantity == QUANTITY_PER_PAGE)
        {
            _currentPage++;
            _lastLoadedQuantity = 0;
        }
    });

    private Task CancelBtnCommand()
    {
        _cancelButtonDelegate?.Invoke();
        return Task.CompletedTask;
    }
}