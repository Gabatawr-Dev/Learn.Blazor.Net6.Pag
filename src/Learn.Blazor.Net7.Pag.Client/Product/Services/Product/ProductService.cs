using System.Runtime.CompilerServices;
using Learn.Blazor.Net7.Pag.Extensions;
using Learn.Blazor.Net7.Pag.Grpc.Product;
using Learn.Blazor.Net7.Pag.Models.Product;

namespace Learn.Blazor.Net7.Pag.Client.Product.Services.Product;

public class ProductService : IProductService
{
    private readonly ProductGrpcService.ProductGrpcServiceClient _client;
    public ProductService(ProductGrpcService.ProductGrpcServiceClient client)
    {
        _client = client;
    }

    public async IAsyncEnumerable<ProductModel> GetAsync(int quantity, int offset = 0, [EnumeratorCancellation] CancellationToken token = default)
    {
        var request = new ProductGetReq { Quantity = quantity };
        if (offset > 0) request.Offset = offset;

        var response = await _client.GetReqAsync(request, cancellationToken: token);

        if (response.Units.Any() is false) yield break;
        foreach (var unit in response.Units) yield return unit.MapToModel();
        if (response.QuantityRes == null || quantity <= response.QuantityRes.MaxPerRequest)
            yield break;

        var limit = response.QuantityRes.MaxPerRequest;
        for (var i = response.Units.Count; i < quantity; i += response.Units.Count)
        {
            var left = quantity - i;
            var q = left < limit ? left : limit;
            response = await _client.GetReqAsync(new ProductGetReq
            {
                Quantity = q,
                Offset = i + offset
            }, cancellationToken: token);

            if (response.Units.Any() is false) yield break;
            foreach (var unit in response.Units) yield return unit.MapToModel();
            if (response.Units.Count < limit)
                yield break;
        }
    }

    public async IAsyncEnumerable<ProductModel> GetStreamAsync(int quantity = 0, int offset = 0, [EnumeratorCancellation] CancellationToken token = default)
    {
        var request = new ProductGetReq { Quantity = quantity };
        if (offset > 0) request.Offset = offset;

        var stream = _client.GetStreamReq(request, cancellationToken: token)
            .ResponseStream;
        while (await stream.MoveNext(token))
            yield return stream.Current.MapToModel();
    }

    public Task<ProductGetQuantityRes> GetQuantityAsync(bool includeTotal = false, CancellationToken token = default)
    {
        var request = new ProductGetQuantityReq();
        if (includeTotal)
            request.IncludeTotal = true;
        return _client.GetQuantityReqAsync(request, cancellationToken: token)
            .ResponseAsync;
    }

    public async Task<ProductModel?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var request = new ProductGetByIdReq { Id = id.ToString() };
        var response = await _client.GetByIdReqAsync(request, cancellationToken: token);
        return response.Unit?.MapToModel();
    }
}