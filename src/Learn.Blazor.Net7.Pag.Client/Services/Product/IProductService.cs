using Learn.Blazor.Net7.Pag.Grpc.Product;
using Learn.Blazor.Net7.Pag.Models.Product;

namespace Learn.Blazor.Net7.Pag.Client.Services.Product;

public interface IProductService
{
    public IAsyncEnumerable<ProductModel> GetAsync(int quantity, int offset = 0, CancellationToken token = default);
    public IAsyncEnumerable<ProductModel> GetStreamAsync(int quantity = 0, int offset = 0, CancellationToken token = default);
    public Task<ProductGetQuantityRes> GetQuantityAsync(bool includeTotal = false, CancellationToken token = default);
}