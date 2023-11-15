using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Client : BaseEntity
{
    public string NameClient { get; set; }

    public string NameContact { get; set; }

    public string LastnameContact { get; set; }

    public string PhoneNumber { get; set; }

    public string Fax { get; set; }

    public string Address1 { get; set; }

    public string Address2 { get; set; }

    public string City { get; set; }

    public string Region { get; set; }

    public string Country { get; set; }

    public string ZipCode { get; set; }

    public int? IdEmployee { get; set; }

    public decimal? CreditLimit { get; set; }

    public virtual Employee IdEmployeeNavigation { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
