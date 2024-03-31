using MRMS.Enums;
using System.ComponentModel.DataAnnotations;

namespace MRMS.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PreferredDoctorId { get; set; }
        public int PatientId { get; set; }
        public String Reason { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public String? SpecialAccomodations { get; set; }

        public AppointmentStatus Status { get; set; }

    }
}
