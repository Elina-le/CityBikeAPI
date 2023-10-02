﻿using CityBikeAPI.Models;
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

        [HttpGet("{id}")]
        public ActionResult GetSingleStation(int id) 
        {

            try
            {
                Station station = _db.Stations.Find(id);

                if (station != null)
                {
                    return Ok(station);
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



            //return Ok(new Station { Id = id });
        }
    }
}
