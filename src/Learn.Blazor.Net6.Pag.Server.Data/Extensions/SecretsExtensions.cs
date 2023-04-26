using Learn.Blazor.Net6.Pag.Server.Models.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Learn.Blazor.Net6.Pag.Server.Data.Extensions;

public static class SecretsExtensions
{
    public static DbContextOptionsBuilder UseSqlWithSecrets(this DbContextOptionsBuilder options, IConfiguration configuration) =>
        options.UseSqlServer(configuration
            .GetSection(nameof(SqlSecrets)).Get<SqlSecrets>()
            .GetConnectionString(configuration.GetConnectionString("DefaultConnection")));
}