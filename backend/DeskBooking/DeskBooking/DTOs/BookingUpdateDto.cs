namespace DeskBookingAPI.DTOs
{
    public class BookingUpdateDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public Guid WorkspaceId { get; set; }
        public DateTime StartDateLocal { get; set; }
        public DateTime EndDateLocal { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int PeopleCount { get; set; } = 0;
             public DateTime StartDateUTC => StartDateLocal.ToUniversalTime();
        public DateTime EndDateUTC => EndDateLocal.ToUniversalTime();

    }
}
