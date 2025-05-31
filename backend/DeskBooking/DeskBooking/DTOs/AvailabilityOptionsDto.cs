using DeskBookingAPI.Models;

namespace DeskBookingAPI.DTOs
{
    public class AvailabilityOptionsDto
    {
        public int Capacity { get; set; }
        public string UnitType { get; set; }
        public int Quantity { get; set; } = 0;
    }
}
