using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MRMS.Data;
using MRMS.Enums;
using MRMS.Models;

namespace MRMS.Pages.Prescriptions
{
    [Authorize(Roles="doctor, admin")]
    public class CreateModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;
        private readonly UserManager<User> _userManager;

        public CreateModel(MRMS.Data.MRMSContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            Patients = _userManager.GetUsersInRoleAsync("patient").Result.ToList();
            Doctors = _userManager.GetUsersInRoleAsync("doctor").Result.ToList();
            if (_context.Consultation != null && _context.Appointment != null)
            {
                Consultations = _context.Consultation.ToList();
                Appointments = _context.Appointment.ToList();
            }
            return Page();
        }

        [BindProperty]
        public Prescription Prescription { get; set; } = default!;

        public IList<Consultation> Consultations { get; set; }

        public IList<Appointment> Appointments { get; set; }

        public IList<User> Patients { get; set; }

        public IList<User> Doctors { get; set; }
 

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Prescription == null || Prescription == null)
            {
                return Page();
            }

            _context.Prescription.Add(Prescription);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
