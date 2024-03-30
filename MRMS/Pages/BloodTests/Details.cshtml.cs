using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MRMS.Data;
using MRMS.Models;

namespace MRMS.Pages.BloodTests
{
    [Authorize(Roles = "doctor, admin, nurse, patient")]
    public class DetailsModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;

        public DetailsModel(MRMS.Data.MRMSContext context)
        {
            _context = context;
        }

      public BloodTest BloodTest { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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
    }
}
