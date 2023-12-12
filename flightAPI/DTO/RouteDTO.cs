using System;

namespace flightAPI.DTO
{
    public class RouteDTO
    {
        public int Id { get; set; }
        public int DepartureAirport { get; set; }
        public int ArrivalAirport { get; set; }
    }
}
