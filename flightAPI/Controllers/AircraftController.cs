using System;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;



using flightAPI.DTO;


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
        public async Task<ActionResult<IEnumerable<AircraftDTO>>> Get()
        {
            var aircrafts = await _context.Aircrafts
                                          .Include(a => a.AircraftModel)
                                          .Include(a => a.Company)
                                          .Select(a => new AircraftDTO
                                          {
                                              Id = a.Id,
                                              AircraftModelId = a.AircraftModelId,
                                              CompanyId = a.CompanyId
                                          })
                                          .ToListAsync();
            return Ok(aircrafts);
        }

        // GET api/aircraft/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AircraftDTO>> Get(int id)
        {
            var aircraft = await _context.Aircrafts
                                         .Include(a => a.AircraftModel)
                                         .Include(a => a.Company)
                                         .Where(a => a.Id == id)
                                         .Select(a => new AircraftDTO
                                         {
                                             Id = a.Id,
                                             AircraftModelId = a.AircraftModelId,
                                             CompanyId = a.CompanyId
                                         })
                                         .FirstOrDefaultAsync();

            if (aircraft == null)
            {
                return NotFound();
            }
            return Ok(aircraft);
        }

        // POST api/aircraft
        [HttpPost]
        public async Task<ActionResult<AircraftDTO>> Post([FromBody] AircraftDTO newAircraftDto)
        {
            var newAircraft = new Aircraft
            {
                AircraftModelId = newAircraftDto.AircraftModelId,
                CompanyId = newAircraftDto.CompanyId
            };

            _context.Aircrafts.Add(newAircraft);
            await _context.SaveChangesAsync();

            newAircraftDto.Id = newAircraft.Id;
            return CreatedAtAction(nameof(Get), new { id = newAircraft.Id }, newAircraftDto);
        }

        // PUT api/aircraft/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AircraftDTO updatedAircraftDto)
        {
            var aircraft = await _context.Aircrafts.FindAsync(id);
            if (aircraft == null)
            {
                return NotFound();
            }

            aircraft.AircraftModelId = updatedAircraftDto.AircraftModelId;
            aircraft.CompanyId = updatedAircraftDto.CompanyId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/aircraft/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
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

