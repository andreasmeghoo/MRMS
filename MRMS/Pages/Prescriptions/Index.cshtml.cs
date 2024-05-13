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

namespace MRMS.Pages.Prescriptions
{
    [Authorize(Roles = "doctor, admin, nurse, patient")]
    public class IndexModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;
        private readonly UserManager<User> _userManager;

        public IndexModel(MRMS.Data.MRMSContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Prescription> Prescription { get;set; } = default!;
        public String UserId { get; set; }

        public IList<Consultation> AllConsultations { get; set; } = default!;

        public IList<Appointment> AllAppointments { get; set; } = default!;
        public IList<User> Patients { get; set; } = default!;

        public IList<User> Doctors { get; set; } = default!;

        public async Task OnGetAsync()
        {
            UserId = _userManager.GetUserId(User);

            if (_context.Prescription != null && _context.Consultation != null && _context.Appointment != null)
            {
                IList<Prescription> AllPrescriptions = await _context.Prescription.ToListAsync();
                AllAppointments = await _context.Appointment.ToListAsync();
                AllConsultations = await _context.Consultation.ToListAsync();
                IList<int> CurrentUserAppointments = new List<int>();
                IList<int> CurrentUserConsultations = new List<int>();
                Patients = _userManager.GetUsersInRoleAsync("patient").Result.ToList();
                Doctors = _userManager.GetUsersInRoleAsync("doctor").Result.ToList();

                if (!User.IsInRole("patient"))
                {
                    Prescription = AllPrescriptions;
                }
                else
                {
                    foreach (Appointment appointment in AllAppointments)
                    {
                        if (appointment.PatientId == UserId)
                        {
                            CurrentUserAppointments.Add(appointment.AppointmentId);
                        }
                    }

                    foreach(Consultation consultation in AllConsultations)
                    {
                        if(CurrentUserAppointments.Contains(consultation.AppointmentId))
                        {
                            CurrentUserConsultations.Add(consultation.ConsultationId);
                        }
                    }

                    Prescription = new List<Prescription>();
                    foreach(Prescription prescription in AllPrescriptions)
                    {
                        if(CurrentUserConsultations.Contains(prescription.ConsultationId))
                        {
                            Prescription.Add(prescription);
                        }
                    }
                }

            }
        }
    }
}
