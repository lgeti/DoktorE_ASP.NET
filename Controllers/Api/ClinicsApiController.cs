using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Filters;
using web.Models;

namespace web.Controllers_Api
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class ClinicsApiController : ControllerBase
    {
        private readonly DoktorEContext _context;

        public ClinicsApiController(DoktorEContext context)
        {
            _context = context;
        }

        // GET: api/ClinicsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clinic>>> GetClinics()
        {
          if (_context.Clinics == null)
          {
              return NotFound();
          }
            return await _context.Clinics.ToListAsync();
        }

        // GET: api/ClinicsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Clinic>> GetClinic(int id)
        {
          if (_context.Clinics == null)
          {
              return NotFound();
          }
            var clinic = await _context.Clinics.FindAsync(id);

            if (clinic == null)
            {
                return NotFound();
            }

            return clinic;
        }

        // PUT: api/ClinicsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClinic(int id, Clinic clinic)
        {
            if (id != clinic.ID)
            {
                return BadRequest();
            }

            _context.Entry(clinic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ClinicsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Clinic>> PostClinic(Clinic clinic)
        {
          if (_context.Clinics == null)
          {
              return Problem("Entity set 'DoktorEContext.Clinics'  is null.");
          }
            _context.Clinics.Add(clinic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClinic", new { id = clinic.ID }, clinic);
        }

        // DELETE: api/ClinicsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClinic(int id)
        {
            if (_context.Clinics == null)
            {
                return NotFound();
            }
            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }

            _context.Clinics.Remove(clinic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClinicExists(int id)
        {
            return (_context.Clinics?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
