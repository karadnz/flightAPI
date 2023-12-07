using System;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace flightAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftController : ControllerBase
    {
        private readonly DataContext _context;

        public AircraftController(DataContext context)
        {
            _context = context;
        }

        // GET: api/aircraft
        [HttpGet]
        public async Task<IEnumerable<Aircraft>> Get()
        {
            return await _context.Aircrafts
                                 .Include(a => a.AircraftModel)
                                 .Include(a => a.Company)
                                 .ToListAsync();
        }

        // GET api/aircraft/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aircraft>> Get(int id)
        {
            var aircraft = await _context.Aircrafts
                                         .Include(a => a.AircraftModel)
                                         .Include(a => a.Company)
                                         .FirstOrDefaultAsync(a => a.Id == id);

            if (aircraft == null)
            {
                return NotFound();
            }
            return aircraft;
        }

        // POST api/aircraft
        [HttpPost]
        public async Task<ActionResult<Aircraft>> Post([FromBody] Aircraft newAircraft)
        {
            _context.Aircrafts.Add(newAircraft);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = newAircraft.Id }, newAircraft);
        }

        // PUT api/aircraft/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Aircraft updatedAircraft)
        {
            var aircraft = await _context.Aircrafts.FindAsync(id);
            if (aircraft == null)
            {
                return NotFound();
            }

            aircraft.AircraftModelId = updatedAircraft.AircraftModelId;
            aircraft.CompanyId = updatedAircraft.CompanyId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/aircraft/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var aircraft = await _context.Aircrafts.FindAsync(id);
            if (aircraft == null)
            {
                return NotFound();
            }

            _context.Aircrafts.Remove(aircraft);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
