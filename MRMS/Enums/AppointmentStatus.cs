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

    public class DisplayActionAttribute : Attribute
    {
        public string Name { get; }

        public DisplayActionAttribute(string name)
        {
            Name = name;
        }
    }

    public class DisplayStatusAttribute : Attribute
    {
        public string Name { get; }

        public DisplayStatusAttribute(string name)
        {
            Name = name;
        }
    }
    public enum AppointmentStatus
    {
        [DisplayAction("Arrival Pending"), DisplayStatus("Arrival Pending")]
        [RoleAccess("admin"), RoleAccess("receptionist"), RoleAccess("doctor"), RoleAccess("nurse")]
        ArrivalPending,

        [DisplayAction("Arrived"), DisplayStatus("Arrived")]
        [RoleAccess("admin"), RoleAccess("receptionist"),  RoleAccess("doctor"), RoleAccess("nurse")]
        Arrived,

        [DisplayAction("Appointment Missed"), DisplayStatus("Appointment Missed")]
        [RoleAccess("admin"), RoleAccess("receptionist"), RoleAccess("doctor"), RoleAccess("nurse")]
        MissedAppointment,

        [DisplayAction("Left Prematurely"), DisplayStatus("Left Prematurely")]
        [RoleAccess("admin"), RoleAccess("receptionist"),  RoleAccess("doctor"), RoleAccess("nurse")]
        LeftPrematurely,

        [DisplayAction("Send In Patient"), DisplayStatus("Nurse Sent In Patient")]
        [RoleAccess("admin"), RoleAccess("nurse")]
        NurseSentInPatient,

        [DisplayAction("Finish Consultation"), DisplayStatus("Nurse Consultation Completed")]
        [RoleAccess("admin"), RoleAccess("nurse")]
        SeenNurse,

        [DisplayAction("Send In Patient"), DisplayStatus("Doctor Sent In Patient")]
        [RoleAccess("admin"), RoleAccess("doctor")]
        DoctorSentInPatient,

        [DisplayAction("Finish Appointment"), DisplayStatus("Appointment Completed")]
        [RoleAccess("admin"), RoleAccess("receptionist"), RoleAccess("doctor")]
        AppointmentComplete,

        [DisplayAction("Order Immediate Tests"), DisplayStatus("Immediate Tests Ordered")]
        [RoleAccess("admin"), RoleAccess("doctor"), RoleAccess("nurse")]
        ImmediateTestsOrdered,

        [DisplayAction("Finish Testing"), DisplayStatus("Tests Completed")]
        [RoleAccess("admin"), RoleAccess("doctor"), RoleAccess("nurse")]
        TestsComplete
    }
}
