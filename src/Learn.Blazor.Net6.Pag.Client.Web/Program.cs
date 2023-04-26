using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Learn.Blazor.Net6.Pag.Client.Services.Product;
using Learn.Blazor.Net6.Pag.Grpc.Product;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Learn.Blazor.Net6.Pag.Client.Web;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(_ => new HttpClient
        {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
        });

        builder.Services.AddScoped(_ => new ProductGrpcService
            .ProductGrpcServiceClient(GrpcChannel
                .ForAddress(builder.HostEnvironment.BaseAddress, new GrpcChannelOptions
                {
                    HttpHandler = new GrpcWebHandler(new HttpClientHandler())
                })));

        builder.Services.AddServices();

        await builder.Build()
                     .RunAsync();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        return services;
    }
}