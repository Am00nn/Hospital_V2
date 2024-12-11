using Hospital_V2.Models;

namespace Hospital_V2.Services
{
    public interface IClinicService
    {
        void AddClinic(Clinic clinic);
        IEnumerable<Clinic> GetAllClinics();
        void UpdateClinic(Clinic clinic);
    }
}