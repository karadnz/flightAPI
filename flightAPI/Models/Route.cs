using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flightAPI.Models
{
    public class Route
    {
        public int Id { get; set; }

        [Required]
        public int DepartureAirportId { get; set; }

        [ForeignKey("DepartureAirportId")]
        public Airport DepartureAirport { get; set; }

        [Required]
        public int ArrivalAirportId { get; set; }

        [ForeignKey("ArrivalAirportId")]
        public Airport ArrivalAirport { get; set; }
    }
}
