using Learn.Blazor.Net6.Pag.Models.Product;

namespace Learn.Blazor.Net6.Pag.Grpc.Product;

public static class ProductExtensions
{
    public static ProductModel MapToModel(this ProductDTO dto) => new ProductModel
    {
        Id = dto.Id,
        Title = dto.Title,
        Description = dto.Description,
        ImageUrl = dto.ImageUrl,
        Price = dto.Price,
    };

    public static ProductModel MapToModel(this ProductUnit unit) => new ProductModel
    {
        Id = Guid.Parse(unit.Id),
        Title = unit.Title,
        Description = unit.Description,
        ImageUrl = unit.ImageUrl,
        Price = unit.Price,
    };

    public static ProductUnit MapToUnit(this ProductModel model) => new ProductUnit
    {
        Id = model.Id.ToString(),
        Title = model.Title,
        Description = model.Description,
        ImageUrl = model.ImageUrl,
        Price = model.Price,
    };
}