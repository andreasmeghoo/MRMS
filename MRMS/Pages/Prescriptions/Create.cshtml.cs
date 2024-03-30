using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MRMS.Data;
using MRMS.Models;

namespace MRMS.Pages.Prescriptions
{
    [Authorize(Roles="doctor")]
    public class CreateModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;

        public CreateModel(MRMS.Data.MRMSContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Prescription Prescription { get; set; } = default!;
        

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
