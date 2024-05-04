using System.ComponentModel.DataAnnotations;

namespace MRMS.Models
{
    public class ExternalConsultation
    {
        public int ExternalConsultationId { get; set; }

        [Display(Name = "Patient")]
        public string PatientName { get; set; }
        [Display(Name = "Address Line 1")]
        public string PatientAddressLineOne { get; set; }
        [Display(Name = "Address Line 2")]
        public string PatientAddressLineTwo { get; set; }
        [Display(Name = "Street")]
        public string PatientStreet { get; set; }
        [Display(Name = "City")]
        public string PatientCity { get; set; }

        [Display(Name = "Doctor")]
        public string DoctorName { get; set; }
        [Display(Name = "Nurse")]
        public string NurseName { get; set; }
        [Display(Name = "Patient History")]
        public String PatientHistory { get; set; }

        [Display(Name = "Exam Details")]
        public String ExamDetails { get; set; }
        [Display(Name = "Care Plan")]
        public String CarePlan { get; set; }

        public String? Comments { get; set; }

        public String Diagnosis { get; set; }

    }

}
