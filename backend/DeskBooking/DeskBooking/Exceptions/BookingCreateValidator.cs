using DeskBookingAPI.DTOs;
using DeskBookingAPI.Data;
using DeskBookingAPI.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DeskBookingAPI.Validators
{
    public class BookingCreateValidator : AbstractValidator<BookingCreateDto>
    {
        public BookingCreateValidator(DeskBookingContext context)
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User name is required.");

            RuleFor(x => x.UserEmail)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not valid.");

            RuleFor(x => x.WorkspaceId)
                .NotEmpty().WithMessage("WorkspaceId is required.")
                .MustAsync(async (id, _) =>
                {
                    return await context.Workspaces.AnyAsync(w => w.Id == id);
                }).WithMessage("Workspace does not exist.");

            RuleFor(x => x.StartDateUTC)
                .GreaterThan(DateTime.Now)
                .WithMessage("Start date must be in the future.");

            RuleFor(x => x)
                .Must(x => x.EndDateUTC > x.StartDateUTC)
                .WithMessage("End date must be after start date.");

            RuleFor(x => x)
                .CustomAsync(async (dto, contextValidation, ct) =>
                {
                    var workspace = await context.Workspaces.FirstOrDefaultAsync(w => w.Id == dto.WorkspaceId, ct);
                    if (workspace != null)
                    {
                        var duration = dto.EndDateUTC - dto.StartDateUTC;

                        if (workspace.Type == WorkspaceType.MeetingRoom && duration.TotalDays > 1)
                        {
                            contextValidation.AddFailure("Meeting rooms can be booked for a maximum of 1 day.");
                        }

                        if ((workspace.Type == WorkspaceType.OpenSpace || workspace.Type == WorkspaceType.PrivateRoom) && duration.TotalDays > 30)
                        {
                            contextValidation.AddFailure("Open spaces and private rooms can be booked for a maximum of 30 days.");
                        }
                    }
                });
        }
    }
}
