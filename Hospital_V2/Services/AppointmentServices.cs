using Hospital_V2.Models;
using Hospital_V2.Repositories;

namespace Hospital_V2.Services
{
    public class AppointmentServices : IAppointmentServices
    {
        private readonly IBookingRepository _bookingRepository;

        private readonly IClinicRepository _clinicRepository;

        private readonly IPatientRepository _patientRepository;

        public AppointmentServices(IBookingRepository bookingRepository, IClinicRepository clinicRepository, IPatientRepository patientRepository)
        {
            _bookingRepository = bookingRepository;

            _clinicRepository = clinicRepository;

            _patientRepository = patientRepository;
        }
        public string AddBooking(string patientName, string clinicName, DateTime bookingDate)
        {
            // Find patient and clinic using LINQ
            var patient = _patientRepository.GetPatients()
                .FirstOrDefault(p => p.P_Name.Equals(patientName, StringComparison.OrdinalIgnoreCase));

            var clinic = _clinicRepository.GetClinics()
                .FirstOrDefault(c => c.C_Name.Equals(clinicName, StringComparison.OrdinalIgnoreCase));

            // Validate patient and clinic
            if (patient == null)
                return "Invalid patient name.";

            if (clinic == null)
                return "Invalid clinic name.";

            // Check if patient already has an appointment on the same date
            var existingAppointment = _bookingRepository.ViewAppointmentByPatient(patient.P_Id)
                .Any(b => b.BookingDate.Date == bookingDate.Date && b.ClinicId == clinic.CID);

            if (existingAppointment)
                return "Patient already has an appointment in this clinic on the same day.";

            // Check if the clinic has available slots
            if (clinic.NumberOfSlots <= 0)
                return "No available slots in the clinic.";

            // Create and save the new booking
            var booking = new Booking
            {
                PatientId = patient.P_Id,
                ClinicId = clinic.CID,
                BookingDate = bookingDate
            };

            _bookingRepository.BookAppointment(booking);

            // Update the clinic's available slots
            clinic.NumberOfSlots -= 1;
            _clinicRepository.UpdateClinic(clinic);

            return $"Booking successful. Remaining slots: {clinic.NumberOfSlots}";
        }



        public IEnumerable<Booking> ViewAppointmentByClinic(string clinicName)
        {
            var clinic = _clinicRepository.GetClinics()

                .FirstOrDefault(c => c.C_Name.Equals(clinicName, StringComparison.OrdinalIgnoreCase));

            if (clinic == null)
            {
                return Enumerable.Empty<Booking>();
            }

            return _bookingRepository.ViewAppointmentByClinic(clinic.CID);
        }

        public IEnumerable<Booking> ViewAppointmentByPatient(string patientName)
        {
            var patient = _patientRepository.GetPatients()

                .FirstOrDefault(p => p.P_Name.Equals(patientName, StringComparison.OrdinalIgnoreCase));

            if (patient == null)
            {

                return Enumerable.Empty<Booking>();
            }

            return _bookingRepository.ViewAppointmentByPatient(patient.P_Id);
        }
    }
}
