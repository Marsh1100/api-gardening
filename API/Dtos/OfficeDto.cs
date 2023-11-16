using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class OfficeDto
{
    public int Id { get; set; }
    [Required]
    public string OfficineCode { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string Country { get; set; }
    [Required]
    public string Region { get; set; }
    [Required]
    public string ZipCode { get; set; }
    [Required]
    public string Phone { get; set; }
    [Required]
    public string Address1 { get; set; }

    public string Address2 { get; set; }
    
}
