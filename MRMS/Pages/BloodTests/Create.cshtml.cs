using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Diagnostics;
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

            BloodTestResult bloodTestResult = new BloodTestResult();
            bloodTestResult.BloodTestId = BloodTest.BloodTestId;
            bloodTestResult.redBloodCellCount = GenerateBloodTestResult((decimal)4.5, 9);
            bloodTestResult.whiteBloodCellCount = GenerateBloodTestResult((decimal)4.5, 100);
            bloodTestResult.plateletCount = GenerateBloodTestResult((decimal)150, 450);
            bloodTestResult.glucoseLevel = GenerateBloodTestResult((decimal)70, 600);
            bloodTestResult.cholestorolLevel = GenerateBloodTestResult((decimal)300, 1000);
            bloodTestResult.liverFunction = GenerateBloodTestResult((decimal)7, 56);
            bloodTestResult.kidneyFunction = GenerateBloodTestResult((decimal)0.5, 10);
            await _context.BloodTestResult.AddAsync(bloodTestResult);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public static decimal GenerateBloodTestResult(decimal minimum, decimal maximum)
        {
            Random random = new Random();
            return (decimal)(random.NextDouble() * (double)(maximum - minimum)) + minimum;
        }
    }
}
