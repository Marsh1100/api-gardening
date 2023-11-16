using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class PaymentDto
{
    public int Id { get; set; }
    [Required]
    public int IdClient { get; set; }
    [Required]
    public string TransactionId { get; set; }
    [Required]
    public string PaymentMethod { get; set; }
    [Required]
    public DateOnly PaymentDate { get; set; }
    [Required]
    public decimal Total { get; set; }
    
}
