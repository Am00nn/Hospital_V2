using Hospital_V2.Models;

namespace Hospital_V2.Services
{
    public interface IAppointmentServices
    {
        string AddBooking(string patientName, string clinicName, DateTime bookingDate);
        IEnumerable<Booking> ViewAppointmentByClinic(string clinicName);
        IEnumerable<Booking> ViewAppointmentByPatient(string patientName);
    }
}