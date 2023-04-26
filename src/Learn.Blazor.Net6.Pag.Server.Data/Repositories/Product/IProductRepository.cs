using Learn.Blazor.Net6.Pag.Server.Data.Entities;

namespace Learn.Blazor.Net6.Pag.Server.Data.Repositories.Product;

public interface IProductRepository : IRepository<ProductEntity, Guid>
{
}
