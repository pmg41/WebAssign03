using System;
using System.Collections.Generic;

namespace Assign03.Data;

public partial class Product
{
    public int ProductId { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public decimal? Pricing { get; set; }

    public decimal? ShippingCost { get; set; }
}
