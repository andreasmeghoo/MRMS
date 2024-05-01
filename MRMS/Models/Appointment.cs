using MRMS.Enums;
using System.ComponentModel.DataAnnotations;

namespace MRMS.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        [Display(Name = "Preffered Doctor")]
        public String PreferredDoctorId { get; set; }
        [Display(Name = "Patient")]
        public String PatientId { get; set; }
        public String Reason { get; set; }
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }
        [Display(Name = "Special Accomodations")]
        public String? SpecialAccomodations { get; set; }

        public AppointmentStatus Status { get; set; }

        public bool Confirmed { get; set; }

    }
}
