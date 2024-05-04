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
    public class ConsultationsModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;

        public ConsultationsModel(MRMS.Data.MRMSContext context)
        {
            _context = context;
        }

        public IList<ExternalConsultation> ExternalConsultation { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ExternalConsultation != null)
            {
                ExternalConsultation = await _context.ExternalConsultation.ToListAsync();
            }
        }
    }
}
