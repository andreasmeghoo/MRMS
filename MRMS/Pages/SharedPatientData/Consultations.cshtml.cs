using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MRMS.Data;
using MRMS.Helpers;
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

        public IList<ExternalConsultation> ExternalConsultation { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ExternalConsultation != null)
            {
                ExternalConsultation = await _context.ExternalConsultation.ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostImportDataAsync(int externalConsultationId)
        {
            if (_context.ExternalConsultation != null)
            {
                ExternalConsultation = await _context.ExternalConsultation.ToListAsync();
            }

            var patients = _userManager.GetUsersInRoleAsync("patient").Result.ToList();

            var externalConsultation = _context.ExternalConsultation.FirstOrDefault(e => e.ExternalConsultationId == externalConsultationId);

            foreach (var patient in patients)
            {
                if (
                    ExternalDataHelper.FuzzyMatch(patient.FirstName + patient.LastName, externalConsultation.PatientName) &&
                    ExternalDataHelper.FuzzyMatch(patient.Address, externalConsultation.PatientAddressLineOne + externalConsultation.PatientAddressLineTwo + externalConsultation.PatientCity)
                    )
                {
                    externalConsultation.Imported = true;
                    await _context.SaveChangesAsync();
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
                    externalConsultation.MatchedPatientId = patient.Id;
                    _context.Consultation.Add(newConsultation);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("../Consultations/Index");
                }
            }
            ModelState.AddModelError("consultationNoMatch", "No matching patient found for this external consultation.");
            return Page();
        }
    }
}
