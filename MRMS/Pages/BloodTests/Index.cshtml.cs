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
    public class IndexModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;

        public IndexModel(MRMS.Data.MRMSContext context)
        {
            _context = context;
        }

        public IList<BloodTest> BloodTest { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.BloodTest != null)
            {
                BloodTest = await _context.BloodTest.ToListAsync();
            }
        }
    }
}
