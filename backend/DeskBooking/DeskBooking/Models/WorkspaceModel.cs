namespace DeskBookingAPI.Models
{
    public enum WorkspaceType
    {
        OpenSpace,
        PrivateRoom,
        MeetingRoom
    }

    public class Workspace
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }
        public WorkspaceType Type { get; set; }
        public int[] Capacity { get; set; }
        public string[] Amenities { get; set; } =[];
        public List<BookingModel> Bookings { get; set; } = new();
        public List<RoomModel> AvailabilityOptions { get; set; } = new();
        public Workspace()
        {
            Id = Guid.NewGuid();
        }
    }
}