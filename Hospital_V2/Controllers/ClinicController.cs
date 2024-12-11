using Hospital_V2.Models;
using Hospital_V2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_V2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClinicController : ControllerBase
    {
        private readonly IClinicService _clinicService;

        // Constructor to inject the clinic service
        public ClinicController(IClinicService clinicService)
        {
            _clinicService = clinicService;
        }

        // Endpoint to retrieve all clinics
        [HttpGet("GetAllClinics")]
        public IActionResult GetAllClinics()
        {
            try
            {
                // Fetch all clinics from the service
                var clinics = _clinicService.GetAllClinics();

                // Check if no clinics are found
                if (clinics == null || !clinics.Any())
                {
                    return NotFound("No clinics found.");
                }

                return Ok(clinics); // Return 200 OK with the list of clinics
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return StatusCode(500, $"Error retrieving clinics: {ex.Message}");
            }
        }

        // Endpoint to add a new clinic
        [HttpPost("AddClinic")]
        public IActionResult AddClinic(
            [FromQuery] string c_Name,
            [FromQuery] string c_Specialization,
            [FromQuery] int numberOfSlots = 20)
        {
            try
            {
                // Validate input data
                if (string.IsNullOrWhiteSpace(c_Name) || string.IsNullOrWhiteSpace(c_Specialization))
                {
                    return BadRequest("Clinic name and specialization are required.");
                }

                if (numberOfSlots <= 0)
                {
                    return BadRequest("Number of slots must be greater than zero.");
                }

                // Create a new Clinic object with the provided data
                var clinic = new Clinic
                {
                    C_Name = c_Name,
                    C_Specialization = c_Specialization,
                    NumberOfSlots = numberOfSlots
                };

                // Add the clinic using the service
                _clinicService.AddClinic(clinic);

                return Ok("Clinic added successfully.");
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return StatusCode(500, $"Error adding clinic: {ex.Message}");
            }
        }
    }
}
