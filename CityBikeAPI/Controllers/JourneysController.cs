using CityBikeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


        [HttpGet]
        public ActionResult GetAllJourneys()
        {
            try
            {
                List<Journey> journeys = _db.Journeys.Take(100).ToList();
                return Ok(journeys);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong: " + e.Message);
            }
        }



    }
}
