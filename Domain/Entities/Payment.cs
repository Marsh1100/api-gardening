using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Persistence.Entities;

public partial class Payment : BaseEntity
{
    public int IdClient { get; set; }

    public string PaymentMethod { get; set; }

    public DateOnly PaymentDate { get; set; }

    public decimal Total { get; set; }

    public virtual Client IdClientNavigation { get; set; }
}
