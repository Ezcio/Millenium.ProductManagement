using ProductManagement.Core.Enums;

namespace ProductManagement.Core.DTO;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ProductType ProductType { get; set; }
}
