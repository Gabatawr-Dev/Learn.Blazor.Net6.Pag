using Microsoft.Extensions.Logging;

namespace Learn.Blazor.Net7.Pag.Client.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp
            .CreateBuilder()
            .AddMauiInfrastructure();

        builder.Services.AddCommonInfrastructure();

        return builder.Build();
    }

    private static MauiAppBuilder AddMauiInfrastructure(this MauiAppBuilder builder)
    {
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
        return builder;
    }
}