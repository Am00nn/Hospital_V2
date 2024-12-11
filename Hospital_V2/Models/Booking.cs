using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hospital_V2.Models
{
    public class Booking
    {
        [Key]
        [JsonIgnore]
        public int BookingId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int ClinicId { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [JsonIgnore]
        public Patient? Patient { get; set; }
        [JsonIgnore]
        public Clinic? Clinic { get; set; }
    }
}
