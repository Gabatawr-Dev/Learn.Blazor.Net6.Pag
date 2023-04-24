using Learn.Blazor.Net6.Pag.Models.Product;

namespace Learn.Blazor.Net6.Pag.Client.Components.Product;

public partial class ProductCollection
{
    private ButtonViewModel ButtonContext { get; set; } = null!;
    private List<ProductModel> Collection { get; set; } = null!;
}