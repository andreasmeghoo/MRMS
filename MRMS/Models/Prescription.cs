namespace MRMS.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public int ConsultationId { get; set; }
        public DateTime Date { get; set; }
        public string Medication { get; set; }
        public int DoseAmount { get; set; }
        public string DoseFrequency { get; set; }
        public int Strength { get; set; }

        public int TotalQuantity { get; set; }
        public int Refills { get; set; }

        public int Duration { get; set; }

    }
}
