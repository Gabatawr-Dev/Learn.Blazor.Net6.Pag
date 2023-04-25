using Learn.Blazor.Net6.Pag.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace Learn.Blazor.Net6.Pag.Data.Contexts;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<ProductDTO> Products { get; set; } = null!;
}