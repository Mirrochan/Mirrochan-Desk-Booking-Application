using DeskBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeskBookingAPI.Controllers
{
    public class WorkspaceController : Controller
    {
        readonly IWorkspaceService _workspaceService;
        public WorkspaceController(IWorkspaceService workspaceService)
        {
            _workspaceService = workspaceService;
        }

        [HttpGet]
        [Route("api/workspaces")]
        public async Task<IActionResult> GetWorkspaces()
        {
            var workspaces = await _workspaceService.Workspaces();
            return Ok(workspaces);

        }
        [HttpGet]
        [Route("api/workspacesList")]
        public async Task<IActionResult> GetWorkspacesList()
        {
            var workspaces = await _workspaceService.WorkspacesList();
            return Ok(workspaces);
        }
    }
}
