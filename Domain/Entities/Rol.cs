using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Rol : BaseEntity
{
    public string Name { get; set; }
    public ICollection<User> Users { get; set; } = new HashSet<User>();
    public ICollection<UserRol> UsersRoles { get; set; }

}
