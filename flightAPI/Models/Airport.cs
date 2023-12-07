using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flightAPI.Models
{
    public class Airport
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }


        // Navigation properties for routes
        //[InverseProperty("OriginAirport")]
        //public ICollection<Route> OriginRoutes { get; set; }  // Routes that originate from this airport

        //[ForeignKey("DestinationAirportId")]
        //public ICollection<Route> DestinationRoutes { get; set; }  // Routes that end at this airport

    }
}

