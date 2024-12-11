using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hospital_V2.Models
{
    public class Patient
    {
        [Key]
        public int P_Id { get; set; }

        [Required]
        public string P_Name { get; set; }

        [Required]
        public string P_Age { get; set; }

        [Required]
        public string P_Gender { get; set; }


        [JsonIgnore]
        public ICollection<Booking>? Bookings { get; set; }
    }
}
