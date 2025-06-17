using DeskBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;
using DeskBookingAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace DeskBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoworkingController : ControllerBase
    {
        private readonly  DeskBookingContext _context;
        
   readonly IWorkspaceService _workspaceService;
        public CoworkingController(DeskBookingContext context, IWorkspaceService workspaceService)
        {
            _context = context;
            _workspaceService = workspaceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var coworkings = await _context.Coworkings
                .Include(c => c.Workspaces)
                .ToListAsync();

            var result = coworkings.Select(c => new
            {
                c.Id,
                c.Name,
                c.Address,
                c.Description,
                WorkspaceSummary = c.Workspaces
                    .GroupBy(w => w.Type)
                    .ToDictionary(g => g.Key, g => g.Count())
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _workspaceService.WorkspacesByCoworking(id);
            return Ok(data);
        }
    }

    
}