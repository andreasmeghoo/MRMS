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

namespace MRMS.Pages.Consultations
{
    [Authorize(Roles = "doctor, admin, nurse")]
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
        public Consultation Consultation { get; set; } = default!;

        public IList<Appointment> Appointments { get; set; }
        public IList<User> Doctors { get; set; }
        public IList<User> Nurses { get; set; }

        public IList<User> Patients { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            Doctors = _userManager.GetUsersInRoleAsync("doctor").Result.ToList();
            Nurses = _userManager.GetUsersInRoleAsync("nurse").Result.ToList();
            Patients = _userManager.GetUsersInRoleAsync("patient").Result.ToList();
            if (_context.Appointment != null)
            {
                Appointments = _context.Appointment.ToList();
            }
            if (id == null || _context.Consultation == null)
            {
                return NotFound();
            }

            var consultation =  await _context.Consultation.FirstOrDefaultAsync(m => m.ConsultationId == id);
            if (consultation == null)
            {
                return NotFound();
            }
            Consultation = consultation;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int appointmentId)
        {
            Doctors = _userManager.GetUsersInRoleAsync("doctor").Result.ToList();
            Nurses = _userManager.GetUsersInRoleAsync("nurse").Result.ToList();
            Patients = _userManager.GetUsersInRoleAsync("patient").Result.ToList();
            Consultation.AppointmentId = appointmentId;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Consultation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultationExists(Consultation.ConsultationId))
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

        private bool ConsultationExists(int id)
        {
          return (_context.Consultation?.Any(e => e.ConsultationId == id)).GetValueOrDefault();
        }
    }
}
