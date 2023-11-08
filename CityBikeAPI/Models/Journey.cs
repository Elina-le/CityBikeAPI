using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CityBikeAPI.Models;

public partial class Journey
{
    public int Id { get; set; }

    public DateTime Departure { get; set; }

    public DateTime Return { get; set; }

    public int DepartureStationId { get; set; }

    public string DepartureStationName { get; set; } = null!;

    public int ReturnStationId { get; set; }

    public string ReturnStationName { get; set; } = null!;

    public double? CoveredDistanceM { get; set; }

    public int DurationSec { get; set; }

    public virtual Station DepartureStation { get; set; } = null!;

    public virtual Station ReturnStation { get; set; } = null!;

}
