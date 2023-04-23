using Google.Protobuf.WellKnownTypes;
using Learn.Blazor.Net6.Pag.Grpc.Product;
using Learn.Blazor.Net6.Pag.Models.Product;
using Microsoft.AspNetCore.Components;

namespace Learn.Blazor.Net6.Pag.Client.Shared;

public partial class ProductList
{
    //[Inject]
    //protected HttpClient Http { get; set; } = null!;

    [Inject]
    private ProductGrpcService.ProductGrpcServiceClient _grpcClient { get; set; } = null!;

    public IEnumerable<ProductModel>? Products { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        //Products = await Http.GetFromJsonAsync<IEnumerable<ProductDTO>>("api/products");
        var result = await _grpcClient.GetAllReqAsync(new Empty());
        Products = result.Units.Select(unit => unit.MapToModel());
    }
}