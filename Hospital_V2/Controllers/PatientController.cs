using Hospital_V2.Models;
using Hospital_V2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_V2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        // Constructor for injecting the patient service
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // Retrieves all patients from the system
        [HttpGet("GetAllPatients")]
        public IActionResult GetAllPatients()
        {
            try
            {
                var patients = _patientService.GetAllPatients();

                // Check if no patients are found
                if (patients == null || !patients.Any())
                {
                    return NotFound("No patients found.");
                }

                return Ok(patients); // Return the list of patients
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return StatusCode(500, $"An error occurred while retrieving patients: {ex.Message}");
            }
        }

        // Adds a new patient to the system
        [HttpPost("AddPatient")]
        public IActionResult AddPatient(
            [FromQuery] string p_Name,
            [FromQuery] string p_Age,
            [FromQuery] string p_Gender)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(p_Name) || string.IsNullOrWhiteSpace(p_Age) || string.IsNullOrWhiteSpace(p_Gender))
                {
                    return BadRequest("Invalid input: Name, Age, and Gender are required.");
                }

                // Create a new patient object
                var patient = new Patient
                {
                    P_Name = p_Name,
                    P_Age = p_Age,
                    P_Gender = p_Gender
                };

                // Add the patient
                _patientService.AddPatient(patient);

                return Ok("Patient added successfully.");
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return StatusCode(500, $"An error occurred while adding the patient: {ex.Message}");
            }
        }

        // Retrieves all appointments for a specific patient
        [HttpGet("GetAppointmentsByPatient/{patientId}")]
        public IActionResult GetAppointmentsByPatient(int patientId)
        {
            try
            {
                // Validate patient ID
                if (patientId <= 0)
                {
                    return BadRequest("Invalid patient ID.");
                }

                // Get appointments
                var appointments = _patientService.GetPatientAppointments(patientId);

                // Check if no appointments are found
                if (appointments == null || !appointments.Any())
                {
                    return NotFound("No appointments found for the specified patient.");
                }

                return Ok(appointments); // Return the list of appointments
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return StatusCode(500, $"An error occurred while retrieving appointments: {ex.Message}");
            }
        }
    }
}
