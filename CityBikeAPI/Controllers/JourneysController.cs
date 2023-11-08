using CityBikeAPI.Models;
using CityBikeAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityBikeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneysController : ControllerBase
    {
        private readonly CityBikeDbContext _db;
        private readonly JourneyService _journeyService;

        public JourneysController(CityBikeDbContext db, JourneyService journeyService)
        {
            _db = db;
            _journeyService = journeyService;
        }

        [HttpGet]
        public IActionResult GetPaginatedJourneys(int page = 0, int pageSize = 100)
        {
            try
            {
                var totalRecords = _db.Journeys.Count();
                var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                //Request validation:
                if (page < 0)
                {
                    return BadRequest("Invalid page number. Page number must be greater than or equal to 1.");
                }

                if (page > totalPages)
                {
                    return BadRequest($"Requested page {page} exceeds the total number of pages ({totalPages}).");
                }

                if (pageSize <= 0)
                {
                    return BadRequest("Invalid page size. Page size must be greater than 0.");
                }
            

                //Using DTO
                var query = _db.Journeys
                    .Skip((page) * pageSize)
                    .Take(pageSize)
                    .Select(journey => new JourneyDto
                    {
                        Id = journey.Id,
                        DepartureStationName = journey.DepartureStationName,
                        ReturnStationName = journey.ReturnStationName,
                        CoveredKilometers = _journeyService.metersToKilometers(journey.CoveredDistanceM),
                        DurationTime = _journeyService.secondsToHoursAndMinutes(journey.DurationSec)
                    });

                var data = query.ToList();

                //Return information about the pagination in the response headers:
                Response.Headers.Add("X-Total-Count", totalRecords.ToString());
                Response.Headers.Add("X-Total-Pages", totalPages.ToString());

                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong: " + e.Message);
            }

        }


    }
}
