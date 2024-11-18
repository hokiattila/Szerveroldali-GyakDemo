using System;
using System.Collections.Generic;

namespace CarSalesAPI.Models;

public partial class Car
{
    public string Vin { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Modell { get; set; } = null!;

    public short BuildYear { get; set; }

    public short DoorCount { get; set; }

    public string Color { get; set; } = null!;

    public short? Weight { get; set; }

    public short? Power { get; set; }

    public string Con { get; set; } = null!;

    public string FuelType { get; set; } = null!;

    public int Price { get; set; }
}
