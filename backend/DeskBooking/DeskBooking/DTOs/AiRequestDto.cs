using DeskBookingAPI.DTOs;

public class AiRequestDto
{
    public required string Question { get; set; } 
    public required string Bookings { get; set; }
}