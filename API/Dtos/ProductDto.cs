using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    [Required]
    public string ProductCode { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int IdProductType { get; set; }

    public string Dimensions { get; set; }

    public string Provider { get; set; }

    public string Description { get; set; }
    [Required]
    public short Stock { get; set; }
    [Required]
    public decimal SalePrice { get; set; }

    public decimal? ProviderPrice { get; set; }
    
}
