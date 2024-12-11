using Hospital_V2.Models;

namespace Hospital_V2.Repositories
{
    public interface IBookingRepository
    {
        void BookAppointment(Booking B);
        IEnumerable<Booking> ViewAppointmentByClinic(int clinicId);
        IEnumerable<Booking> ViewAppointmentByPatient(int patientId);
    }
}