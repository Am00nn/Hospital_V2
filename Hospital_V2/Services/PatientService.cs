using Hospital_V2.Models;
using Hospital_V2.Repositories;

namespace Hospital_V2.Services
{
    // Service class for managing patient-related business logic
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IBookingRepository _bookingRepository;

        // Constructor for injecting the patient and booking repositories
        public PatientService(IPatientRepository patientRepository, IBookingRepository bookingRepository)
        {
            _patientRepository = patientRepository;
            _bookingRepository = bookingRepository;
        }

        // Adds a new patient to the system
        public void AddPatient(Patient patient)
        {
            try
            {
                // Validate the patient object
                if (patient == null || string.IsNullOrWhiteSpace(patient.P_Name) ||
                    string.IsNullOrWhiteSpace(patient.P_Age) || string.IsNullOrWhiteSpace(patient.P_Gender))
                {
                    throw new ArgumentException("Invalid patient data."); // Invalid input error
                }

                // Save the patient to the repository
                _patientRepository.AddPatient(patient);
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors
                throw new InvalidOperationException($"Error while adding patient: {ex.Message}");
            }
        }

        // Retrieves all patients from the system
        public IEnumerable<Patient> GetAllPatients()
        {
            try
            {
                // Fetch all patients
                var patients = _patientRepository.GetPatients();

                // Check if there are no patients
                if (patients == null || !patients.Any())
                {
                    throw new InvalidOperationException("No patients found.");
                }

                return patients; // Return the list of patients
            }
            catch (Exception ex)
            {
                // Handle errors during retrieval
                throw new InvalidOperationException($"Error while retrieving patients: {ex.Message}");
            }
        }

        // Retrieves all appointments for a specific patient
        public IEnumerable<Booking> GetPatientAppointments(int patientId)
        {
            try
            {
                // Validate the patient ID
                if (patientId <= 0)
                {
                    throw new ArgumentException("Invalid patient ID."); // Invalid input error
                }

                // Fetch all appointments for the specified patient
                var appointments = _bookingRepository.ViewAppointmentByPatient(patientId);

                // Check if there are no appointments
                if (appointments == null || !appointments.Any())
                {
                    throw new InvalidOperationException("No appointments found for the specified patient.");
                }

                return appointments; // Return the list of appointments
            }
            catch (Exception ex)
            {
                // Handle errors during retrieval
                throw new InvalidOperationException($"Error while retrieving patient appointments: {ex.Message}");
            }
        }
    }
}
