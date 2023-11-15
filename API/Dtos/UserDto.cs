using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string IdenNumber { get; set; }
    public string UserName { get; set; }
    
}
public class UserAllDto
{
    public int Id { get; set; }
    public string IdenNumber { get; set; }
    public string UserName { get; set; }
    public List<RolDto> Roles { get; set; }
}