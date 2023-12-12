using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Helpers;
using flightAPI.DTO;

namespace flightAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftModelController : ControllerBase
    {
        private readonly DataContext _context;

        public AircraftModelController(DataContext context)
        {
            _context = context;
        }

        // GET: api/aircraftmodel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AircraftModelDTO>>> Get()
        {
            var aircraftModels = await _context.AircraftModels
                .Select(a => new AircraftModelDTO { Id = a.Id, Name = a.Name, Capacity = a.Capacity })
                .ToListAsync();
            return Ok(aircraftModels);
        }

        // GET api/aircraftmodel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AircraftModelDTO>> Get(int id)
        {
            var aircraftModel = await _context.AircraftModels.FindAsync(id);
            if (aircraftModel == null)
            {
                return NotFound();
            }
            var aircraftModelDTO = new AircraftModelDTO { Id = aircraftModel.Id, Name = aircraftModel.Name, Capacity = aircraftModel.Capacity };
            return aircraftModelDTO;
        }

        // POST api/aircraftmodel
        [HttpPost]
        public async Task<ActionResult<AircraftModelDTO>> Post([FromBody] AircraftModelDTO newAircraftModelDTO)
        {
            var aircraftModel = new AircraftModel { Name = newAircraftModelDTO.Name, Capacity = newAircraftModelDTO.Capacity };
            _context.AircraftModels.Add(aircraftModel);
            await _context.SaveChangesAsync();

            newAircraftModelDTO.Id = aircraftModel.Id; // Assign the generated ID back to DTO
            return CreatedAtAction(nameof(Get), new { id = aircraftModel.Id }, newAircraftModelDTO);
        }

        // PUT api/aircraftmodel/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AircraftModelDTO updatedAircraftModelDTO)
        {
            if (id != updatedAircraftModelDTO.Id)
            {
                return BadRequest();
            }

            var aircraftModel = await _context.AircraftModels.FindAsync(id);
            if (aircraftModel == null)
            {
                return NotFound();
            }

            aircraftModel.Name = updatedAircraftModelDTO.Name;
            aircraftModel.Capacity = updatedAircraftModelDTO.Capacity;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/aircraftmodel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var aircraftModel = await _context.AircraftModels.FindAsync(id);
            if (aircraftModel == null)
            {
                return NotFound();
            }
            _context.AircraftModels.Remove(aircraftModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
