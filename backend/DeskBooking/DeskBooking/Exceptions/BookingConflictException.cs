namespace DeskBookingAPI.Exceptions
{
    public class BookingConflictException : InvalidOperationException
    {
        public BookingConflictException(string message) : base(message) { }
    }
}
