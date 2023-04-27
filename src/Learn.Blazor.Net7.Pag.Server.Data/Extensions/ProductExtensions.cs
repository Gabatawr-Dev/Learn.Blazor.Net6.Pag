using Learn.Blazor.Net7.Pag.Models.Product;
using Learn.Blazor.Net7.Pag.Server.Data.Entities;

namespace Learn.Blazor.Net7.Pag.Server.Data.Extensions;

public static class ProductExtensions
{
    public static ProductModel MapToModel(this ProductEntity entity) => new ProductModel
    {
        Id = entity.Id,
        Title = entity.Title,
        Description = entity.Description,
        ImageUrl = entity.ImageUrl,
        Price = entity.Price,
    };

    public static ProductEntity MapToEntity(this ProductModel model) => new ProductEntity
    {
        Id = model.Id,
        Title = model.Title,
        Description = model.Description,
        ImageUrl = model.ImageUrl,
        Price = model.Price,
    };
}