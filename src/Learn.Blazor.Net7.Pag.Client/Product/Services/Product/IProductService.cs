using Learn.Blazor.Net7.Pag.Grpc.Product;
using Learn.Blazor.Net7.Pag.Models.Product;

namespace Learn.Blazor.Net7.Pag.Client.Product.Services.Product;

public interface IProductService
{
    IAsyncEnumerable<ProductModel> GetAsync(int quantity, int offset = 0, CancellationToken token = default);
    IAsyncEnumerable<ProductModel> GetStreamAsync(int quantity = 0, int offset = 0, CancellationToken token = default);
    Task<ProductGetQuantityRes> GetQuantityAsync(bool includeTotal = false, CancellationToken token = default);
    Task<ProductModel?> GetByIdAsync(Guid id, CancellationToken token = default);
}