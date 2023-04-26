using Learn.Blazor.Net6.Pag.Server.Data.Contexts;
using Learn.Blazor.Net6.Pag.Server.Data.Entities;
using Learn.Blazor.Net6.Pag.Server.Models.Configuration;
using Microsoft.Extensions.Configuration;

namespace Learn.Blazor.Net6.Pag.Server.Data.Repositories.Product;

public class ProductRepository : Repository<ProductEntity, Guid>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context, IConfiguration configuration)
        : base(context)
    {
        MaxQuantityPerRequest = configuration
            .GetSection(nameof(QuantityPerRequest))
            .Get<QuantityPerRequest>()
            ?.Product ?? MaxQuantityPerRequest;
    }
}