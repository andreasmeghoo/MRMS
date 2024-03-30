using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MRMS.Data;
using MRMS.Models;

namespace MRMS.Pages.BloodTests
{
    [Authorize(Roles = "doctor, nurse")]
    public class EditModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;

        public EditModel(MRMS.Data.MRMSContext context)
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

            var bloodtest =  await _context.BloodTest.FirstOrDefaultAsync(m => m.BloodTestId == id);
            if (bloodtest == null)
            {
                return NotFound();
            }
            BloodTest = bloodtest;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BloodTest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BloodTestExists(BloodTest.BloodTestId))
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

        private bool BloodTestExists(int id)
        {
          return (_context.BloodTest?.Any(e => e.BloodTestId == id)).GetValueOrDefault();
        }
    }
}
