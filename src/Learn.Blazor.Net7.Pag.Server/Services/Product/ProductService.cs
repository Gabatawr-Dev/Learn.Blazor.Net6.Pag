using System.Runtime.CompilerServices;
using Learn.Blazor.Net7.Pag.Models.Product;
using Learn.Blazor.Net7.Pag.Server.Data.Extensions;
using Learn.Blazor.Net7.Pag.Server.Data.Repositories.Product;

namespace Learn.Blazor.Net7.Pag.Server.Services.Product;

public class ProductService : IProductService
{
    public int MaxQuantityPerRequest => _repository.MaxQuantityPerRequest;

    private readonly IProductRepository _repository;
    public ProductService(IProductRepository repository) =>
        _repository = repository;

    public async Task<IEnumerable<ProductModel>> GetAsync(int quantity, int offset, CancellationToken token)
    {
        var products = await _repository.GetAsync(quantity, offset, 
            token, isTracking: false);
        
        var models = products
            .Select(entity => entity.MapToModel())
            .ToList();
        
        return models;
    }

    public async IAsyncEnumerable<ProductModel> GetAsyncEnumerable(int quantity, int offset, [EnumeratorCancellation] CancellationToken token)
    {
        await foreach (var entity in _repository.GetAsyncEnumerable(quantity, offset,
                           isTracking: false).WithCancellation(token))
            yield return entity.MapToModel();
    }

    public async Task<int> GetTotalQuantityAsync(CancellationToken token) =>
        await _repository.GetCountAsync(token);

    public async Task<ProductModel?> GetByIdAsync(Guid id, CancellationToken token)
    {
        var product = await _repository.FindAsync(id, token);
        return product?.MapToModel();
    }
}