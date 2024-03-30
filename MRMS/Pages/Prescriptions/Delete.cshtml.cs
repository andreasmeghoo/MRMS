﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MRMS.Data;
using MRMS.Models;

namespace MRMS.Pages.Prescriptions
{
    [Authorize(Roles = "doctor, admin")]
    public class DeleteModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;

        public DeleteModel(MRMS.Data.MRMSContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Prescription Prescription { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Prescription == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescription.FirstOrDefaultAsync(m => m.PrescriptionId == id);

            if (prescription == null)
            {
                return NotFound();
            }
            else 
            {
                Prescription = prescription;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Prescription == null)
            {
                return NotFound();
            }
            var prescription = await _context.Prescription.FindAsync(id);

            if (prescription != null)
            {
                Prescription = prescription;
                _context.Prescription.Remove(Prescription);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
