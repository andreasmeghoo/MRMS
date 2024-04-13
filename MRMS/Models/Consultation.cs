using Microsoft.Identity.Client;

namespace MRMS.Models
{
    public class Consultation
    {
        public int ConsultationId { get; set; }
        public int AppointmentId { get; set; }
        public String DoctorId { get; set; }

        public String NurseId { get; set; }

        public String PatientHistory {  get; set; } 

        public String ExamDetails { get; set;}

        public String CarePlan { get; set; }

        public String Comments { get; set; }

        public String Diagnosis { get; set; }


    }
}
