using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MRMS.Data;
using MRMS.Models;

namespace MRMS.Pages.BloodTests
{
    [Authorize(Roles = "doctor, admin, nurse, patient")]
    public class ResultsModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;
        private readonly UserManager<User> _userManager;

        public ResultsModel(MRMS.Data.MRMSContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public BloodTestResult BloodTestResult { get; set; } = default!;
        public IList<User> Patients { get; set; } = default!;

        public IList<User> Doctors { get; set; } = default!;

        public IList<Consultation> AllConsultations { get; set; } = default!;

        public IList<Appointment> AllAppointments { get; set; } = default!;

        public IList<BloodTest> AllBloodTests { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Patients = _userManager.GetUsersInRoleAsync("patient").Result.ToList();
            Doctors = _userManager.GetUsersInRoleAsync("doctor").Result.ToList();
            AllAppointments = await _context.Appointment.ToListAsync();
            AllConsultations = await _context.Consultation.ToListAsync();
            AllBloodTests = await _context.BloodTest.ToListAsync();
            if (id == null || _context.BloodTestResult == null)
            {
                return NotFound();
            }

            var bloodtestresult = await _context.BloodTestResult.FirstOrDefaultAsync(m => m.BloodTestId == id);
            if (bloodtestresult == null)
            {
                return NotFound();
            }
            else 
            {
                BloodTestResult = bloodtestresult;
            }
            return Page();
        }
    }
}
