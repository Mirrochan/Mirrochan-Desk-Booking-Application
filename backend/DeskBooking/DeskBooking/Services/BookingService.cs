using AutoMapper;
using DeskBookingAPI.Data;
using DeskBookingAPI.DTOs;
using DeskBookingAPI.Models;
using DeskBookingAPI.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DeskBookingAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly DeskBookingContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<BookingCreateDto> _bookingCreateValidator;

        public BookingService(
            DeskBookingContext context,
            IMapper mapper,
            IValidator<BookingCreateDto> bookingCreateValidator)
        {
            _context = context;
            _mapper = mapper;
            _bookingCreateValidator = bookingCreateValidator;
        }

        public async Task<Guid> CreateBooking(BookingCreateDto bookingDTO)
        {
            var validationResult = await _bookingCreateValidator.ValidateAsync(bookingDTO);

            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException($"Validation failed: {errors}");
            }

            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);

            try
            {
                var availabilityOption = await ValidateAvailability(
                    bookingDTO.WorkspaceId,
                    bookingDTO.StartDate,
                    bookingDTO.EndDate,
                    bookingDTO.PeopleCount
                );

                var workspace = await _context.Workspaces.FindAsync(bookingDTO.WorkspaceId);

                var booking = _mapper.Map<BookingModel>(bookingDTO);
                booking.RoomId = availabilityOption.Id;
                booking.Workspace = workspace;

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return booking.Id;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    


    public async Task DeleteBooking(Guid id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                throw new KeyNotFoundException("Booking not found");

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBooking(BookingUpdateDto updateDto)
        {
            var booking = await _context.Bookings.FindAsync(updateDto.Id);
            if (booking == null)
                throw new KeyNotFoundException("Booking not found");

            var workspace = await _context.Workspaces.FindAsync(updateDto.WorkspaceId);
            if (workspace == null)
                throw new ArgumentException("Invalid workspace ID");

            ValidateBookingDates(updateDto.StartDate, updateDto.EndDate);
            ValidateBookingDuration(workspace.Type, updateDto.StartDate, updateDto.EndDate);

            var availabilityOption = await ValidateAvailability(
                updateDto.WorkspaceId,
                updateDto.StartDate,
                updateDto.EndDate,
                updateDto.PeopleCount
            );

            booking.UserName = updateDto.UserName;
            booking.UserEmail = updateDto.UserEmail;
            booking.StartDate = updateDto.StartDate;
            booking.EndDate = updateDto.EndDate;
            booking.RoomId = availabilityOption.Id;
            booking.WorkspaceId = updateDto.WorkspaceId;
            booking.Workspace = workspace;

            await _context.SaveChangesAsync();
        }

        public async Task<List<BookingResponseDto>> GetAllBookings()
        {
            return await _context.Bookings
                .Include(b => b.Workspace)
                .Include(b => b.Room)
                .Select(b => new BookingResponseDto
                {
                    Id = b.Id,
                    UserName = b.UserName,
                    UserEmail = b.UserEmail,
                    WorkspaceId = b.Workspace.Id,
                    WorkspaceName = b.Workspace.Name,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    PeopleCount = b.Room.Capacity,
                    Duration = (float)(b.EndDate - b.StartDate).TotalHours
                })
                .ToListAsync();
        }

        public async Task<BookingResponseDto> GetBooking(Guid id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Workspace)
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
                throw new KeyNotFoundException("Booking not found");

            return _mapper.Map<BookingResponseDto>(booking);
        }

        private void ValidateBookingDates(DateTime start, DateTime end)
        {
            if (start < DateTime.UtcNow.Date)
                throw new ArgumentException("Start date cannot be in the past");

            if (end < DateTime.UtcNow.Date)
                throw new ArgumentException("End date cannot be in the past");

            if (end <= start)
                throw new ArgumentException("End date must be after start date");
        }

        private void ValidateBookingDuration(WorkspaceType type, DateTime start, DateTime end)
        {
            var duration = end - start;

            if (type == WorkspaceType.MeetingRoom && duration.TotalDays > 1)
                throw new ArgumentException("Meeting rooms can be booked for a maximum of 1 day");

            if ((type == WorkspaceType.OpenSpace || type == WorkspaceType.PrivateRoom) && duration.TotalDays > 30)
                throw new ArgumentException("Open spaces and private rooms can be booked for a maximum of 30 days");
        }

        private async Task<RoomModel> ValidateAvailability(Guid workspaceId, DateTime start, DateTime end, int peopleCount)
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
                    return option;
            }

            throw new InvalidOperationException("The selected workspace is not available for the specified time period and people count.");
        }

        public async Task<BookingResponseDto> GetLastBooking()
        {
            var booking = await _context.Bookings
                .Include(b => b.Workspace)
                .Include(b => b.Room)
                .Where(b => b.EndDate >= DateTime.UtcNow)
                .OrderByDescending(b => b.CreatedAt)
                .Select(b => new BookingResponseDto
                {
                    Id = b.Id,
                    UserName = b.UserName,
                    UserEmail = b.UserEmail,
                    WorkspaceId = b.Workspace.Id,
                    WorkspaceName = b.Workspace.Name,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    PeopleCount = b.Room.Capacity,
                    Duration = (float)(b.EndDate - b.StartDate).TotalHours
                })
                .FirstOrDefaultAsync(); 

            if (booking == null)
                throw new KeyNotFoundException("No valid bookings found.");

            return booking; 
        }


    }
}
