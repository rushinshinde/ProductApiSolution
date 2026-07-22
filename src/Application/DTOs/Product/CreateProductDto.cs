namespace Application.DTOs.Product;

public class CreateProductDto
{
    public string ProductName { get; set; } = string.Empty;

    public string CreatedBy { get; set; } = string.Empty;
}