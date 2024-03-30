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

namespace MRMS.Pages.Consultations
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;

        public DetailsModel(MRMS.Data.MRMSContext context)
        {
            _context = context;
        }

      public Consultation Consultation { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Consultation == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultation.FirstOrDefaultAsync(m => m.ConsultationId == id);
            if (consultation == null)
            {
                return NotFound();
            }
            else 
            {
                Consultation = consultation;
            }
            return Page();
        }
    }
}
