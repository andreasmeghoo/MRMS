using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MRMS.Data;
using MRMS.Models;

namespace MRMS.Pages.SharedPatientData
{
    public class BloodTestsModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;

        public BloodTestsModel(MRMS.Data.MRMSContext context)
        {
            _context = context;
        }

        public IList<ExternalBloodTest> ExternalBloodTest { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ExternalBloodTest != null)
            {
                ExternalBloodTest = await _context.ExternalBloodTest.ToListAsync();
            }
        }
    }
}
