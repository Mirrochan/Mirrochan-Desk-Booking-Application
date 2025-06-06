using DeskBookingAPI.DTOs;

namespace DeskBookingAPI.Services
{
    public interface IBookingService
    {
        Task<Guid> CreateBooking(BookingCreateDto bookingDTO);
        Task DeleteBooking(Guid id);
        Task<List<BookingResponseDto>> GetAllBookings();
        Task<IEnumerable<BookingResponseDto>> GetBookings(string? email = null);
        Task UpdateBooking(BookingUpdateDto updateBooking);
    }
}
