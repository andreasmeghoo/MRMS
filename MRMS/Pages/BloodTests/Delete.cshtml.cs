using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MRMS.Data;
using MRMS.Models;

namespace MRMS.Pages.BloodTests
{
    public class DeleteModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;

        public DeleteModel(MRMS.Data.MRMSContext context)
        {
            _context = context;
        }

        [BindProperty]
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
