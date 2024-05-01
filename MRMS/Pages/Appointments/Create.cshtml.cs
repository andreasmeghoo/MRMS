using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Scripting;
using MRMS.Data;
using MRMS.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MRMS.Pages.Appointments
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly MRMS.Data.MRMSContext _context;
        private readonly UserManager<User> _userManager;

        public CreateModel(MRMS.Data.MRMSContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = default!;

        public List<User> Doctors { get; set; }

        public List<(TimeOnly StartTime, TimeOnly EndTime)> AvailableTimeSlots { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        public IActionResult OnGet()
        {
            var doctors = _userManager.GetUsersInRoleAsync("doctor").Result.ToList();
            Doctors = doctors;
            if (User.IsInRole("patient"))
            {
                Appointment = new Appointment();
                Appointment.PatientId = _userManager.GetUserId(User);
            }         
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string time, string date)
        {
            var doctors = _userManager.GetUsersInRoleAsync("doctor").Result.ToList();
            Doctors = doctors;
            if (!ModelState.IsValid || _context.Appointment == null || Appointment == null)
            {
                return Page();
            }
            if (User.IsInRole("patient"))
            {
                Appointment.PatientId = _userManager.GetUserId(User);
            }
            Time = TimeOnly.TryParse(time, out TimeOnly parsedTime) ? parsedTime : default;
            Date = DateOnly.TryParse(date, out DateOnly parsedDate) ? parsedDate : default;
            DateTime startTime = Date.ToDateTime(Time);
            Appointment.StartTime = startTime;
            Appointment.EndTime = startTime.AddMinutes(15);
            Appointment.Confirmed = true;
            _context.Appointment.Add(Appointment);
            await _context.SaveChangesAsync();
           
            return RedirectToPage("./Index");
        }

        public IActionResult OnPostDoctorDateSelection(string doctorId, string date)
        {
            var doctors = _userManager.GetUsersInRoleAsync("doctor").Result.ToList();
            Doctors = doctors;
            if (User.IsInRole("patient"))
            {
                Appointment.PatientId = _userManager.GetUserId(User);
                Date = DateOnly.TryParse(date, out DateOnly parsedDate) ? parsedDate : default;
                ModelState.ClearValidationState("Appointment.Reason");

                AvailableTimeSlots = GenerateTimeSlots(doctorId, Date);
            }         
            return Page();
        }

        private List<(TimeOnly, TimeOnly)> GenerateTimeSlots(string doctorId, DateOnly date)
        {
            List<(TimeOnly, TimeOnly)> timeSlots = new List<(TimeOnly, TimeOnly)>();

            TimeOnly morningStart = new TimeOnly(8, 0);
            TimeOnly morningEnd = new TimeOnly(13, 0);
            TimeOnly afternoonStart = new TimeOnly(14, 0);
            TimeOnly afternoonEnd = new TimeOnly(18, 0);

            var doctorAppointments = _context.Appointment
                                        .Where(a => a.PreferredDoctorId == doctorId &&
                                        a.StartTime.Date == date.ToDateTime(new TimeOnly(0, 0)).Date)
                                        .ToList();

            TimeOnly i = morningStart;
            while (i < morningEnd)
            {
                if (!IsTimeSlotBooked(doctorAppointments, i, i.AddMinutes(15),date))
                {
                    timeSlots.Add((i, i.AddMinutes(15)));
                }
                i = i.AddMinutes(15);
            }

            i = afternoonStart;
            while (i < afternoonEnd)
            {
                if (!IsTimeSlotBooked(doctorAppointments, i, i.AddMinutes(15), date))
                {
                    timeSlots.Add((i, i.AddMinutes(15)));
                }
                i = i.AddMinutes(15);
            }

            return timeSlots;
        }

        private bool IsTimeSlotBooked(List<Appointment> doctorAppointments, TimeOnly startTime, TimeOnly endTime, DateOnly date)
        {
            foreach (var appointment in doctorAppointments)
            {
                DateTime slotStartDateTime = date.ToDateTime(startTime);
                DateTime slotEndDateTime = date.ToDateTime(endTime);
                if (appointment.StartTime < slotEndDateTime && appointment.EndTime > slotStartDateTime)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
