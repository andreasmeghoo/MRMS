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
    public class CreateModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;
        private readonly UserManager<User> _userManager;

        public CreateModel(MRMS.Data.MRMSContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Appointment> Appointments { get; set; }
        public IList<User> Doctors { get; set; }
        public IList<User> Nurses { get; set; }

        public IList<User> Patients { get; set; }

        public  IActionResult OnGet()
        {
            Doctors = _userManager.GetUsersInRoleAsync("doctor").Result.ToList();
            Nurses = _userManager.GetUsersInRoleAsync("nurse").Result.ToList();
            Patients = _userManager.GetUsersInRoleAsync("patient").Result.ToList();
            if (_context.Appointment != null)
            {
                Appointments = _context.Appointment.ToList();
            }
                
            return Page();
        }

        [BindProperty]
        public Consultation Consultation { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Consultation == null || Consultation == null)
            {
                return Page();
            }

            _context.Consultation.Add(Consultation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
