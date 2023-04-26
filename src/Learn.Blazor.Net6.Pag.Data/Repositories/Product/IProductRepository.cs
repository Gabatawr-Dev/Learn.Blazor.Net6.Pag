using Learn.Blazor.Net6.Pag.Data.Entities;

namespace Learn.Blazor.Net6.Pag.Data.Repositories.Product;

public interface IProductRepository : IRepository<ProductEntity, Guid>
{
}
