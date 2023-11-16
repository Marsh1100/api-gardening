using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class RequestdetailDto
{
    public int Id { get; set; }
    [Required]
    public int IdRequest { get; set; }
    [Required]
    public int IdProduct { get; set; }
    [Required]
    public int Quantity { get; set; }
    public decimal? UnitPrice { get; set; }
    [Required]
    public short LineNumber { get; set; }
}
