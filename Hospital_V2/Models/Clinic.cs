using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
namespace Hospital_V2.Models
{
    public class Clinic
    {
        [Key]
        public int CID { get; set; }

        [Required]
        public string C_Name { get; set; }

        [Required]
        public string C_Specialization { get; set; }

        [Required]
        public int NumberOfSlots { get; set; } = 20;


        [JsonIgnore]
        public ICollection<Booking>? Bookings { get; set; }
    }
}
