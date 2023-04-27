using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Learn.Blazor.Net7.Pag.Client.Common.Navigation;
using Learn.Blazor.Net7.Pag.Client.Product.Services.Product;
using Learn.Blazor.Net7.Pag.Grpc.Product;
using Microsoft.Extensions.DependencyInjection;

namespace Learn.Blazor.Net7.Pag.Client;

public static class Extensions
{
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        return services;
    }

    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(_ => new ProductGrpcService
            .ProductGrpcServiceClient(GrpcChannel
                .ForAddress("https://localhost:7272", new GrpcChannelOptions
                {
                    HttpHandler = new GrpcWebHandler(new HttpClientHandler())
                })));
        
        services.AddSingleton<IHistoryManager, HistoryManager>();
        services.AddSingleton<IStateContainer, StateContainer>();
        services.AddScoped<INavigator, Navigator>();

        services.AddServices();

        return services;
    }
}