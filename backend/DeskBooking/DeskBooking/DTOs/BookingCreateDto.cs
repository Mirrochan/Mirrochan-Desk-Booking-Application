using System.ComponentModel.DataAnnotations;

public class BookingCreateDto
{
    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public Guid WorkspaceId { get; set; }
    
    [Required]
    public DateTime StartDateLocal { get; set; }
    
    [Required]
    public DateTime EndDateLocal { get; set; }
    
    public int PeopleCount { get; set; } = 0;
    
    public DateTime StartDateUTC => StartDateLocal.Kind == DateTimeKind.Unspecified 
        ? DateTime.SpecifyKind(StartDateLocal, DateTimeKind.Local).ToUniversalTime()
        : StartDateLocal.ToUniversalTime();
        
    public DateTime EndDateUTC => EndDateLocal.Kind == DateTimeKind.Unspecified 
        ? DateTime.SpecifyKind(EndDateLocal, DateTimeKind.Local).ToUniversalTime()
        : EndDateLocal.ToUniversalTime();
}