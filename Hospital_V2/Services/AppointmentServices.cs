﻿using Hospital_V2.Models;
using Hospital_V2.Repositories;
using Hospital_V2.Services;

namespace Hospital_V2.Services
{
    public class AppointmentServices : IAppointmentServices
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IClinicService _clinicService;
        private readonly IPatientService _patientService;

        public AppointmentServices(IBookingRepository bookingRepository, IClinicService clinicService, IPatientService patientService)
        {
            _bookingRepository = bookingRepository;
            _clinicService = clinicService;
            _patientService = patientService;
        }

        public string AddBooking(string patientName, string clinicName, DateTime bookingDate)
        {
            try
            {
                // Retrieve patient and clinic using services
                var patient = _patientService.GetAllPatients()
                    .FirstOrDefault(p => p.P_Name.Equals(patientName, StringComparison.OrdinalIgnoreCase));

                var clinic = _clinicService.GetAllClinics()
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
                _clinicService.UpdateClinic(clinic);

                return $"Booking successful. Remaining slots: {clinic.NumberOfSlots}";
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return $"An error occurred while booking the appointment: {ex.Message}";
            }
        }

        public IEnumerable<Booking> ViewAppointmentByClinic(string clinicName)
        {
            try
            {
                var clinic = _clinicService.GetAllClinics()
                    .FirstOrDefault(c => c.C_Name.Equals(clinicName, StringComparison.OrdinalIgnoreCase));

                if (clinic == null)
                {
                    return Enumerable.Empty<Booking>(); // Return empty if clinic not found
                }

                return _bookingRepository.ViewAppointmentByClinic(clinic.CID);
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                throw new InvalidOperationException($"An error occurred while retrieving appointments: {ex.Message}");
            }
        }

        public IEnumerable<Booking> ViewAppointmentByPatient(string patientName)
        {
            try
            {
                var patient = _patientService.GetAllPatients()
                    .FirstOrDefault(p => p.P_Name.Equals(patientName, StringComparison.OrdinalIgnoreCase));

                if (patient == null)
                {
                    return Enumerable.Empty<Booking>(); // Return empty if patient not found
                }

                return _bookingRepository.ViewAppointmentByPatient(patient.P_Id);
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                throw new InvalidOperationException($"An error occurred while retrieving patient appointments: {ex.Message}");
            }
        }
    }
}
