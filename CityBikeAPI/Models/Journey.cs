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

    public string Kilometers
    {
        get
        {
            double? kilometers = this.CoveredDistanceM / 1000;
            double? roundedKm = Math.Round((double)kilometers.GetValueOrDefault(), 2);
            string result = roundedKm.GetValueOrDefault().ToString();
            return result;
        }
    }

    public string Minutes
    {
        get
        {
            int seconds = this.DurationSec % 60;
            int minutes = (this.DurationSec - seconds) / 60;
            string result = "";

            if (minutes > 60)
            {
                int newMinutes = minutes % 60;
                int hours = (minutes - newMinutes) / 60;
                result = hours.ToString() + "h " + newMinutes.ToString() + "m " + seconds.ToString() + "s";
            }
            else
            {
                result = minutes.ToString() + "m " + seconds.ToString() + "s";
            }
            return result;
        }
    }
}
