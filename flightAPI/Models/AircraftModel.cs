﻿using System;
using System.ComponentModel.DataAnnotations;

namespace flightAPI.Models
{
    public class AircraftModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Capacity { get; set; }

        // Navigation property for Aircraft
        public ICollection<Aircraft> Aircrafts { get; set; } // Represents a one-to-many relationship with Aircraft
    }
}
