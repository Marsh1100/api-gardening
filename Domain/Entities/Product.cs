using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Persistence.Entities;

public partial class Product: BaseEntity
{

    public string ProductCode { get; set; }

    public string Name { get; set; }

    public int IdProductType { get; set; }

    public string Dimensions { get; set; }

    public string Provider { get; set; }

    public string Description { get; set; }

    public short Stock { get; set; }

    public decimal SalePrice { get; set; }

    public decimal? ProviderPrice { get; set; }

    public virtual Producttype IdProductTypeNavigation { get; set; }

    public virtual ICollection<Requestdetail> Requestdetails { get; set; } = new List<Requestdetail>();
}
