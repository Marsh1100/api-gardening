using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class EmployeeDto
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string FirstSurname { get; set; }
    [Required]
    public string SecondSurname { get; set; }
    [Required]
    public string Extension { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public int IdOffice { get; set; }

    public int? IdBoss { get; set; }

    public string Position { get; set; }
    
}
