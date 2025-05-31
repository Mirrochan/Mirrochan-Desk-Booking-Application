using DeskBookingAPI.DTOs;
using FluentValidation;

namespace DeskBookingAPI.Validators
{
    public class BookingCreateValidator : AbstractValidator<BookingCreateDto>
    {
        public BookingCreateValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.UserEmail).EmailAddress();
            RuleFor(x => x.WorkspaceId).NotEmpty();
            RuleFor(x => x.StartDate).LessThan(x => x.EndDate).WithMessage("Start must be before End.");
            RuleFor(x => x.StartDate).GreaterThan(DateTime.Now).WithMessage("Cannot book in the past.");
        }
    }
}
