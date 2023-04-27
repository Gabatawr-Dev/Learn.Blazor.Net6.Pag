using Learn.Blazor.Net7.Pag.Grpc.Product;
using Learn.Blazor.Net7.Pag.Models.Product;

namespace Learn.Blazor.Net7.Pag.Server.Services.Product;

public interface IProductService
{
    int MaxQuantityPerRequest { get; }

    Task<IEnumerable<ProductModel>> GetAsync(ProductGetReq request, CancellationToken token);
    IAsyncEnumerable<ProductModel> GetAsyncEnumerable(ProductGetReq request, CancellationToken token);
    Task<int> GetTotalQuantityAsync(CancellationToken token);
}