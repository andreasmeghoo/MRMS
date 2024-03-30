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

namespace MRMS.Pages.Consultations
{
    [Authorize(Roles = "doctor, admin, nurse")]
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
