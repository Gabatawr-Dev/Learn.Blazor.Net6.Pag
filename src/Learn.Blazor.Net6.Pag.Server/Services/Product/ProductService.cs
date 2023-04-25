using System.Runtime.CompilerServices;
using Learn.Blazor.Net6.Pag.Data.Repositories.Product;
using Learn.Blazor.Net6.Pag.Grpc.Product;
using Learn.Blazor.Net6.Pag.Models.Product;

namespace Learn.Blazor.Net6.Pag.Server.Services.Product;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository) =>
        _repository = repository;

    public async Task<IEnumerable<ProductModel>> GetAllAsync(CancellationToken token)
    {
        var products = await _repository.GetAllAsync(token, isTracking: false);
        var models = products
            .Select(dto => dto.MapToModel())
            .ToList();
        return models;
    }

    public async IAsyncEnumerable<ProductModel> GetAsyncEnumerable([EnumeratorCancellation] CancellationToken token)
    {
        await foreach (var dto in _repository.GetAsyncEnumerable(isTracking: false)
                           .WithCancellation(token))
            yield return dto.MapToModel();
    }
}