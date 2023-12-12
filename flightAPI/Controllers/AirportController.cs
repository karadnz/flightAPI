using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using flightAPI.Models;
using flightAPI.DTO;
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
        public async Task<ActionResult<IEnumerable<AirportDTO>>> Get()
        {
            var airports = await _context.Airports
                .Select(a => new AirportDTO { Id = a.Id, Name = a.Name, City = a.City })
                .ToListAsync();
            return Ok(airports);
        }

        // GET api/airport/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AirportDTO>> Get(int id)
        {
            var airport = await _context.Airports.FindAsync(id);
            if (airport == null)
            {
                return NotFound();
            }
            var airportDTO = new AirportDTO { Id = airport.Id, Name = airport.Name, City = airport.City };
            return airportDTO;
        }

        // POST api/airport
        [HttpPost]
        public async Task<ActionResult<AirportDTO>> Post([FromBody] AirportDTO newAirportDTO)
        {
            var airport = new Airport { Name = newAirportDTO.Name, City = newAirportDTO.City };
            _context.Airports.Add(airport);
            await _context.SaveChangesAsync();

            newAirportDTO.Id = airport.Id; // Assign the generated ID back to DTO
            return CreatedAtAction(nameof(Get), new { id = airport.Id }, newAirportDTO);
        }

        // PUT api/airport/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AirportDTO updatedAirportDTO)
        {
            if (id != updatedAirportDTO.Id)
            {
                return BadRequest();
            }

            var airport = await _context.Airports.FindAsync(id);
            if (airport == null)
            {
                return NotFound();
            }

            airport.Name = updatedAirportDTO.Name;
            airport.City = updatedAirportDTO.City;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/airport/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
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
