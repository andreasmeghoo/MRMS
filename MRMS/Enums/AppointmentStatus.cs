using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MRMS.Enums
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class RoleAccessAttribute : Attribute
    {
        public string Role { get; set; }

        public RoleAccessAttribute(string role)
        {
            Role = role;
        }
    }
    public enum AppointmentStatus
    {
        [Display(Name =  "Arrival Pending")]
        [RoleAccess("admin"), RoleAccess("receptionist"), RoleAccess("doctor"), RoleAccess("nurse")]
        ArrivalPending,

        [Display(Name = "Arrived")]
        [RoleAccess("admin"), RoleAccess("receptionist"),  RoleAccess("doctor"), RoleAccess("nurse")]
        Arrived,

        [Display(Name = "Appointment Missed")]
        [RoleAccess("admin"), RoleAccess("receptionist"), RoleAccess("doctor"), RoleAccess("nurse")]
        MissedAppointment,

        [Display(Name = "Left Prematurely")]
        [RoleAccess("admin"), RoleAccess("receptionist"),  RoleAccess("doctor"), RoleAccess("nurse")]
        LeftPrematurely,

        [Display(Name = "Nurse Consulting Patient")]
        [RoleAccess("admin"), RoleAccess("nurse")]
        NurseSentInPatient,

        [Display(Name = "Nurse Consultation Completed")]
        [RoleAccess("admin"), RoleAccess("nurse")]
        SeenNurse,

        [Display(Name = "Doctor Consulting Patient")]
        [RoleAccess("admin"), RoleAccess("doctor")]
        DoctorSentInPatient,

        [Display(Name = "Appointment Completed")]
        [RoleAccess("admin"), RoleAccess("receptionist"), RoleAccess("doctor")]
        AppointmentComplete,

        [Display(Name = "Immediate Tests Underway")]
        [RoleAccess("admin"), RoleAccess("doctor"), RoleAccess("nurse")]
        ImmediateTestsOrdered,

        [Display(Name = "Tests Completed")]
        [RoleAccess("admin"), RoleAccess("doctor"), RoleAccess("nurse")]
        TestsComplete
    }
}
