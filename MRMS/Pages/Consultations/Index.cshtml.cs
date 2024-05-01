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
        public String UserId { get; set; }

        public async Task OnGetAsync()
        {
            UserId = _userManager.GetUserId(User);
            

            if (_context.Consultation != null && _context.Appointment != null)
            {
                IList<Consultation> AllConsultations = await _context.Consultation.ToListAsync();
                IList<Appointment> AllAppointments = await _context.Appointment.ToListAsync();
                IList<int> CurrentUserAppointments = new List<int>();
                
                if (User.IsInRole("doctor"))
                {
                    foreach (Appointment appointment in AllAppointments)
                    {
                        if (appointment.PreferredDoctorId == UserId)
                        {
                            CurrentUserAppointments.Add(appointment.AppointmentId);
                        }
                    }
                    Consultation = new List<Consultation>();
                    foreach (Consultation consultation in AllConsultations)
                    {
                        if (CurrentUserAppointments.Contains(consultation.AppointmentId))
                        {
                            Consultation.Add(consultation);
                        }
                    }
                }
                else if (User.IsInRole("doctor"))
                {
                    foreach (Appointment appointment in AllAppointments)
                    {
                        if (appointment.PatientId == UserId)
                        {
                            CurrentUserAppointments.Add(appointment.AppointmentId);
                        }
                    }

                    Consultation = new List<Consultation>();
                    foreach (Consultation consultation in AllConsultations)
                    {
                        if(CurrentUserAppointments.Contains(consultation.AppointmentId))
                        {
                            Consultation.Add(consultation);
                        }
                    }
                }
                else
                {
                    Consultation = AllConsultations;
                }

            }

        }
    }
}
