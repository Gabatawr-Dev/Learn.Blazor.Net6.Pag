using Learn.Blazor.Net6.Pag.Data.Contexts;
using Learn.Blazor.Net6.Pag.Models.Configuration;
using Learn.Blazor.Net6.Pag.Models.Product;
using Microsoft.Extensions.Configuration;

namespace Learn.Blazor.Net6.Pag.Data.Repositories.Product;

public class ProductRepository : Repository<ProductDTO, Guid>, IProductRepository
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