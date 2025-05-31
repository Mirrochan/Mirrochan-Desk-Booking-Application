using DeskBookingAPI.DTOs;

namespace DeskBookingAPI.Services
{
    public interface IBookingService
    {
        Task<Guid> CreateBooking(BookingCreateDto bookingDTO);
        Task DeleteBooking(Guid id);
        Task<BookingResponseDto> GetBooking(Guid id);
        Task<IEnumerable<BookingResponseDto>> GetBookings(string? email = null);
        Task UpdateBooking(BookingUpdateDto updateBooking);
    }
}
