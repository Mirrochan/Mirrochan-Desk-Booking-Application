using DeskBookingAPI.Models;

namespace DeskBookingAPI.DTOs
{
    public class BookingResponseDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public Guid WorkspaceId { get; set; }
        public string WorkspaceName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PeopleCount { get; set; } = 0;
        public float Duration { get; set; } = 0;
    }
}
