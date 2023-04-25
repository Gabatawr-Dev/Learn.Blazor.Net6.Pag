using Learn.Blazor.Net6.Pag.Models.Product;

namespace Learn.Blazor.Net6.Pag.Data.Repositories.Product;

public interface IProductRepository : IRepository<ProductDTO, Guid>
{
}
