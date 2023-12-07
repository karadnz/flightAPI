using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace flightAPI.Models
{
    public class Route
    {
        public int Id { get; set; }

        [Required]
        public int DepartureAirportId { get; set; }

        [JsonIgnore]
        public Airport DepartureAirport { get; set; }

        [Required]
        public int ArrivalAirportId { get; set; }

        [JsonIgnore]
        public Airport ArrivalAirport { get; set; }
    }
}

