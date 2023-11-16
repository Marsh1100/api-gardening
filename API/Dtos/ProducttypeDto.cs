using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class ProducttypeDto
{
    public int Id { get; set; }
    [Required]
    public string Type { get; set; }
    public string DescriptionText { get; set; }
    public string DescriptionHtml { get; set; }
    public string Image { get; set; }

    
}
