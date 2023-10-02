using System;
using System.Collections.Generic;

namespace CityBikeAPI.Models;

public partial class Station
{
    public int Fid { get; set; }

    public int Id { get; set; }

    public string Nimi { get; set; } = null!;

    public string Namn { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Osoite { get; set; } = null!;

    public string Adress { get; set; } = null!;

    public string? Kaupunki { get; set; }

    public string? Stad { get; set; }

    public string? Operaattor { get; set; }

    public int Kapasiteet { get; set; }

    public string X { get; set; } = null!;

    public string Y { get; set; } = null!;

    public virtual ICollection<Journey> JourneyDepartureStations { get; set; } = new List<Journey>();

    public virtual ICollection<Journey> JourneyReturnStations { get; set; } = new List<Journey>();
}
