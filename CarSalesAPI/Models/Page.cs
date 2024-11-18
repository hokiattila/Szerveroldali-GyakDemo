using System;
using System.Collections.Generic;

namespace CarSalesAPI.Models;

public partial class Page
{
    public string Url { get; set; } = null!;

    public string Page1 { get; set; } = null!;

    public string Permission { get; set; } = null!;

    public byte Sortingorder { get; set; }
}
