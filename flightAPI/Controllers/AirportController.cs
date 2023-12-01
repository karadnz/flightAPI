using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace flightAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        List<Airport> airports = new List<Airport>
        {
            new Airport {Id = 1, Name = "Sabiha", City = "Istanbul"},
            new Airport {Id = 2, Name = "Ataturk", City = "Istanbul"}, // Changed Id to 2
            new Airport {Id = 3, Name = "Vecihi", City = "Zibarankoy"} // Changed Id to 3
        };

        // GET: api/airport
        [HttpGet]
        public IEnumerable<Airport> Get()
        {
            return airports;
        }

        // GET api/airport/5
        [HttpGet("{id}")]
        public ActionResult<Airport> Get(int id)
        {
            var airport = airports.FirstOrDefault(a => a.Id == id);
            if (airport == null)
            {
                return NotFound();
            }
            return airport;
        }

        // POST api/airport
        [HttpPost]
        public ActionResult Post([FromBody] Airport newAirport)
        {
            airports.Add(newAirport);
            return CreatedAtAction(nameof(Get), new { id = newAirport.Id }, newAirport);
        }

        // PUT api/airport/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Airport updatedAirport)
        {
            var airport = airports.FirstOrDefault(a => a.Id == id);
            if (airport == null)
            {
                return NotFound();
            }
            airport.Name = updatedAirport.Name;
            airport.City = updatedAirport.City;
            return NoContent();
        }

        // DELETE api/airport/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var airport = airports.FirstOrDefault(a => a.Id == id);
            if (airport == null)
            {
                return NotFound();
            }
            airports.Remove(airport);
            return NoContent();
        }
    }

}
