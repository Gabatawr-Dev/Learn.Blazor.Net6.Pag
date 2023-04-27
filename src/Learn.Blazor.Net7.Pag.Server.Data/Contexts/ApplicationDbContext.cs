using Learn.Blazor.Net7.Pag.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Learn.Blazor.Net7.Pag.Server.Data.Contexts;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<ProductEntity> Products { get; set; } = null!;
}