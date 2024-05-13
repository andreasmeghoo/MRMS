using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace MRMS.Models
{
    public class Consultation
    {
        public int ConsultationId { get; set; }
        [Display(Name = "Appointment")]
        public int? ExternalConsultationId { get; set; }
        public int AppointmentId { get; set; }
        [Display(Name = "Doctor")]
        public String DoctorId { get; set; }
        [Display(Name = "Nurse")]
        public String NurseId { get; set; }
        [Display(Name = "Patient History")]
        public String PatientHistory {  get; set; } 

        [Display(Name="Exam Details")]
        public String ExamDetails { get; set;}
        [Display(Name = "Care Plan")]
        public String CarePlan { get; set; }

        public String? Comments { get; set; }

        public String Diagnosis { get; set; }

     
    }
}
