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
    [Authorize]
    public class ResultsModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;

        public ResultsModel(MRMS.Data.MRMSContext context)
        {
            _context = context;
        }

      public BloodTestResult BloodTestResult { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BloodTestResult == null)
            {
                return NotFound();
            }

            var bloodtestresult = await _context.BloodTestResult.FirstOrDefaultAsync(m => m.BloodTestResultId == id);
            if (bloodtestresult == null)
            {
                return NotFound();
            }
            else 
            {
                BloodTestResult = bloodtestresult;
            }
            return Page();
        }
    }
}
