using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learn.Blazor.Net6.Pag.Data.Entities;

public class ProductEntity : IEntity<Guid>
{
    [Key]
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
}