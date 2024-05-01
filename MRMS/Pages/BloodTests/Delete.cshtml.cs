using System;
using System.Collections;
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
    [Authorize(Roles = "doctor, nurse")]
    public class DeleteModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;
        private readonly UserManager<User> _userManager;

        public DeleteModel(MRMS.Data.MRMSContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public BloodTest BloodTest { get; set; } = default!;
       
        public List<User> Staff { get; set; } = new List<User>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var doctors = _userManager.GetUsersInRoleAsync("doctor").Result.ToList();
            var nurses = _userManager.GetUsersInRoleAsync("nurse").Result.ToList();
            Staff.AddRange(doctors);
            Staff.AddRange(nurses);
            if (id == null || _context.BloodTest == null)
            {
                return NotFound();
            }

            var bloodtest = await _context.BloodTest.FirstOrDefaultAsync(m => m.BloodTestId == id);

            if (bloodtest == null)
            {
                return NotFound();
            }
            else 
            {
                BloodTest = bloodtest;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.BloodTest == null)
            {
                return NotFound();
            }
            var bloodtest = await _context.BloodTest.FindAsync(id);

            if (bloodtest != null)
            {
                BloodTest = bloodtest;
                _context.BloodTest.Remove(BloodTest);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
