using Hospital_V2.Models;

namespace Hospital_V2.Services
{
    public interface IPatientService
    {
        void AddPatient(Patient patient);
        IEnumerable<Patient> GetAllPatients();
        IEnumerable<Booking> GetPatientAppointments(int patientId);
    }
}