using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MRMS.Data;
using MRMS.Models;

namespace MRMS.Pages.Prescriptions
{
    public class RepeatModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;

        public RepeatModel(MRMS.Data.MRMSContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            prescription = await _context.Prescription.FirstOrDefaultAsync(m => m.PrescriptionId == id);
            RepeatPrescription = new RepeatPrescription();
            RepeatPrescription.PrescriptionId = prescription.PrescriptionId;
            return Page();
        }

        [BindProperty]
        public RepeatPrescription RepeatPrescription { get; set; } = default!;
        public Prescription prescription { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
 
            if (!ModelState.IsValid || _context.RepeatPrescription == null || RepeatPrescription == null)
            {
                return Page();
            }
            _context.RepeatPrescription.Add(RepeatPrescription);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
