using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using flightAPI.Models;
using flightAPI.DTO;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
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
        public async Task<ActionResult<IEnumerable<RouteDTO>>> Get()
        {
            var routes = await _context.Routes
                .Select(r => new RouteDTO { Id = r.Id, DepartureAirport = r.DepartureAirportId, ArrivalAirport = r.ArrivalAirportId })
                .ToListAsync();
            return Ok(routes);
        }

        // GET api/route/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RouteDTO>> Get(int id)
        {
            var route = await _context.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }
            var routeDTO = new RouteDTO { Id = route.Id, DepartureAirport = route.DepartureAirportId, ArrivalAirport = route.ArrivalAirportId };
            return routeDTO;
        }

        // POST api/route
        [HttpPost]
        public async Task<ActionResult<RouteDTO>> Post([FromBody] RouteDTO newRouteDTO)
        {
            var route = new Route { DepartureAirportId = newRouteDTO.DepartureAirport, ArrivalAirportId = newRouteDTO.ArrivalAirport };
            _context.Routes.Add(route);
            await _context.SaveChangesAsync();

            newRouteDTO.Id = route.Id; // Assign the generated ID back to DTO
            return CreatedAtAction(nameof(Get), new { id = route.Id }, newRouteDTO);
        }

        // PUT api/route/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RouteDTO updatedRouteDTO)
        {
            if (id != updatedRouteDTO.Id)
            {
                return BadRequest();
            }

            var route = await _context.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }

            route.DepartureAirportId = updatedRouteDTO.DepartureAirport;
            route.ArrivalAirportId = updatedRouteDTO.ArrivalAirport;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/route/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
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
