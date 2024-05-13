using System.ComponentModel.DataAnnotations;

namespace MRMS.Models
{
    public class BloodTest
    {
        public int BloodTestId { get; set; }
        [Display(Name = "Consultation")]
        public int ConsultationId { get; set; }

        public string Comments { get; set; }
        [Display(Name = "Date Performed")]
        public DateTime PerformedDate { get; set; }
        [Display(Name = "Performed By")]
        public String PerformedById { get; set; }

        public int ExternalConsultationId { get; set; }

    }
}
