using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MRMS.Data;
using MRMS.Models;

namespace MRMS.Pages.Consultations
{
    [Authorize(Roles = "doctor, admin, nurse, patient")]
    public class DetailsModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;
        private readonly UserManager<User> _userManager;

        public DetailsModel(MRMS.Data.MRMSContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Consultation Consultation { get; set; } = default!;

        public IList<Appointment> Appointments { get; set; }

        public IList<ExternalConsultation> ExternalConsultations { get; set; } = default!;
        public List<User> Doctors { get; set; }

        public List<User> Nurses { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            Doctors = _userManager.GetUsersInRoleAsync("doctor").Result.ToList();
            Nurses = _userManager.GetUsersInRoleAsync("nurse").Result.ToList();
            Appointments = _context.Appointment.ToList();
            ExternalConsultations = _context.ExternalConsultation.ToList();

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
