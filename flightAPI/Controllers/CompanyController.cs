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
        public async Task<ActionResult<IEnumerable<CompanyDTO>>> Get()
        {
            var companies = await _context.Companies
                .Select(c => new CompanyDTO { Id = c.Id, Name = c.Name, City = c.City })
                .ToListAsync();
            return Ok(companies);
        }

        // GET api/company/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDTO>> Get(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            var companyDTO = new CompanyDTO { Id = company.Id, Name = company.Name, City = company.City };
            return companyDTO;
        }

        // POST api/company
        [HttpPost]
        public async Task<ActionResult<CompanyDTO>> Post([FromBody] CompanyDTO newCompanyDTO)
        {
            var company = new Company { Name = newCompanyDTO.Name, City = newCompanyDTO.City };
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            newCompanyDTO.Id = company.Id; // Assign the generated ID back to DTO
            return CreatedAtAction(nameof(Get), new { id = company.Id }, newCompanyDTO);
        }

        // PUT api/company/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CompanyDTO updatedCompanyDTO)
        {
            if (id != updatedCompanyDTO.Id)
            {
                return BadRequest();
            }

            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            company.Name = updatedCompanyDTO.Name;
            company.City = updatedCompanyDTO.City;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/company/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
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


