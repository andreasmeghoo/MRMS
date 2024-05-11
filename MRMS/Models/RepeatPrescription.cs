using System.ComponentModel.DataAnnotations;

namespace MRMS.Models
{
    public class RepeatPrescription
    {
        public int RepeatPrescriptionId { get; set; }
        public int PrescriptionId { get; set; }
        [Display(Name = "Date Requested")]
        public DateTime DateRequested { get; set; }

        public string Reason { get; set; }

        public bool Granted { get; set; }

    }
}
