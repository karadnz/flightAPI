using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Helpers;

namespace flightAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly DataContext _context;

        public AirportController(DataContext context)
        {
            _context = context;
        }

        // GET: api/airport
        [HttpGet]
        public async Task<IEnumerable<Airport>> Get()
        {
            return await _context.Airports.ToListAsync();
        }

        // GET api/airport/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Airport>> Get(int id)
        {
            var airport = await _context.Airports.FindAsync(id);
            if (airport == null)
            {
                return NotFound();
            }
            return airport;
        }

        // POST api/airport
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Airport newAirport)
        {
            _context.Airports.Add(newAirport);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = newAirport.Id }, newAirport);
        }

        // PUT api/airport/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Airport updatedAirport)
        {
            var airport = await _context.Airports.FindAsync(id);
            if (airport == null)
            {
                return NotFound();
            }
            airport.Name = updatedAirport.Name;
            airport.City = updatedAirport.City;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/airport/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var airport = await _context.Airports.FindAsync(id);
            if (airport == null)
            {
                return NotFound();
            }
            _context.Airports.Remove(airport);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
