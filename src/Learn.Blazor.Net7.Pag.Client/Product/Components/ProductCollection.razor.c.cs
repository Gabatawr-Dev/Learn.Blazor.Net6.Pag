using Grpc.Core;
using Learn.Blazor.Net7.Pag.Models.Product;

namespace Learn.Blazor.Net7.Pag.Client.Product.Components;

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
        var pages = _service
            .GetAsync(QUANTITY_PER_PAGE, _previousPage * QUANTITY_PER_PAGE, token);

        await foreach (var model in pages.WithCancellation(token))
            Collection.Add(model); // TODO;

        Page++;
    });

    private async Task NextPageLoadingCommand() => await Loading(async () =>
    {
        var stream = _service
            .GetStreamAsync(QUANTITY_PER_PAGE - _lastLoadedQuantity,
                _previousPage * QUANTITY_PER_PAGE + _lastLoadedQuantity);

        var token = GetToken();
        await foreach (var model in stream.WithCancellation(token))
        {
            Collection.Add(model);
            StateHasChanged();
            _lastLoadedQuantity++;
        }

        if (_lastLoadedQuantity == QUANTITY_PER_PAGE)
        {
            Page++;
            _lastLoadedQuantity = 0;
        }
    });

    private Task CancelBtnCommand()
    {
        _cancelButtonDelegate?.Invoke();
        return Task.CompletedTask;
    }

    private void OpenDetails(ProductModel model)
    {
        _navigator.SaveState(model);
        _navigator.NavigateTo($"/product/{model.Id}");
    }
}