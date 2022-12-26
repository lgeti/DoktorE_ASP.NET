using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly DoktorEContext _context;
        private readonly UserManager<ApplicationUser> _usermanager;

        public AppointmentsController(DoktorEContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _usermanager = userManager;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var doktorEContext = _context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).Include(a => a.Prescription);
            return View(await doktorEContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                    .ThenInclude(d => d.Clinic)
                .AsNoTracking()

                .Include(a => a.Patient)
                    .ThenInclude(p => p.BloodDonations)
                .AsNoTracking()

                .Include(a => a.Prescription)
                .AsNoTracking()

                .FirstOrDefaultAsync(m => m.ID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        [Authorize(Roles = "Staff, Administrator")]
        public IActionResult Create()
        {
            ViewData["DoctorID"] = new SelectList(_context.Doctors, "ID", "ID");
            ViewData["PatientID"] = new SelectList(_context.Patients, "ID", "ID");
            ViewData["PrescriptionID"] = new SelectList(_context.Prescriptions, "ID", "ID");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AppointmentDate,DoctorsNote,PatientID,DoctorID,PrescriptionID,InvoiceID")] Appointment appointment)
        {
            var currentUser = await _usermanager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                appointment.Owner = currentUser;

                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorID"] = new SelectList(_context.Doctors, "ID", "ID", appointment.DoctorID);
            ViewData["PatientID"] = new SelectList(_context.Patients, "ID", "ID", appointment.PatientID);
            ViewData["PrescriptionID"] = new SelectList(_context.Prescriptions, "ID", "ID", appointment.PrescriptionID);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        [Authorize(Roles = "Staff, Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["DoctorID"] = new SelectList(_context.Doctors, "ID", "ID", appointment.DoctorID);
            ViewData["PatientID"] = new SelectList(_context.Patients, "ID", "ID", appointment.PatientID);
            ViewData["PrescriptionID"] = new SelectList(_context.Prescriptions, "ID", "ID", appointment.PrescriptionID);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AppointmentDate,DoctorsNote,PatientID,DoctorID,PrescriptionID,InvoiceID")] Appointment appointment)
        {
            if (id != appointment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorID"] = new SelectList(_context.Doctors, "ID", "ID", appointment.DoctorID);
            ViewData["PatientID"] = new SelectList(_context.Patients, "ID", "ID", appointment.PatientID);
            ViewData["PrescriptionID"] = new SelectList(_context.Prescriptions, "ID", "ID", appointment.PrescriptionID);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        [Authorize(Roles = "Staff, Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.Prescription)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'DoktorEContext.Appointments'  is null.");
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }
            else{
                Console.WriteLine("*****ERROR*****DeleteConfirmed(metoda)*****ERROR***** var appointment = await _context.Appointments.FindAsync(id); appointment == null");
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
          return (_context.Appointments?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
