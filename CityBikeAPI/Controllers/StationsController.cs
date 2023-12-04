using CityBikeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityBikeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly CityBikeDbContext _db;

        public StationsController(CityBikeDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult GetAllStations()
        {
            try
            {
                var stations = from s in _db.Stations.OrderBy(o => o.Name) select s;
                return Ok(stations);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong: " + e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetStationCalculations(int id)
        {
            // Average distance of a journey starting from the station
            var departureAverageDistance = _db.Journeys
                .Where(j => j.DepartureStationId == id)
                .Average(j => (double?)j.CoveredDistanceM);

            decimal departureAverageDistanceInKilometers = (decimal)(departureAverageDistance.GetValueOrDefault() / 1000.0);
            decimal roundedAverageDistanceInKilometers_departure = Math.Round(departureAverageDistanceInKilometers, 2);
            
            // Average distance of a journey ending at the station
            var returnAverageDistance = _db.Journeys
               .Where(j => j.ReturnStationId == id)
               .Average(j => (double?)j.CoveredDistanceM);

            decimal returnAverageDistanceInKilometers = (decimal)(returnAverageDistance.GetValueOrDefault() / 1000.0);
            decimal roundedAverageDistanceInKilometers_return = Math.Round(returnAverageDistanceInKilometers, 2);

            // The top 5 most popular return stations for journeys starting from the station.
            var top5ReturnStations = _db.Journeys
                    .Where(j => j.DepartureStationId == id)
                    .GroupBy(j => j.ReturnStationId)
                    .OrderByDescending(group => group.Count())
                    .Take(5)
                    .Select(group => new
                    {
                        returnStationId = group.Key,
                        returnStationName = group.First().ReturnStationName,
                        numberOfReturns = group.Count()
                    })
                    .ToList();

            //The 5 most popular departure stations for journeys ending at the station.
            var top5DepartureStations = _db.Journeys
                    .Where(j => j.ReturnStationId == id)
                    .GroupBy(j => j.DepartureStationId)
                    .OrderByDescending(group => group.Count())
                    .Take(5)
                    .Select(group => new
                    {
                        departureStationId = group.Key,
                        departureStationName = group.First().DepartureStationName,
                        numberOfDepartures = group.Count()
                    })
                    .ToList();

            try
            {
                var stationCalculations = new
                {
                    Id = id,
                    Departures = _db.Journeys.Count(j => j.DepartureStationId == id),
                    Returns = _db.Journeys.Count(j => j.ReturnStationId == id),
                    AverageDistanceStartingFromStation = roundedAverageDistanceInKilometers_departure,
                    AverageDistanceEndingAtStation = roundedAverageDistanceInKilometers_return,
                    Top5ReturnStations = top5ReturnStations,
                    Top5DepartureStations = top5DepartureStations,
                };

                if (stationCalculations != null)
                {
                    return Ok(stationCalculations);
                }
                else
                {
                    return NotFound($"Station with id: {id}, did not found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong: " + e.Message);
            }
        }

    }
}
