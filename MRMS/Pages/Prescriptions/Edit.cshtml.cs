using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MRMS.Data;
using MRMS.Models;

namespace MRMS.Pages.Prescriptions
{
    [Authorize(Roles = "doctor, admin")]
    public class EditModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;
        private readonly UserManager<User> _userManager;

        public EditModel(MRMS.Data.MRMSContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Prescription Prescription { get; set; } = default!;

        public IList<Consultation> Consultations { get; set; }

        public IList<Appointment> Appointments { get; set; }

        public IList<User> Patients { get; set; }

        public IList<User> Doctors { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Patients = _userManager.GetUsersInRoleAsync("patient").Result.ToList();
            Doctors = _userManager.GetUsersInRoleAsync("doctor").Result.ToList();
            if (_context.Consultation != null && _context.Appointment != null)
            {
                Consultations = _context.Consultation.ToList();
                Appointments = _context.Appointment.ToList();
            }

            if (id == null || _context.Prescription == null)
            {
                return NotFound();
            }

            var prescription =  await _context.Prescription.FirstOrDefaultAsync(m => m.PrescriptionId == id);

            if (prescription == null)
            {
                return NotFound();
            }
            Prescription = prescription;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int consultationId)
        {
            Prescription.ConsultationId = consultationId;
            Patients = _userManager.GetUsersInRoleAsync("patient").Result.ToList();
            Doctors = _userManager.GetUsersInRoleAsync("doctor").Result.ToList();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Prescription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrescriptionExists(Prescription.PrescriptionId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PrescriptionExists(int id)
        {
          return (_context.Prescription?.Any(e => e.PrescriptionId == id)).GetValueOrDefault();
        }
    }
}
