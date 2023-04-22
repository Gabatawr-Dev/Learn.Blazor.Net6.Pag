using Learn.Blazor.Net6.Pag.Data.Repositories.Product;
using Learn.Blazor.Net6.Pag.Models.Product;

namespace Learn.Blazor.Net6.Pag.Server.Services.Product;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository) => 
        _repository = repository;

    public async Task<IEnumerable<ProductModel>> GetAllAsync(CancellationToken token)
    {
        var products = await _repository.GetAllAsync(token);
        var models = products
            .Select(dto => new ProductModel(dto))
            .ToList();

        if (models.Any() is false)
            models = await GetAllAsyncMoq();

        return models;
    }

    private static Task<List<ProductModel>> GetAllAsyncMoq()
    {
        return Task.FromResult(new List<ProductModel>
        {
            new ("The Great Gatsby",
                "The Great Gatsby is a 1925 novel written by American author F. Scott Fitzgerald that follows a cast of characters living in the fictional town of West Egg on prosperous Long Island in the summer of 1922.",
                9.99m,
                "https://picsum.photos/200/300"),
            new ("The Catcher in the Rye",
                "The Catcher in the Rye is a 1951 novel by J. D. Salinger. A controversial novel originally published for adults, it has since become popular with adolescent readers for its themes of teenage angst and alienation.",
                7.99m,
                "https://picsum.photos/200/300"),
            new ("The Grapes of Wrath",
                "The Grapes of Wrath is an American realist novel written by John Steinbeck and published in 1939. The book won the National Book Award and Pulitzer Prize for Fiction, and it was cited prominently when Steinbeck was awarded the Nobel Prize in 1962.",
                5.99m,
                "https://picsum.photos/200/300"),
        });
    }
}