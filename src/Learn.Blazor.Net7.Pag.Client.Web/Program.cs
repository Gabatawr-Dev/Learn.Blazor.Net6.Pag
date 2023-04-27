using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Learn.Blazor.Net7.Pag.Client.Web;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder
            .CreateDefault(args)
            .AddWasmInfrastructure();

        builder.Services.AddCommonInfrastructure();

        await builder.Build()
                     .RunAsync();
    }

    private static WebAssemblyHostBuilder AddWasmInfrastructure(this WebAssemblyHostBuilder builder)
    {
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(_ => new HttpClient
        {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
        });
        
        return builder;
    }
}