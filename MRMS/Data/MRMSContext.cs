using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MRMS.Models;

namespace MRMS.Data
{
    public class MRMSContext : DbContext
    {
        public MRMSContext (DbContextOptions<MRMSContext> options)
            : base(options)
        {
        }

        public DbSet<MRMS.Models.Appointment> Appointment { get; set; } = default!;

        public DbSet<MRMS.Models.Consultation> Consultation { get; set; } = default!;

        public DbSet<MRMS.Models.Prescription> Prescription { get; set; } = default!;

        public DbSet<MRMS.Models.BloodTest> BloodTest { get; set; } = default!;

        public DbSet<MRMS.Models.BloodTestResult> BloodTestResult { get; set; } = default!;
    }
}
