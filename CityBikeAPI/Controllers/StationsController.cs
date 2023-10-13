using CityBikeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityBikeAPI.Controllers
{
    [Route("api/[controller]")]
    //[Route("[controller]/[action]")]
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
                var stations = from s in _db.Stations.OrderBy(o => o.Name) select s;
                return Ok(stations);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong: " + e.Message);
            }
        }

        //[HttpGet("{id}")]
        //public ActionResult GetSingleStation(int id)
        //{
        //    try
        //    {
        //        //Station station = _db.Stations.Find(id);

        //        if (station != null)
        //        {
        //            return Ok(station);
        //        }
        //        else
        //        {
        //            return NotFound($"Station with id: {id}, did not found.");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest("Something went wrong: " + e.Message);
        //    }
        //}


        [HttpGet("{id}")]
        public ActionResult GetStationCalculations(int id)
        {
            try
            {
                var stationCalculations = new
                {
                    Id = id,
                    Departures = _db.Journeys.Count(j => j.DepartureStationId == id),
                    Returns = _db.Journeys.Count(j => j.ReturnStationId == id)
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
