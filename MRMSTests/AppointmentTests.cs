using Moq;
using MRMS.Enums;
using MRMS.Models;
using MRMS.Pages.Appointments;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MRMSTests
{
    public class AppointmentTests
    {

        Mock<Appointment> mockAppointment;
        List<Mock<Appointment>> doctorAppointments;
        public AppointmentTests()
        {
            mockAppointment = new Mock<Appointment>();
            doctorAppointments = new List<Mock<Appointment>>();
        }

        [Fact]
        public void TestIsTimeSlotUnavailable_ShouldReturnTrue()
        {
            //Arrange
            TimeOnly startTime = new TimeOnly(10, 40, 0);
            TimeOnly endTime = new TimeOnly(10, 55, 0);
            DateOnly date = new DateOnly(2024, 5, 6);
            mockAppointment.Object.AppointmentId = 1;
            mockAppointment.Object.PreferredDoctorId = "456789hvcjbrw876";
            mockAppointment.Object.PatientId = "67895hbwnjvjri87";
            mockAppointment.Object.Reason = "Sore Throat";
            mockAppointment.Object.StartTime = new DateTime(2024, 5, 6, 10, 30, 0);
            mockAppointment.Object.EndTime = new DateTime(2024, 5, 6, 10, 45, 0);
            mockAppointment.Object.Status = AppointmentStatus.ArrivalPending;
            mockAppointment.Object.Confirmed = true;
            doctorAppointments.Add(mockAppointment);
            bool result = false;

            //Act
            foreach (var appointment in doctorAppointments)
            {
                DateTime slotStartDateTime = date.ToDateTime(startTime);
                DateTime slotEndDateTime = date.ToDateTime(endTime);
                if (appointment.Object.StartTime < slotEndDateTime && appointment.Object.EndTime > slotStartDateTime)
                {
                    result = true;
                }
            }
            
            //Assert
            Assert.Equal(result, true);


        }

        [Fact]
        public void TestIsTimeSlotUnavailable_ShouldReturnFalse()
        {
            //Arrange
            TimeOnly startTime = new TimeOnly(10, 0, 0);
            TimeOnly endTime = new TimeOnly(10, 30, 0);
            DateOnly date = new DateOnly(2024, 5, 6);
            mockAppointment.Object.AppointmentId = 1;
            mockAppointment.Object.PreferredDoctorId = "456789hvcjbrw876";
            mockAppointment.Object.PatientId = "67895hbwnjvjri87";
            mockAppointment.Object.Reason = "Sore Throat";
            mockAppointment.Object.StartTime = new DateTime(2024, 5, 6, 10, 30, 0);
            mockAppointment.Object.EndTime = new DateTime(2024, 5, 6, 10, 45, 0);
            mockAppointment.Object.Status = AppointmentStatus.ArrivalPending;
            mockAppointment.Object.Confirmed = true;
            doctorAppointments.Add(mockAppointment);
            bool result = false;

            //Act
            foreach (var appointment in doctorAppointments)
            {
                DateTime slotStartDateTime = date.ToDateTime(startTime);
                DateTime slotEndDateTime = date.ToDateTime(endTime);
                if (appointment.Object.StartTime < slotEndDateTime && appointment.Object.EndTime > slotStartDateTime)
                {
                    result = true;
                }
            }

            //Assert
            Assert.Equal(result, false);

        }

        [Fact]
        public void TestGenerateTimeSlots()
        {
            //Arrange
            List<(TimeOnly, TimeOnly)> timeSlots = new List<(TimeOnly, TimeOnly)>();

            TimeOnly morningStart = new TimeOnly(8, 0);
            TimeOnly morningEnd = new TimeOnly(13, 0);
            TimeOnly afternoonStart = new TimeOnly(14, 0);
            TimeOnly afternoonEnd = new TimeOnly(18, 0);
            DateOnly date = new DateOnly(2024, 5, 6);
            mockAppointment.Object.AppointmentId = 1;
            mockAppointment.Object.PreferredDoctorId = "456789hvcjbrw876";
            mockAppointment.Object.PatientId = "67895hbwnjvjri87";
            mockAppointment.Object.Reason = "Sore Throat";
            mockAppointment.Object.StartTime = new DateTime(2024, 5, 6, 10, 30, 0);
            mockAppointment.Object.EndTime = new DateTime(2024, 5, 6, 10, 45, 0);
            mockAppointment.Object.Status = AppointmentStatus.ArrivalPending;
            mockAppointment.Object.Confirmed = true;
            doctorAppointments.Add(mockAppointment);
            bool unavailable = false;

            //Act

            TimeOnly i = morningStart;
            while (i < morningEnd)
            {
                foreach (var appointment in doctorAppointments)
                {
                    DateTime slotStartDateTime = date.ToDateTime(i);
                    DateTime slotEndDateTime = date.ToDateTime(i.AddMinutes(15));
                    if (appointment.Object.StartTime < slotEndDateTime && appointment.Object.EndTime > slotStartDateTime)
                    {
                        unavailable = true;
                    }
                }

                if (!unavailable)
                {
                    timeSlots.Add((i, i.AddMinutes(15)));
                }
                i = i.AddMinutes(15);
                unavailable = false;
            }

            i = afternoonStart;
            while (i < afternoonEnd)
            {
                foreach (var appointment in doctorAppointments)
                {
                    DateTime slotStartDateTime = date.ToDateTime(i);
                    DateTime slotEndDateTime = date.ToDateTime(i.AddMinutes(15));
                    if (appointment.Object.StartTime < slotEndDateTime && appointment.Object.EndTime > slotStartDateTime)
                    {
                        unavailable = true;
                    }
                }

                if (!unavailable)
                {
                    timeSlots.Add((i, i.AddMinutes(15)));
                }
                i = i.AddMinutes(15);
                unavailable = false;
            }

            //Assert
            Assert.DoesNotContain((new TimeOnly(10, 30), new TimeOnly(10, 45)), timeSlots);
            Assert.Equal(35, timeSlots.Count);

        }
    }
}