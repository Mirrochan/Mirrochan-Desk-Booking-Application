using AutoMapper;
using DeskBookingAPI.DTOs;
using DeskBookingAPI.Models;

namespace DeskBookingAPI.Profiles
{
    public class WorkspaceProfile:Profile
    {
       public WorkspaceProfile() {

            CreateMap<WorkspaceDto, Workspace>();
            CreateMap<Workspace, WorkspaceDto>();

        }
    }
}
