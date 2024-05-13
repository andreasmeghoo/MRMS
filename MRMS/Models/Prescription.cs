using MRMS.Enums;
using System.ComponentModel.DataAnnotations;

namespace MRMS.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        [Display(Name = "Consultation")]
        public int ConsultationId { get; set; }
        [Display(Name = "Date Prescribed")]
        public DateTime Date { get; set; }
        public string Medication { get; set; }
        [Display(Name="Dose Quantity")]
        public int DoseQuantity { get; set; }

        public DoseQuantityUnits DoseQuantityUnits { get; set; }
        [Display(Name = "Frequency")]
        public string DoseFrequency { get; set; }
        [Display(Name = "Strength")]
        public int DoseStrength { get; set; }

        public DoseStrengthUnits DoseStrengthUnits { get; set; }
        [Display(Name = "Total Quantity")]
        public int TotalQuantity { get; set; }
        public int Refills { get; set; }
        [Display(Name = "Duration (days)")]
        public int Duration { get; set; }

    }
}
