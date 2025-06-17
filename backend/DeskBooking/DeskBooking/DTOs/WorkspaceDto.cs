using DeskBookingAPI.Models;

namespace DeskBookingAPI.DTOs
{
    public class WorkspaceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public WorkspaceType Type { get; set; }
        public int[] Capacity { get; set; }
        public string[] Amenities { get; set; }
        public List<AvailabilityOptionsDto> AvailabilityOptions { get; set; }
    }
}
