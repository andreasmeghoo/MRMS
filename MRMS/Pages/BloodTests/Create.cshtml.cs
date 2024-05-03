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
using MRMS.Models;
using NuGet.Packaging;

namespace MRMS.Pages.BloodTests
{
    [Authorize(Roles = "doctor, nurse")]
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
            var nurses = _userManager.GetUsersInRoleAsync("nurse").Result.ToList();
            Staff.AddRange(nurses);
            Staff.AddRange(Doctors);

            if (_context.Consultation != null && _context.Appointment != null)
            {
                Consultations = _context.Consultation.ToList();
                Appointments = _context.Appointment.ToList();
            }
            return Page();
        }

        [BindProperty]
        public BloodTest BloodTest { get; set; } = default!;

        public IList<Consultation> Consultations { get; set; }

        public IList<Appointment> Appointments { get; set; }

        public IList<User> Patients { get; set; }

        public IList<User> Doctors { get; set; }

        public List<User> Staff { get; set; } = new List<User>();


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.BloodTest == null || BloodTest == null)
            {
                return Page();
            }

            _context.BloodTest.Add(BloodTest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
