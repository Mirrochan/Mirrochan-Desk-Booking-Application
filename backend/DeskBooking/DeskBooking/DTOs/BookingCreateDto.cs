using DeskBookingAPI.Models;

namespace DeskBookingAPI.DTOs
{
    public class BookingCreateDto
    {
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public Guid WorkspaceId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PeopleCount { get; set; } = 0;
        
    }
}
