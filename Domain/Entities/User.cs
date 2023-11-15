using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string IdenNumber { get; set; }
    public string UserName { get; set;}
    public string Email { get; set; }
    public string Password { get; set; }

    public ICollection<Rol> Roles { get; set; } = new HashSet<Rol>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
    public ICollection<UserRol> UsersRoles { get; set; }

}
