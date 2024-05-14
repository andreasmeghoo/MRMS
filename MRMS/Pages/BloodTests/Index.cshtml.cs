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

namespace MRMS.Pages.BloodTests
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

        public IList<BloodTest> BloodTest { get;set; } = default!;
        public List<User> Staff { get;set; } = new List<User>();

        public IList<Consultation> AllConsultations { get; set; } = default!;

        public IList<ExternalBloodTest> ExternalBloodTests { get; set; } = default!;

        public IList<Appointment> AllAppointments { get; set; } = default!;
        public IList<User> Patients { get; set; } = default!;

        public IList<User> Doctors { get; set; } = default!;
        public String UserId { get; set; }

        public async Task OnGetAsync()
        {
            UserId = _userManager.GetUserId(User);
            ExternalBloodTests = _context.ExternalBloodTest.ToList();
            Patients = _userManager.GetUsersInRoleAsync("patient").Result.ToList();
            Doctors = _userManager.GetUsersInRoleAsync("doctor").Result.ToList(); 
            var nurses = _userManager.GetUsersInRoleAsync("nurse").Result.ToList();
            Staff.AddRange(Doctors);
            Staff.AddRange(nurses);

            if (_context.BloodTest != null && _context.Consultation != null && _context.Appointment != null)
            {
                IList<BloodTest> AllBloodTests = await _context.BloodTest.ToListAsync();

                IList<BloodTest> CurrentUserBloodTests = (from BloodTest in _context.BloodTest
                                                          join Consultation in _context.Consultation on BloodTest.ConsultationId equals Consultation.ConsultationId into bc
                                                          from subConsultation in bc.DefaultIfEmpty()
                                                          join Appointment in _context.Appointment on subConsultation.AppointmentId equals Appointment.AppointmentId into ab
                                                          from subAppointment in ab.DefaultIfEmpty()
                                                          join ExternalBloodTest in _context.ExternalBloodTest on BloodTest.ExternalBloodTestId equals ExternalBloodTest.ExternalBloodTestId into be
                                                          from subExternalBloodTest in be.DefaultIfEmpty()
                                                          where (subAppointment != null && subAppointment.PatientId == UserId) || (subExternalBloodTest != null && subExternalBloodTest.MatchedPatientId == UserId)
                                                          select BloodTest).ToList();

                AllAppointments = await _context.Appointment.ToListAsync();
                AllConsultations = await _context.Consultation.ToListAsync();


                if (User.IsInRole("patient"))
                {
                    BloodTest = CurrentUserBloodTests;
                }
                else
                {
                    BloodTest = AllBloodTests;
                }

            }
        }
    }
}
