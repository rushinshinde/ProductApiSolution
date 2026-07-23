namespace Application.DTOs.Product;

public class UpdateProductDto
{
    public string ProductName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string ModifiedBy { get; set; } = string.Empty;
}