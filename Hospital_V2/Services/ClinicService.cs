using Hospital_V2.Models;
using Hospital_V2.Repositories;

namespace Hospital_V2.Services
{
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepository _clinicRepository;

        // Constructor for injecting the clinic repository
        public ClinicService(IClinicRepository clinicRepository)
        {
            _clinicRepository = clinicRepository;
        }

        // Retrieves all clinics
        public IEnumerable<Clinic> GetAllClinics()
        {
            try
            {
                // Fetch all clinics from the repository
                var clinics = _clinicRepository.GetClinics();

                // Validate if clinics list is null or empty
                if (clinics == null || !clinics.Any())
                {
                    throw new InvalidOperationException("No clinics found.");
                }

                return clinics; // Return the list of clinics
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                throw new InvalidOperationException($"Error retrieving clinics: {ex.Message}");
            }
        }

        // Adds a new clinic to the system
        public void AddClinic(Clinic clinic)
        {
            try
            {
                // Validate the clinic object
                if (clinic == null)
                {
                    throw new ArgumentException("Clinic data cannot be null.");
                }

                if (string.IsNullOrWhiteSpace(clinic.C_Name))
                {
                    throw new ArgumentException("Clinic name is required.");
                }

                if (string.IsNullOrWhiteSpace(clinic.C_Specialization))
                {
                    throw new ArgumentException("Clinic specialization is required.");
                }

                if (clinic.NumberOfSlots <= 0)
                {
                    throw new ArgumentException("Number of slots must be greater than zero.");
                }

                // Add the validated clinic to the repository
                _clinicRepository.AddClinic(clinic);
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                throw new InvalidOperationException($"Error adding clinic: {ex.Message}");
            }
        }
    }
}
