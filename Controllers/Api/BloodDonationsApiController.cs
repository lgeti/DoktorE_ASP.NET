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
    public class BloodDonationsApiController : ControllerBase
    {
        private readonly DoktorEContext _context;

        public BloodDonationsApiController(DoktorEContext context)
        {
            _context = context;
        }

        // GET: api/BloodDonationsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BloodDonation>>> GetBloodDonations()
        {
          if (_context.BloodDonations == null)
          {
              return NotFound();
          }
            return await _context.BloodDonations.ToListAsync();
        }

        // GET: api/BloodDonationsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BloodDonation>> GetBloodDonation(int id)
        {
          if (_context.BloodDonations == null)
          {
              return NotFound();
          }
            var bloodDonation = await _context.BloodDonations.FindAsync(id);

            if (bloodDonation == null)
            {
                return NotFound();
            }

            return bloodDonation;
        }

        // PUT: api/BloodDonationsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBloodDonation(int id, BloodDonation bloodDonation)
        {
            if (id != bloodDonation.ID)
            {
                return BadRequest();
            }

            _context.Entry(bloodDonation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BloodDonationExists(id))
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

        // POST: api/BloodDonationsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BloodDonation>> PostBloodDonation(BloodDonation bloodDonation)
        {
          if (_context.BloodDonations == null)
          {
              return Problem("Entity set 'DoktorEContext.BloodDonations'  is null.");
          }
            _context.BloodDonations.Add(bloodDonation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBloodDonation", new { id = bloodDonation.ID }, bloodDonation);
        }

        // DELETE: api/BloodDonationsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloodDonation(int id)
        {
            if (_context.BloodDonations == null)
            {
                return NotFound();
            }
            var bloodDonation = await _context.BloodDonations.FindAsync(id);
            if (bloodDonation == null)
            {
                return NotFound();
            }

            _context.BloodDonations.Remove(bloodDonation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BloodDonationExists(int id)
        {
            return (_context.BloodDonations?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
