namespace Learn.Blazor.Net6.Pag.Server.Models.Configuration;

public class SqlSecrets
{
    public string Server { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string GetConnectionString(string template) =>
        template.Replace($"{{{nameof(Server)}}}", Server)
                .Replace($"{{{nameof(User)}}}", User)
                .Replace($"{{{nameof(Password)}}}", Password);
}