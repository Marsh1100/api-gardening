using System;
using System.Collections.Generic;
namespace Domain.Entities;

public partial class Office : BaseEntity
{

    public string OfficineCode { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public string Region { get; set; }

    public string ZipCode { get; set; }

    public string Phone { get; set; }

    public string Address1 { get; set; }

    public string Address2 { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
