using Hospital_V2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_V2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentServices _appointmentService;

        public AppointmentController(IAppointmentServices appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // Endpoint to book an appointment
        [HttpPost("BookAppointment")]
        public IActionResult BookAppointment(string patientName, string clinicName, DateTime bookingDate)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(patientName))
                    return BadRequest("Patient name is required.");
                if (string.IsNullOrWhiteSpace(clinicName))
                    return BadRequest("Clinic name is required.");
                if (bookingDate == default)
                    return BadRequest("Booking date is invalid.");

                // Attempt to book the appointment
                var result = _appointmentService.AddBooking(patientName, clinicName, bookingDate);

                // Check for failure messages from the service
                if (result.Contains("not found") || result.Contains("No available slots"))
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return StatusCode(500, $"Error booking appointment: {ex.Message}");
            }
        }

        // Endpoint to get appointments by clinic name
        [HttpGet("GetByClinic/{clinicName}")]
        public IActionResult GetByClinic(string clinicName)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(clinicName))
                    return BadRequest("Clinic name is required.");

                // Retrieve appointments

                var appointments = _appointmentService.ViewAppointmentByClinic(clinicName);

                // Check if no appointments are found

                if (appointments == null || !appointments.Any())
                    return NotFound("No appointments found for the specified clinic.");

                return Ok(appointments);
            }
            catch (Exception ex)
            {
                // Handle unexpected errors

                return StatusCode(500, $"Error retrieving appointments: {ex.Message}");
            }
        }

        // Endpoint to get appointments by patient name

        [HttpGet("GetByPatient/{patientName}")]
        public IActionResult GetByPatient(string patientName)
        {
            try
            {
                // Validate input

                if (string.IsNullOrWhiteSpace(patientName))

                    return BadRequest("Patient name is required.");

                // Retrieve appointments

                var appointments = _appointmentService.ViewAppointmentByPatient(patientName);

                // Check if no appointments are found

                if (appointments == null || !appointments.Any())

                    return NotFound("No appointments found for the specified patient.");

                return Ok(appointments);
            }
            catch (Exception ex)
            {

                // Handle unexpected errors


                return StatusCode(500, $"Error retrieving appointments: {ex.Message}");
            }
        }
    }
}
