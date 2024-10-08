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

namespace MRMS.Pages.Appointments
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

        public IList<Appointment> Appointment { get;set; } = default!;
        public string UserId { get; set; }

        public List<User> Doctors { get; set; }

        public List<User> Patients { get; set; }


        public async Task OnGetAsync()
        {
            UserId = _userManager.GetUserId(User);
            Doctors = _userManager.GetUsersInRoleAsync("doctor").Result.ToList();
            Patients = _userManager.GetUsersInRoleAsync("patient").Result.ToList();
            if (_context.Appointment != null)
            {
                Appointment = await _context.Appointment.ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int id, Enums.AppointmentStatus newStatus)
        {
            var appointment = _context.Appointment.Single(x => x.AppointmentId == id);
            if (appointment != null)
            {
                appointment.Status = newStatus;
                _context.SaveChanges();
                if (_context.Appointment != null)
                {
                    Appointment = _context.Appointment.ToList();
                }
            }

            UserId = _userManager.GetUserId(User);
            Doctors = _userManager.GetUsersInRoleAsync("doctor").Result.ToList();
            Patients = _userManager.GetUsersInRoleAsync("patient").Result.ToList();
            if (_context.Appointment != null)
            {
                Appointment = await _context.Appointment.ToListAsync();
            }

            return Page();
        }
    }
}
