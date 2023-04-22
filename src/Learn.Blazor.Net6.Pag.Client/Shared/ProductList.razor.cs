using System.Net.Http.Json;
using Learn.Blazor.Net6.Pag.Models.Product;
using Microsoft.AspNetCore.Components;

namespace Learn.Blazor.Net6.Pag.Client.Shared;

public partial class ProductList
{
    [Inject]
    protected HttpClient Http { get; set; } = null!;

    public IEnumerable<ProductDTO>? Products { get; private set; }

    protected override async Task OnInitializedAsync() => 
        Products = await Http.GetFromJsonAsync<IEnumerable<ProductDTO>>("api/products");
}