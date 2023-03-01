namespace API.Dtos;

public class ProductDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public IFormFile? Picture { get; set; } 
    public string? PictureUrl { get; set; }
    public Guid ProductTypeId { get; set; }
    public Guid ProductBrandId { get; set; }
}
