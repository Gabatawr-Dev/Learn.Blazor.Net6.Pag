using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Learn.Blazor.Net6.Pag.Grpc.Product;

namespace Learn.Blazor.Net6.Pag.Client.Components.Product;

public partial class ProductCollection
{
    private async Task StreamBtnCommand()
    {
        UpdateButton(_cancelButtonContext);
        var token = GetStreamToken();
        try
        {
            var stream = _grpcClient.GetAllStreamReq(new Empty());

            Collection.Clear();
            await foreach (var unit in stream.ResponseStream.ReadAllAsync(token))
            {
                Collection.Add(unit.MapToModel());
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
            UpdateButton(_streamButtonContext);
        }
    }

    private Task CancelBtnCommand()
    {
        _cancelButtonDelegate?.Invoke();
        return Task.CompletedTask;
    }
}