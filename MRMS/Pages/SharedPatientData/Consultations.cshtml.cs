using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public ConsultationsModel(MRMS.Data.MRMSContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<ExternalConsultation> ExternalConsultation { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ExternalConsultation != null)
            {
                ExternalConsultation = await _context.ExternalConsultation.ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostImportDataAsync()
        {
            if (_context.ExternalConsultation != null)
            {
                ExternalConsultation = await _context.ExternalConsultation.ToListAsync();
            }

            var consultations = from Consultation in _context.Consultation
                                join Appointment in _context.Appointment on Consultation.AppointmentId equals Appointment.AppointmentId
                                join user in _userManager.Users on Appointment.PatientId equals user.Id
                                select new
                                {
                                    FirstName = user.FirstName,
                                    LastName = user.LastName,
                                    Address = user.Address,
                                    PatientHistory = Consultation.PatientHistory,
                                    ExamDetails = Consultation.ExamDetails,
                                    CarePlan = Consultation.CarePlan,
                                    Comments = Consultation.Comments,
                                    Diagnosis = Consultation.Diagnosis
                                };

              var externalConsultations = _context.ExternalConsultation.ToList();

            foreach (var externalConsultation in externalConsultations)
            {
                foreach (var consultation in consultations)
                {
                    if (
                        FuzzyMatch(consultation.FirstName + consultation.LastName, externalConsultation.PatientName) &&
                        FuzzyMatch(consultation.Address, externalConsultation.PatientAddressLineOne + externalConsultation.PatientAddressLineTwo + externalConsultation.PatientCity)
                        )
                    {
                        Consultation newConsultation = new Consultation();
                        newConsultation.ExternalConsultationId = externalConsultation.ExternalConsultationId;
                        newConsultation.AppointmentId = 999;
                        newConsultation.DoctorId = "999";
                        newConsultation.NurseId = "999";
                        newConsultation.PatientHistory = externalConsultation.PatientHistory;
                        newConsultation.ExamDetails = externalConsultation.ExamDetails;
                        newConsultation.CarePlan = externalConsultation.CarePlan;
                        newConsultation.Comments = externalConsultation.Comments;
                        newConsultation.Diagnosis = externalConsultation.Diagnosis;

                        _context.Consultation.Add(newConsultation);
                        await _context.SaveChangesAsync();
                        return RedirectToPage("../Consultations/Index");
                    }
                }
            }
            return Page();
        }

        public bool FuzzyMatch(string str1, string str2)
        {
            return LevenshteinDistance(str1, str2) < 5;
        }

        public int LevenshteinDistance(string s, string t)
        {
            int[,] d = new int[s.Length + 1, t.Length + 1];

            if (s.Length == 0)
                return t.Length;
            if (t.Length == 0)
                return s.Length;

            for (int i = 0; i <= s.Length; i++)
                d[i, 0] = i;
            for (int j = 0; j <= t.Length; j++)
                d[0, j] = j;

            for (int j = 1; j <= t.Length; j++)
            {
                for (int i = 1; i <= s.Length; i++)
                {
                    int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }

            return d[s.Length, t.Length];
        }
    }
}
