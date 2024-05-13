using System.ComponentModel.DataAnnotations;

namespace MRMS.Models
{
    public class ExternalBloodTest
    {
        public int ExternalBloodTestId { get; set; }

        [Display(Name = "Patient")]
        public string PatientName { get; set; }
        [Display(Name = "Address Line 1")]
        public string PatientAddressLineOne { get; set; }
        [Display(Name = "Address Line 2")]
        public string? PatientAddressLineTwo { get; set; }

        [Display(Name = "City")]
        public string PatientCity { get; set; }

        [Display(Name = "Doctor")]
        public string DoctorName { get; set; }
        [Display(Name = "Nurse")]
        public string NurseName { get; set; }
        public string Comments { get; set; }
        [Display(Name = "Date Performed")]
        public DateTime PerformedDate { get; set; }
        [Display(Name = "Performed By")]
        public String PerformedBy { get; set; }
        [Display(Name = "Red Blood Cell Count")]
        public decimal redBloodCellCount { get; set; }

        [Display(Name = "White Blood Cell Count")]
        public decimal whiteBloodCellCount { get; set; }
        [Display(Name = "Platelet Count")]
        public decimal plateletCount { get; set; }
        [Display(Name = "Glucose Level")]
        public decimal glucoseLevel { get; set; }
        [Display(Name = "Cholesterol Level")]
        public decimal cholestorolLevel { get; set; }
        [Display(Name = "Liver Function")]
        public decimal liverFunction { get; set; }
        [Display(Name = "Kidney Function")]
        public decimal kidneyFunction { get; set; }

        public bool Imported { get; set; }

        public string MatchedPatientId { get; set; }
    }
}
