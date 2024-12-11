using Hospital_V2.Models;

namespace Hospital_V2.Repositories
{
    public interface IPatientRepository
    {
        void AddPatient(Patient patient);
        IEnumerable<Patient> GetPatients();
    }
}