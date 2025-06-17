
    namespace DeskBookingAPI.Models
    {
        public class BookingModel
        {
            public Guid Id { get; set; }
            public string UserName { get; set; } = string.Empty;
            public string UserEmail { get; set; } = string.Empty;
        public Guid WorkspaceId { get; set; }
        public Workspace Workspace { get; set; } = null!;
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid RoomId { get; set; }
        public RoomModel Room { get; set; } = null!;
       

    }
    }

