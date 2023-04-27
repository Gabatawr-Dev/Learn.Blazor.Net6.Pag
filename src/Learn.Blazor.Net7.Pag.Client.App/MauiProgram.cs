using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Learn.Blazor.Net7.Pag.Client.Services.Product;
using Learn.Blazor.Net7.Pag.Grpc.Product;
using Microsoft.Extensions.Logging;

namespace Learn.Blazor.Net7.Pag.Client.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });
            
        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif
        
        builder.Services.AddScoped(_ => new ProductGrpcService
            .ProductGrpcServiceClient(GrpcChannel
                .ForAddress("https://localhost:7272", new GrpcChannelOptions
                {
                    HttpHandler = new GrpcWebHandler(new HttpClientHandler())
                })));

        builder.Services.AddServices();

        return builder.Build();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        return services;
    }
}