using AutoMapper;
using DeskBookingAPI.Data;
using DeskBookingAPI.DTOs;
using DeskBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DeskBookingAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly DeskBookingContext _context;
        private readonly IMapper _mapper;

        public BookingService(DeskBookingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> CreateBooking(BookingCreateDto bookingDTO)
        {
            var workspace = await _context.Workspaces.FindAsync(bookingDTO.WorkspaceId);
            if (workspace == null)
                throw new ArgumentException("Invalid workspace ID");

            ValidateBookingDuration(workspace.Type, bookingDTO.StartDate, bookingDTO.EndDate);

            var availabilityOption = await ValidateAvailability(
                bookingDTO.WorkspaceId,
                bookingDTO.StartDate,
                bookingDTO.EndDate,
                bookingDTO.PeopleCount
            );

            var booking = _mapper.Map<BookingModel>(bookingDTO);
            booking.RoomId = availabilityOption.Id;
            booking.Workspace = workspace;
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return booking.Id;
        }

        public async Task DeleteBooking(Guid id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                throw new KeyNotFoundException("Booking not found");

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateBooking(BookingUpdateDto updateBooking)
        {
            await DeleteBooking(updateBooking.Id);
            await CreateBooking(_mapper.Map<BookingCreateDto>(updateBooking));
            await _context.SaveChangesAsync();
        }
        public async Task<List<BookingResponseDto>> GetAllBookings()
        {
            List<BookingResponseDto> bookings = await _context.Bookings
    .Include(b => b.Workspace)
    .Include(b => b.Room)
    .Select(b=> new BookingResponseDto
    {
       Id= b.Id,
        UserName = b.UserName,
       UserEmail = b.UserEmail,
        WorkspaceId = b.Workspace.Id,
        WorkspaceName = b.Workspace.Name,
        StartDate = b.StartDate,
        EndDate = b.EndDate,
        PeopleCount=b.Room.Capacity,
        Duration = (float)(b.EndDate - b.StartDate).TotalHours
    }).ToListAsync();
            if (bookings == null)
                throw new KeyNotFoundException("Booking not found");
          

            return bookings;
        }

        public async Task<IEnumerable<BookingResponseDto>> GetBookings(string? email = null)
        {
            var query = await _context.Bookings.Include(b => b.Workspace).Where(b => b.UserEmail == email).ToListAsync();
            return  query
                .Select(b => _mapper.Map<BookingResponseDto>(b))  ;
        }

        private void ValidateBookingDuration(WorkspaceType type, DateTime start, DateTime end)
        {
            var duration = end - start;

            if (type == WorkspaceType.MeetingRoom && duration.TotalDays > 1)
                throw new ArgumentException("Meeting rooms can be booked for a maximum of 1 day");

            if ((type == WorkspaceType.OpenSpace || type == WorkspaceType.PrivateRoom) && duration.TotalDays > 30)
                throw new ArgumentException("Open spaces and private rooms can be booked for a maximum of 30 days");
            if (duration.TotalDays < 0)
            {
                throw new ArgumentException("Invalid dates");
            }
        }

        private async Task<WorkspaceAvailabilityOption> ValidateAvailability(Guid workspaceId, DateTime start, DateTime end, int peopleCount)
        {
            var suitableOptions = await _context.WorkspaceAvailabilityOptions
                .Where(o => o.WorkspaceId == workspaceId && o.Capacity >= peopleCount)
                .ToListAsync();

            foreach (var option in suitableOptions)
            {
                var isOverlapping = await _context.Bookings
                    .AnyAsync(b => b.RoomId == option.Id &&
                                   start < b.EndDate &&
                                   end > b.StartDate);

                if (!isOverlapping)
                {
                    return option;
                }
            }

            throw new InvalidOperationException("The selected workspace is not available for the specified time period and people count.");
        }
    }
}
