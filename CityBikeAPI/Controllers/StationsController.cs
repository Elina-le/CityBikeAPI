using CityBikeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult GetAllSations()
        {
            try
            {
                List<Station> stations = _db.Stations.ToList();
                return Ok(stations);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong: " + e.Message);
            }
        }
    }
}
