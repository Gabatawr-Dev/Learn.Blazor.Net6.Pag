using Google.Protobuf.WellKnownTypes;
using Learn.Blazor.Net6.Pag.Grpc.Product;
using Learn.Blazor.Net6.Pag.Models.Product;
using Microsoft.AspNetCore.Components;

namespace Learn.Blazor.Net6.Pag.Client.Components.Product;

public sealed partial class ProductList
{
    [Inject] private ProductGrpcService.ProductGrpcServiceClient _grpcClient { get; set; } = null!;

    protected List<ProductModel> Products { get; set; } = new();

    private Action? _cancelBtnDelegate;

    protected override async Task OnInitializedAsync()
    {
        var result = await _grpcClient.GetAllReqAsync(new Empty());
        Products = result.Units.Select(unit => unit.MapToModel()).ToList();
    }

    private CancellationToken GetStreamToken()
    {
        var cts = new CancellationTokenSource();
        _cancelBtnDelegate = () => cts.Cancel();
        return cts.Token;
    }
}