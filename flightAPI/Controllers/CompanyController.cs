using System;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace flightAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly DataContext _context;

        public CompanyController(DataContext context)
        {
            _context = context;
        }

        // GET: api/company
        [HttpGet]
        public async Task<IEnumerable<Company>> Get()
        {
            return await _context.Companies.ToListAsync();
        }

        // GET api/company/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> Get(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return company;
        }

        // POST api/company
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Company newCompany)
        {
            _context.Companies.Add(newCompany);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = newCompany.Id }, newCompany);
        }

        // PUT api/company/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Company updatedCompany)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            company.Name = updatedCompany.Name;
            company.City = updatedCompany.City;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/company/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

