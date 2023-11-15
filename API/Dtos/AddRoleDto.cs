
using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class AddRoleDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Role { get; set; }
}

public class RolDto
{
    public string Rol { get; set; }
}