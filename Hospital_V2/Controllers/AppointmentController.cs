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

        [HttpPost("BookAppointment")]
        public IActionResult BookAppointment(string patientName, string clinicName, DateTime bookingDate)
        {
            var result = _appointmentService.AddBooking(patientName, clinicName, bookingDate);


            return Ok(result);
        }

        [HttpGet("GetByClinic/{clinicName}")]
        public IActionResult GetByClinic(string clinicName)
        {
            var appointments = _appointmentService.ViewAppointmentByClinic(clinicName);

            return Ok(appointments);
        }

        [HttpGet("GetByPatient/{patientName}")]
        public IActionResult GetByPatient(string patientName)
        {
            var appointments = _appointmentService.ViewAppointmentByPatient(patientName);


            return Ok(appointments);
        }
    }
}
