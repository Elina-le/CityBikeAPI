using CityBikeAPI.Models;
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

        public JourneysController(CityBikeDbContext db)
        {
            _db = db;
        }


        //[HttpGet]
        //public ActionResult GetAllJourneys()
        //{
        //    try
        //    {
        //        List<Journey> journeys = _db.Journeys.Take(100).ToList();
        //        return Ok(journeys);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest("Something went wrong: " + e.Message);
        //    }
        //}


        [HttpGet]
        public IActionResult GetPaginatedJourneys(int page = 0, int pageSize = 100)
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

            var query = _db.Journeys.Skip((page) * pageSize).Take(pageSize);
            var data = query.ToList();

            //Return information about the pagination in the response headers:
            Response.Headers.Add("X-Total-Count", totalRecords.ToString());
            Response.Headers.Add("X-Total-Pages", totalPages.ToString());

            return Ok(data);

        }


    }
}
