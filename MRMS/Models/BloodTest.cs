namespace MRMS.Models
{
    public class BloodTest
    {
        public int BloodTestId { get; set; }
        public int ConsultationId { get; set; }

        public string Comments { get; set; }

        public DateTime PerformedDate { get; set; }

        public String PerformedById { get; set; }

    }
}
