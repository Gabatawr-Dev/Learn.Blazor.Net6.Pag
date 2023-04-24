using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Learn.Blazor.Net6.Pag.Grpc.Product;

namespace Learn.Blazor.Net6.Pag.Client.Components.Product;

public partial class ProductList
{
    protected async Task StreamBtnCommand()
    {
        IsStreamBtnDisabled = true;
        var token = GetStreamToken();
        try
        {
            var stream = _grpcClient.GetAllStreamReq(new Empty());

            Products.Clear();
            await foreach (var unit in stream.ResponseStream.ReadAllAsync(token))
            {
                Products.Add(unit.MapToModel());
                StateHasChanged();
                await Task.Delay(1000, token); // test
            }
        }
        catch (RpcException rpcException)
        {
            if (rpcException.Status.StatusCode is StatusCode.Cancelled)
                Console.WriteLine(rpcException.Status.Detail);
            else throw;
        }
        finally
        {
            IsStreamBtnDisabled = false;
        }
    }

    protected void CancelBtnCommand() => _cancelBtnDelegate?.Invoke();
}