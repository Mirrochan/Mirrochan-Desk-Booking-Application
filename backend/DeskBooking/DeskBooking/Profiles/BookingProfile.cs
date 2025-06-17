using AutoMapper;
using DeskBookingAPI.DTOs;
using DeskBookingAPI.Models;

namespace DeskBookingAPI.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
       CreateMap<BookingCreateDto, BookingModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDateUTC))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDateUTC));


        CreateMap<BookingModel, BookingCreateDto>()
            .ForMember(dest => dest.StartDateLocal, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDateLocal, opt => opt.MapFrom(src => src.EndDate));
      CreateMap<BookingUpdateDto, BookingModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDateUTC))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDateUTC));

       
            CreateMap<BookingModel, BookingUpdateDto>()
                .ForMember(dest => dest.StartDateLocal, opt => opt.MapFrom(src => src.StartDate.ToLocalTime()))
                .ForMember(dest => dest.EndDateLocal, opt => opt.MapFrom(src => src.EndDate.ToLocalTime()));


            CreateMap<BookingModel, BookingResponseDto>()
                .ForMember(dest => dest.WorkspaceName, opt => opt.MapFrom(src => src.Workspace.Name))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToLocalTime()))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.ToLocalTime()));


            CreateMap<BookingResponseDto, BookingModel>();


        }
    }
}
