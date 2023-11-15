using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Persistence.Entities;

public partial class Request :BaseEntity
{

    public DateOnly RequestDate { get; set; }

    public DateOnly ExpectedDate { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public string State { get; set; }

    public string Feedback { get; set; }

    public int IdClient { get; set; }

    public virtual Client IdClientNavigation { get; set; }

    public virtual ICollection<Requestdetail> Requestdetails { get; set; } = new List<Requestdetail>();
}
