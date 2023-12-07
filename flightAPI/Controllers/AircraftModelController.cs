using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Helpers;
using flightAPI.Models;

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
        public async Task<IEnumerable<AircraftModel>> Get()
        {
            return await _context.AircraftModels.ToListAsync();
        }

        // GET api/aircraftmodel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AircraftModel>> Get(int id)
        {
            var aircraftModel = await _context.AircraftModels.FindAsync(id);
            if (aircraftModel == null)
            {
                return NotFound();
            }
            return aircraftModel;
        }

        // POST api/aircraftmodel
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AircraftModel newAircraftModel)
        {
            _context.AircraftModels.Add(newAircraftModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = newAircraftModel.Id }, newAircraftModel);
        }

        // PUT api/aircraftmodel/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AircraftModel updatedAircraftModel)
        {
            var aircraftModel = await _context.AircraftModels.FindAsync(id);
            if (aircraftModel == null)
            {
                return NotFound();
            }
            aircraftModel.Name = updatedAircraftModel.Name;
            aircraftModel.Capacity = updatedAircraftModel.Capacity;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/aircraftmodel/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
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
