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
    public class DoctorsApiController : ControllerBase
    {
        private readonly DoktorEContext _context;

        public DoctorsApiController(DoktorEContext context)
        {
            _context = context;
        }

        // GET: api/DoctorsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
          if (_context.Doctors == null)
          {
              return NotFound();
          }
            return await _context.Doctors.ToListAsync();
        }

        // GET: api/DoctorsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
          if (_context.Doctors == null)
          {
              return NotFound();
          }
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        // PUT: api/DoctorsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(int id, Doctor doctor)
        {
            if (id != doctor.ID)
            {
                return BadRequest();
            }

            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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

        // POST: api/DoctorsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
        {
          if (_context.Doctors == null)
          {
              return Problem("Entity set 'DoktorEContext.Doctors'  is null.");
          }
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctor", new { id = doctor.ID }, doctor);
        }

        // DELETE: api/DoctorsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            if (_context.Doctors == null)
            {
                return NotFound();
            }
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorExists(int id)
        {
            return (_context.Doctors?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
