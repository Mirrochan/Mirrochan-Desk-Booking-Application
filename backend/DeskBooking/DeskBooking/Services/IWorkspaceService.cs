using DeskBookingAPI.DTOs;

namespace DeskBookingAPI.Services
{
    public interface IWorkspaceService
    {
      public  Task<IEnumerable<WorkspaceDto>> Workspaces();
    }
}
