namespace CityBikeAPI.Models
{
    public class JourneyDto
    {
        public int Id { get; set; }
        public string DepartureStationName { get; set; } = null!;
        public string ReturnStationName { get; set; } = null!;
        public string? DurationTime { get; set; }
        public string? CoveredKilometers { get; set; }

    }
}
