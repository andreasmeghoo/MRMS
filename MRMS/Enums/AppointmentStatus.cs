using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRMS.Enums
{
    public enum AppointmentStatus
    {
        [Display(Name =  "Arrival Pending")]
        ArrivalPending,

        [Display(Name = "Arrived")]
        Arrived,

        [Display(Name = "Appointment Missed")]
        MissedAppointment,

        [Display(Name = "Left Prematurely")]
        LeftPrematurely,

        [Display(Name = "Nurse Consulting Patient")]
        NurseSentInPatient,

        [Display(Name = "Nurse Consultation Completed")]
        SeenNurse,

        [Display(Name = "Doctor Consulting Patient")]
        DoctorSentInPatient,

        [Display(Name = "Appointment Completed")]
        AppointmentComplete,

        [Display(Name = "Immediate Tests Underway")]
        ImmediateTestsOrdered,

        [Display(Name = "Tests Completed")]
        TestsComplete
    }
}
