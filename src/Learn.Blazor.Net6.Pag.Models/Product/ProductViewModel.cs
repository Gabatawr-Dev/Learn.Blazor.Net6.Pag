namespace Learn.Blazor.Net6.Pag.Models.Product;

public class ProductViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }

    public ProductViewModel() { }
    public ProductViewModel(ProductModel model)
    {
        Id = model.Id;
        Title = model.Title;
        Description = model.Description;
        ImageUrl = model.ImageUrl;
        Price = model.Price;
    }
}