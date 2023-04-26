using Learn.Blazor.Net6.Pag.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Learn.Blazor.Net6.Pag.Server.Data.Contexts;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<ProductEntity> Products { get; set; } = null!;
}