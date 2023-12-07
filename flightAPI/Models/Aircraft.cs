using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flightAPI.Models
{
    public class Aircraft
    {
        public int Id { get; set; }

        // Foreign key for AircraftModel
        [Required]
        public int AircraftModelId { get; set; }

        // Navigation property for AircraftModel
        [ForeignKey("AircraftModelId")]
        public AircraftModel AircraftModel { get; set; }

        // Foreign key for Company
        [Required]
        public int CompanyId { get; set; }

        // Navigation property for Company
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
    }
}
