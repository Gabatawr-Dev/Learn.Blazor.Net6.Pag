using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Learn.Blazor.Net6.Pag.Grpc.Product;
using Learn.Blazor.Net6.Pag.Models.Product;
using Microsoft.AspNetCore.Components;

namespace Learn.Blazor.Net6.Pag.Client.Shared;

public partial class ProductList
{
    [Inject] private HttpClient _http { get; set; } = null!;
    [Inject] private ProductGrpcService.ProductGrpcServiceClient _grpcClient { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        var result = await _grpcClient.GetAllReqAsync(new Empty());
        Products = result.Units.Select(unit => unit.MapToModel()).ToList();
    }

    public List<ProductModel> Products { get; private set; } = new();
    private CancellationTokenSource? _cts;
    
    private bool _isStreamBtnDisabled;
    protected bool IsStreamBtnDisabled
    {
        get => _isStreamBtnDisabled;
        private set
        {
            _isStreamBtnDisabled = value;
            IsCancelBtnDisabled = value is false;
            StateHasChanged();
        }
    }

    public bool IsCancelBtnDisabled { get; private set; } = true;
    
    public async Task GetProductsAsync()
    {
        IsStreamBtnDisabled = true;
        
        _cts?.Cancel();
        _cts = new CancellationTokenSource();
        var token = _cts.Token;
        
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
            _cts.Cancel();
            _cts = null;
            IsStreamBtnDisabled = false;
        }
    }

    private async Task StreamBtnCommand() => await GetProductsAsync();
    private void CancelBtnCommand() => _cts?.Cancel();
}