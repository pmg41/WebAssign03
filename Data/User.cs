using System;
using System.Collections.Generic;

namespace Assign03.Data;

public partial class User
{
    public int UserId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Username { get; set; }

    public string? PurchaseHistory { get; set; }

    public string? ShippingAddress { get; set; }
}
