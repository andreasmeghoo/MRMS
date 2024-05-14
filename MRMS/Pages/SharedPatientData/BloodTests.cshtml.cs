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
    public class BloodTestsModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;
        private readonly UserManager<User> _userManager;

        public BloodTestsModel(MRMS.Data.MRMSContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<ExternalBloodTest> ExternalBloodTest { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ExternalBloodTest != null)
            {
                ExternalBloodTest = await _context.ExternalBloodTest.ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostImportDataAsync(int externalBloodTestId)
        {
            if (_context.ExternalBloodTest != null)
            {
                ExternalBloodTest = await _context.ExternalBloodTest.ToListAsync();
            }

            var patients = _userManager.GetUsersInRoleAsync("patient").Result.ToList();

            var externalBloodTest = _context.ExternalBloodTest.FirstOrDefault(e => e.ExternalBloodTestId == externalBloodTestId);

            foreach (var patient in patients)
            {
                if (
                    ExternalDataHelper.FuzzyMatch(patient.FirstName + patient.LastName, externalBloodTest.PatientName) &&
                    ExternalDataHelper.FuzzyMatch(patient.Address, externalBloodTest.PatientAddressLineOne + externalBloodTest.PatientAddressLineTwo + externalBloodTest.PatientCity)
                    )
                {
                    externalBloodTest.Imported = true;
                    externalBloodTest.MatchedPatientId = patient.Id;
                    await _context.SaveChangesAsync();
                    BloodTest newBloodTest = new BloodTest();
                    newBloodTest.ConsultationId = 999;
                    newBloodTest.Comments = externalBloodTest.Comments;
                    newBloodTest.PerformedDate = externalBloodTest.PerformedDate;
                    newBloodTest.PerformedById = "999";
                    newBloodTest.ExternalBloodTestId = externalBloodTest.ExternalBloodTestId;
                    _context.BloodTest.Add(newBloodTest);
                    await _context.SaveChangesAsync();
                    BloodTestResult newBloodTestResult = new BloodTestResult();
                    newBloodTestResult.BloodTestId = newBloodTest.BloodTestId;
                    newBloodTestResult.redBloodCellCount = externalBloodTest.redBloodCellCount;
                    newBloodTestResult.whiteBloodCellCount = externalBloodTest.whiteBloodCellCount;
                    newBloodTestResult.plateletCount = externalBloodTest.plateletCount;
                    newBloodTestResult.glucoseLevel = externalBloodTest.glucoseLevel;
                    newBloodTestResult.cholestorolLevel = externalBloodTest.cholestorolLevel;
                    newBloodTestResult.liverFunction = externalBloodTest.liverFunction;
                    newBloodTestResult.kidneyFunction = externalBloodTest.kidneyFunction;
                    _context.BloodTestResult.Add(newBloodTestResult);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("../BloodTests/Index");
                }
            }

            return Page();
        }
    }
}
