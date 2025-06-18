namespace DeskBookingAPI.Models
{
    public class RoomModel
    {
        public Guid Id { get; set; }
        public Guid WorkspaceId { get; set; }
        public Workspace Workspace { get; set; } = null!;
        public int Capacity { get; set; }
        public string UnitType { get; set; } = string.Empty;
        public ICollection<BookingModel>? Bookings { get; set; } 

    }
}
