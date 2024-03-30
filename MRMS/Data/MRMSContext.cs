using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MRMS.Models;

namespace MRMS.Data
{
    public class MRMSContext : IdentityDbContext<User>
    {
        public MRMSContext (DbContextOptions<MRMSContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var patient = new IdentityRole("patient");
            patient.NormalizedName = "patient";

            var receiptionist = new IdentityRole("receptionist");
            receiptionist.NormalizedName = "receptionist";

            var nurse = new IdentityRole("nurse");
            nurse.NormalizedName = "nurse";

            var doctor = new IdentityRole("doctor");
            doctor.NormalizedName = "doctor";

            builder.Entity<IdentityRole>().HasData(admin, patient, receiptionist, nurse, doctor);
        }

        public DbSet<MRMS.Models.Appointment> Appointment { get; set; } = default!;

        public DbSet<MRMS.Models.Consultation> Consultation { get; set; } = default!;

        public DbSet<MRMS.Models.Prescription> Prescription { get; set; } = default!;

        public DbSet<MRMS.Models.BloodTest> BloodTest { get; set; } = default!;

        public DbSet<MRMS.Models.BloodTestResult> BloodTestResult { get; set; } = default!;
    }
}
