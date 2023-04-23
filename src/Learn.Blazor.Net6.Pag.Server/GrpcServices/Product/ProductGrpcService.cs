using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Learn.Blazor.Net6.Pag.Grpc.Product;
using Learn.Blazor.Net6.Pag.Server.Services.Product;

namespace Learn.Blazor.Net6.Pag.Server.GrpcServices.Product;

public class ProductGrpcService : Grpc.Product.ProductGrpcService.ProductGrpcServiceBase
{
    private readonly IProductService _service;
    public ProductGrpcService(IProductService service) => _service = service;

    public override async Task<ProductGetAllRes> GetAllReq(Empty request, ServerCallContext context)
    {
        var models = await _service.GetAllAsync(context.CancellationToken);
        return new ProductGetAllRes
        {
            Units = 
            {
                models.Select(m => new ProductUnit
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    Description = m.Description,
                    ImageUrl = m.ImageUrl,
                    Price = m.Price
                })
            }
        };
    }

    public override async Task GetAllStreamReq(Empty request, IServerStreamWriter<ProductUnit> responseStream, 
        ServerCallContext context)
    {
        try
        {
            await foreach (var m in _service.GetAsyncEnumerable(context.CancellationToken))
                await responseStream.WriteAsync(m.MapToUnit());
        }
        catch (OperationCanceledException)
        {
            throw new RpcException(new Status(StatusCode.Cancelled, "The operation was cancelled by Client"));
        }
    }
}