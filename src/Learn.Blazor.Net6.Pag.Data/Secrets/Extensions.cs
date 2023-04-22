using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Learn.Blazor.Net6.Pag.Data.Secrets;

public static class Extensions
{
    public static DbContextOptionsBuilder UseSqlWithSecrets(this DbContextOptionsBuilder options, IConfiguration configuration) =>
        options.UseSqlServer(configuration
            .GetSection(nameof(SqlSecrets)).Get<SqlSecrets>()
            .GetConnectionString(configuration.GetConnectionString("DefaultConnection")));
}