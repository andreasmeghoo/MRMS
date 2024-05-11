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
using MRMS.Enums;

namespace MRMS.Pages.Appointments
{
    [Authorize]
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
        public Appointment Appointment { get; set; } = default!;

        public List<User> Doctors { get; set; }

        public List<User> Patients { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Doctors = _userManager.GetUsersInRoleAsync("doctor").Result.ToList();
            Patients = _userManager.GetUsersInRoleAsync("patient").Result.ToList();
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var appointment =  await _context.Appointment.FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }
            Appointment = appointment;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string patientId, bool appointmentConfirmed)
        {
            Doctors = _userManager.GetUsersInRoleAsync("doctor").Result.ToList();
            Patients = _userManager.GetUsersInRoleAsync("patient").Result.ToList();
            ModelState.Remove("Appointment.PatientId");
            Appointment.PatientId = patientId;
            Appointment.Confirmed = appointmentConfirmed;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            _context.Attach(Appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(Appointment.AppointmentId))
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

        private bool AppointmentExists(int id)
        {
          return (_context.Appointment?.Any(e => e.AppointmentId == id)).GetValueOrDefault();
        }
    }
}
