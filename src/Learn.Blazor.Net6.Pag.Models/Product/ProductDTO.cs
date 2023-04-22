using System.ComponentModel.DataAnnotations;

namespace Learn.Blazor.Net6.Pag.Models.Product;

public class ProductDTO : IEntity<Guid>
{
    [Key]
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public string? ImageUrl { get; set; }
    
    [Required]
    [Range(0, int.MaxValue)]
    public decimal Price { get; set; }
}