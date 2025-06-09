using DeskBookingAPI.DTOs;
using DeskBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeskBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingResponseDto>>> GetBookings([FromQuery] Guid id)
        {
            var bookings = await _bookingService.GetBooking(id);
            return Ok(bookings);
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<Guid>> GetAllBookings()
        {
            try
            {
                var booking = await _bookingService.GetAllBookings();
                return Ok(booking);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<BookingResponseDto>> CreateBooking(BookingCreateDto bookingDTO)
        {
            try
            {
                var booking = await _bookingService.CreateBooking(bookingDTO);
                return Ok();
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            try
            {
                await _bookingService.DeleteBooking(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBooking(BookingUpdateDto updateBooking)
        {
            try
            {
                await _bookingService.UpdateBooking(updateBooking);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("last")]
        public async Task<ActionResult<BookingResponseDto>> GetLastBooking()
        {
            try
            {
                var booking = await _bookingService.GetLastBooking();
                return Ok(booking);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        }
}