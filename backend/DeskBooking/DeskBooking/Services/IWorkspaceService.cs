using DeskBookingAPI.DTOs;

namespace DeskBookingAPI.Services
{
  public interface IWorkspaceService
  {
    public Task<IEnumerable<WorkspaceDto>> Workspaces();
    Task<IEnumerable<WorkspacesListDto>> WorkspacesList();
      Task<IEnumerable<WorkspaceDto>> WorkspacesByCoworking(Guid id);  
    }
}
