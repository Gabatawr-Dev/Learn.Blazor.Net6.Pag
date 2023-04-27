using Grpc.Core;
using Learn.Blazor.Net7.Pag.Extensions;
using Learn.Blazor.Net7.Pag.Grpc.Product;
using Learn.Blazor.Net7.Pag.Models.Product;
using Learn.Blazor.Net7.Pag.Server.Services.Product;

namespace Learn.Blazor.Net7.Pag.Server.GrpcServices.Product;

public class ProductGrpcService : Grpc.Product.ProductGrpcService.ProductGrpcServiceBase
{
    private readonly IProductService _service;
    public ProductGrpcService(IProductService service) => _service = service;

    public override async Task<ProductGetRes> GetReq(ProductGetReq request, ServerCallContext context)
    {
        var models = request.Quantity != 0
            ? await _service.GetAsync(request.Quantity, request.Offset, context.CancellationToken)
            : Enumerable.Empty<ProductModel>();
        
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
        await foreach (var m in _service.GetAsyncEnumerable(request.Quantity, request.Offset,
                           context.CancellationToken))
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

    public override async Task<ProductGetByIdRes>GetByIdReq(ProductGetByIdReq request, ServerCallContext context)
    {
        var response = new ProductGetByIdRes();
        
        var id = Guid.TryParse(request.Id, out var guid) ? guid : Guid.Empty;
        if (id == Guid.Empty)
        {
            response.ErrorMessage = $"Invalid Id {{{request.Id}}}";
            return response;
        }

        var model = await _service.GetByIdAsync(id, context.CancellationToken);
        if (model is null)
        {
            response.ErrorMessage = $"Product {{{id}}} not found";
            return response;
        }
        
        response.Unit = model.MapToUnit();
        return response;
    }
}