using Learn.Blazor.Net6.Pag.Data.Contexts;
using Learn.Blazor.Net6.Pag.Data.Repositories.Product;
using Learn.Blazor.Net6.Pag.Data.Secrets;
using Learn.Blazor.Net6.Pag.Server.GrpcServices.Product;
using Learn.Blazor.Net6.Pag.Server.Infrastructures.Filters;
using Learn.Blazor.Net6.Pag.Server.Services.Product;

namespace Learn.Blazor.Net6.Pag.Server;

public static class Program
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
    
    private static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IProductRepository, ProductRepository>();

        return builder;
    }

    private static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IProductService, ProductService>();
        
        return builder;
    }

    #region Infrastructure

    private static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlWithSecrets(builder.Configuration));

        builder.Services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());
        
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        builder.Services.AddGrpc();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    private static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
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
            endpoints.MapGrpcService<ProductGrpcService>().EnableGrpcWeb();
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("index.html");
        });

        return app;
    }

    #endregion Infrastructure
}