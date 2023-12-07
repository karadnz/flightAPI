using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Helpers;
using Route = flightAPI.Models.Route;

namespace flightAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly DataContext _context;

        public RouteController(DataContext context)
        {
            _context = context;
        }

        // GET: api/route
        [HttpGet]
        public async Task<IEnumerable<Route>> Get()
        {
            return await _context.Routes
                                 .Include(r => r.DepartureAirport)
                                 .Include(r => r.ArrivalAirport)
                                 .ToListAsync();
        }

        // GET api/route/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Route>> Get(int id)
        {
            var route = await _context.Routes
                                      .Include(r => r.DepartureAirport)
                                      .Include(r => r.ArrivalAirport)
                                      .FirstOrDefaultAsync(r => r.Id == id);
            if (route == null)
            {
                return NotFound();
            }
            return route;
        }

        // POST api/route
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Route newRoute)
        {
            _context.Routes.Add(newRoute);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = newRoute.Id }, newRoute);
        }

        // PUT api/route/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Route updatedRoute)
        {
            var route = await _context.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }

            route.DepartureAirportId = updatedRoute.DepartureAirportId;
            route.ArrivalAirportId = updatedRoute.ArrivalAirportId;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/route/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var route = await _context.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }
            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
