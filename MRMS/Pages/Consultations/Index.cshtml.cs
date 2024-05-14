using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MRMS.Data;
using MRMS.Models;
using MRMS.Pages.Appointments;

namespace MRMS.Pages.Consultations
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;
        private readonly UserManager<User> _userManager;

        public IndexModel(MRMS.Data.MRMSContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Consultation> Consultation { get;set; } = default!;

        public IList<ExternalConsultation> ExternalConsultations { get;set; } = default!;

        public IList<Appointment> Appointments { get; set; }

        public IList<User> Patients { get; set; }

        public List<User> Doctors { get; set; }

        public List<User> Nurses { get; set; }
        public String UserId { get; set; }

        public async Task OnGetAsync()
        {
            UserId = _userManager.GetUserId(User);
            Doctors = _userManager.GetUsersInRoleAsync("doctor").Result.ToList();
            Nurses = _userManager.GetUsersInRoleAsync("nurse").Result.ToList();
            Patients = _userManager.GetUsersInRoleAsync("patient").Result.ToList();
            ExternalConsultations = _context.ExternalConsultation.ToList();

            if (_context.Consultation != null && _context.Appointment != null)
            {
                if (_context.Appointment != null)
                {
                    Appointments = _context.Appointment.ToList();
                }
                IList<Consultation> AllConsultations = await _context.Consultation.ToListAsync();
                IList<Consultation> CurrentUserConsultations = (from Consultation in _context.Consultation
                                                                join Appointment in _context.Appointment on Consultation.AppointmentId equals Appointment.AppointmentId into ap
                                                                from subAppointment in ap.DefaultIfEmpty()
                                                                join ExternalConsultation in _context.ExternalConsultation on Consultation.ExternalConsultationId equals ExternalConsultation.ExternalConsultationId into ec
                                                                from subExternalConsultation in ec.DefaultIfEmpty()
                                                                where (subAppointment != null && subAppointment.PatientId == UserId) || (subExternalConsultation != null && subExternalConsultation.MatchedPatientId == UserId)
                                                                select Consultation).ToList();

                if (User.IsInRole("patient"))
                { 
                
                    Consultation = CurrentUserConsultations;
                }
                else
                {
                    Consultation = AllConsultations;
                }

            }

        }
    }
}
