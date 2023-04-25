using Grpc.Core;
using Learn.Blazor.Net6.Pag.Grpc.Product;
using Learn.Blazor.Net6.Pag.Server.Services.Product;

namespace Learn.Blazor.Net6.Pag.Server.GrpcServices.Product;

public class ProductGrpcService : Grpc.Product.ProductGrpcService.ProductGrpcServiceBase
{
    private readonly IProductService _service;
    public ProductGrpcService(IProductService service) => _service = service;

    public override async Task<ProductGetRes> GetReq(ProductGetReq request, ServerCallContext context)
    {
        var models = await _service.GetAsync(request, context.CancellationToken);
        var response = new ProductGetRes
        {
            Units = { models.Select(m => m.MapToUnit()) }
        };
        if (request.Quantity > _service.MaxQuantityPerRequest)
            response.QuantityRes = new ProductGetQuantityRes { MaxPerRequest = _service.MaxQuantityPerRequest };
        return response;
    }

    public override async Task GetStreamReq(ProductGetReq request, IServerStreamWriter<ProductUnit> responseStream,
        ServerCallContext context)
    {
        await foreach (var m in _service.GetAsyncEnumerable(request, context.CancellationToken))
            await responseStream.WriteAsync(m.MapToUnit());
    }

    public override async Task<ProductGetQuantityRes> GetQuantityReq(ProductGetQuantityReq request, ServerCallContext context)
    {
        var response = new ProductGetQuantityRes
        {
            MaxPerRequest = _service.MaxQuantityPerRequest,
        };
        if (request is { HasIncludeTotal: true, IncludeTotal: true })
            response.TotalQuantity = await _service.GetTotalQuantityAsync(context.CancellationToken);
        return response;
    }
}