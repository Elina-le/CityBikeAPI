namespace CityBikeAPI.Models
{
    public class PaginatedJourneyResponse<T>
    {
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> ?Data { get; set; }
    }
}
