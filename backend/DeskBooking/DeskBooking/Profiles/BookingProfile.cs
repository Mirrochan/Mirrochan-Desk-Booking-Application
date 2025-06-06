using AutoMapper;
using DeskBookingAPI.DTOs;
using DeskBookingAPI.Models;

namespace DeskBookingAPI.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<BookingUpdateDto, BookingModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<BookingModel, BookingUpdateDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<BookingCreateDto, BookingModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<BookingModel, BookingCreateDto>();
            CreateMap<BookingModel, BookingResponseDto>().
                ForMember(dest => dest.WorkspaceName, opt => opt.MapFrom(src => src.Workspace.Name));
            CreateMap<BookingResponseDto, BookingModel>();
     
            CreateMap<BookingUpdateDto, BookingCreateDto>();

        }
    }
}
