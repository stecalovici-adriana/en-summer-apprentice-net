﻿using System;
using System.Collections.Generic;

namespace TicketManagerSystem.Api.Models;

public partial class TicketCategory
{
    public int TicketCategoryId { get; set; }

    public int EventId { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
