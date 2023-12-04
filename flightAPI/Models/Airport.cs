﻿using System;
using System.ComponentModel.DataAnnotations;

namespace flightAPI.Models
{
    public class Airport
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }

    }
}

