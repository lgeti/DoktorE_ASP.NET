using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers_Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsApiController : ControllerBase
    {
        private readonly DoktorEContext _context;

        public PrescriptionsApiController(DoktorEContext context)
        {
            _context = context;
        }

        // GET: api/PrescriptionsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prescription>>> GetPrescriptions()
        {
          if (_context.Prescriptions == null)
          {
              return NotFound();
          }
            return await _context.Prescriptions.ToListAsync();
        }

        // GET: api/PrescriptionsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prescription>> GetPrescription(int id)
        {
          if (_context.Prescriptions == null)
          {
              return NotFound();
          }
            var prescription = await _context.Prescriptions.FindAsync(id);

            if (prescription == null)
            {
                return NotFound();
            }

            return prescription;
        }

        // PUT: api/PrescriptionsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrescription(int id, Prescription prescription)
        {
            if (id != prescription.ID)
            {
                return BadRequest();
            }

            _context.Entry(prescription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrescriptionExists(id))
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

        // POST: api/PrescriptionsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Prescription>> PostPrescription(Prescription prescription)
        {
          if (_context.Prescriptions == null)
          {
              return Problem("Entity set 'DoktorEContext.Prescriptions'  is null.");
          }
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrescription", new { id = prescription.ID }, prescription);
        }

        // DELETE: api/PrescriptionsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            if (_context.Prescriptions == null)
            {
                return NotFound();
            }
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }

            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrescriptionExists(int id)
        {
            return (_context.Prescriptions?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
