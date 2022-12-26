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
    public class BloodDonationsController : Controller
    {
        private readonly DoktorEContext _context;
        private readonly UserManager<ApplicationUser> _usermanager;
        public BloodDonationsController(DoktorEContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _usermanager = userManager;
        }

        // GET: BloodDonations
        public async Task<IActionResult> Index()
        {
            var doktorEContext = _context.BloodDonations.Include(b => b.Patient);
            return View(await doktorEContext.ToListAsync());
        }

        // GET: BloodDonations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BloodDonations == null)
            {
                return NotFound();
            }

            var bloodDonation = await _context.BloodDonations
                .Include(b => b.Patient)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bloodDonation == null)
            {
                return NotFound();
            }

            return View(bloodDonation);
        }

        // GET: BloodDonations/Create
        [Authorize(Roles = "Staff, Administrator")]
        public IActionResult Create()
        {
            ViewData["PatientID"] = new SelectList(_context.Patients, "ID", "ID");
            return View();
        }

        // POST: BloodDonations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,date,PatientID")] BloodDonation bloodDonation)
        {
            var currentUser = await _usermanager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                bloodDonation.Owner = currentUser;

                _context.Add(bloodDonation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientID"] = new SelectList(_context.Patients, "ID", "ID", bloodDonation.PatientID);
            return View(bloodDonation);
        }

        // GET: BloodDonations/Edit/5
        [Authorize(Roles = "Staff, Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BloodDonations == null)
            {
                return NotFound();
            }

            var bloodDonation = await _context.BloodDonations.FindAsync(id);
            if (bloodDonation == null)
            {
                return NotFound();
            }
            ViewData["PatientID"] = new SelectList(_context.Patients, "ID", "ID", bloodDonation.PatientID);
            return View(bloodDonation);
        }

        // POST: BloodDonations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,date,PatientID")] BloodDonation bloodDonation)
        {
            if (id != bloodDonation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bloodDonation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BloodDonationExists(bloodDonation.ID))
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
            ViewData["PatientID"] = new SelectList(_context.Patients, "ID", "ID", bloodDonation.PatientID);
            return View(bloodDonation);
        }

        // GET: BloodDonations/Delete/5
        [Authorize(Roles = "Staff, Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BloodDonations == null)
            {
                return NotFound();
            }

            var bloodDonation = await _context.BloodDonations
                .Include(b => b.Patient)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bloodDonation == null)
            {
                return NotFound();
            }

            return View(bloodDonation);
        }

        // POST: BloodDonations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BloodDonations == null)
            {
                return Problem("Entity set 'DoktorEContext.BloodDonations'  is null.");
            }
            var bloodDonation = await _context.BloodDonations.FindAsync(id);
            if (bloodDonation != null)
            {
                _context.BloodDonations.Remove(bloodDonation);
            }
            else{
                Console.WriteLine("*****ERROR*****DeleteConfirmed(metoda)*****ERROR***** var appointment = await _context.Appointments.FindAsync(id); appointment == null");
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BloodDonationExists(int id)
        {
          return (_context.BloodDonations?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
