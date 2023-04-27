using Learn.Blazor.Net7.Pag.Models.Product;

namespace Learn.Blazor.Net7.Pag.Server.Services.Product;

public interface IProductService
{
    int MaxQuantityPerRequest { get; }

    Task<IEnumerable<ProductModel>> GetAsync(int quantity, int offset, CancellationToken token);
    IAsyncEnumerable<ProductModel> GetAsyncEnumerable(int quantity, int offset, CancellationToken token);
    Task<int> GetTotalQuantityAsync(CancellationToken token);
    Task<ProductModel?> GetByIdAsync(Guid id, CancellationToken token);
}