using Hospital_V2.Models;

namespace Hospital_V2.Repositories
{
    public interface IClinicRepository
    {
        void AddClinic(Clinic clinic);
        Clinic GetClinicById(int clinicId);
        IEnumerable<Clinic> GetClinics();
        void UpdateClinic(Clinic clinic);
    }
}