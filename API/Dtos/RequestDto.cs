using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class RequestDto
{
    public int Id { get; set; }
    [Required]
    public DateOnly RequestDate { get; set; }
    [Required]
    public DateOnly ExpectedDate { get; set; }

    public DateOnly? DeliveryDate { get; set; }
    [Required]
    public string State { get; set; }

    public string Feedback { get; set; }

    public int IdClient { get; set; }
}
