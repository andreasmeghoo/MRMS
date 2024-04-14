﻿using System;
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
        public String UserId { get; set; }

        public async Task OnGetAsync()
        {
            UserId = _userManager.GetUserId(User);

            if (_context.BloodTest != null && _context.Consultation != null && _context.Appointment != null)
            {
                IList<BloodTest> AllBloodTests = await _context.BloodTest.ToListAsync();
                IList<Appointment> AllAppointments = await _context.Appointment.ToListAsync();
                IList<Consultation> AllConsultations = await _context.Consultation.ToListAsync();
                IList<int> CurrentUserAppointments = new List<int>();
                IList<int> CurrentUserConsultations = new List<int>();

                if (!User.IsInRole("patient"))
                {
                    BloodTest = AllBloodTests;
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

                    foreach (Consultation consultation in AllConsultations)
                    {
                        if (CurrentUserAppointments.Contains(consultation.AppointmentId))
                        {
                            CurrentUserConsultations.Add(consultation.ConsultationId);
                        }
                    }

                    BloodTest = new List<BloodTest>();
                    foreach (BloodTest bloodTest in AllBloodTests)
                    {
                        if (CurrentUserConsultations.Contains(bloodTest.ConsultationId))
                        {
                            BloodTest.Add(bloodTest);
                        }
                    }
                }

            }
        }
    }
}
