using Learn.Blazor.Net6.Pag.Models.Product;

namespace Learn.Blazor.Net6.Pag.Server.Services.Product;

public interface IProductService
{
    Task<IEnumerable<ProductModel>> GetAllAsync(CancellationToken token);
}