using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

public class UserRol 
{
    public int IdUser { get; set; }
    public User User { get; set; }
    public int IdRol { get; set; }
    public Rol Rol { get; set; }
}
