using Learn.Blazor.Net6.Pag.Data.Contexts;
using Learn.Blazor.Net6.Pag.Data.Secrets;
using Learn.Blazor.Net6.Pag.Server.Infrastructures.Filters;
using Learn.Blazor.Net6.Pag.Server.Infrastructures.Interceptors;
using Serilog;

namespace Learn.Blazor.Net6.Pag.Server;

public static partial class Program
{
    public static void Main(string[] args)
    {
        WebApplication.CreateBuilder(args)
            .AddInfrastructure()
            .AddRepositories()
            .AddServices()
            .Build()
                .UseInfrastructure()
                .Run();
    }

    private static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((hostContext, logConfig) => logConfig
            .ReadFrom.Configuration(hostContext.Configuration)
            .WriteTo.Console());

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlWithSecrets(builder.Configuration));

        builder.Services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());
        
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        builder.Services.AddGrpc(options =>
            options.Interceptors.Add<ExceptionInterceptor>());

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    private static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        if (app.Environment.IsDevelopment())
            app.UseWebAssemblyDebugging();
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        app.UseHttpsRedirection();

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();
        app.MapRazorPages();

        app.UseGrpcWeb();

        app.UseEndpoints(endpoints =>
        {
            endpoints
                .AddGrpcServices()
                .MapControllers();
            
            endpoints.MapFallbackToFile("index.html");
        });

        return app;
    }
}