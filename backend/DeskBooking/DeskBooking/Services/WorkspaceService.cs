using AutoMapper;
using DeskBookingAPI.Data;
using DeskBookingAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DeskBookingAPI.Services
{
    public class WorkspaceService : IWorkspaceService
    {
        private readonly DeskBookingContext _context;
        private readonly IMapper _mapper;
      public WorkspaceService(DeskBookingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<WorkspaceDto>> Workspaces()
        {
            var workspacesModels = await _context.Workspaces.Include(w=>w.AvailabilityOptions).ToListAsync();
            List<WorkspaceDto> workspaces=new List<WorkspaceDto>();
            foreach (var workspace in workspacesModels)
            {
              var groupedOptions = workspace.AvailabilityOptions
                    .GroupBy(a=> new { a.Capacity, a.UnitType })
                    .Select(group => new AvailabilityOptionsDto
                    {
                        Capacity = group.Key.Capacity,
                        UnitType = group.Key.UnitType,
                        Quantity =group.Count()
                    }).ToList();
                var workspaceDto = new WorkspaceDto
                {
                    Id = workspace.Id,
                    Name = workspace.Name,
                    Description = workspace.Description,
                    Type = workspace.Type,
                    Amenities = workspace.Amenities,
                    Capacity = groupedOptions.Select(o => o.Capacity).Distinct().ToArray(),
                    AvailabilityOptions = groupedOptions
                };
                workspaces.Add(workspaceDto);
                workspaces.Reverse();
            }
            return workspaces;
        }

        public async Task<IEnumerable<WorkspaceDto>> WorkspacesByCoworking(Guid id)
        {
          var workspacesModels = await _context.Workspaces.Include(w=>w.AvailabilityOptions).Where(w=>w.CoworkingId==id).ToListAsync();
            List<WorkspaceDto> workspaces=new List<WorkspaceDto>();
            foreach (var workspace in workspacesModels)
            {
              var groupedOptions = workspace.AvailabilityOptions
                    .GroupBy(a=> new { a.Capacity, a.UnitType })
                    .Select(group => new AvailabilityOptionsDto
                    {
                        Capacity = group.Key.Capacity,
                        UnitType = group.Key.UnitType,
                        Quantity =group.Count()
                    }).ToList();
                var workspaceDto = new WorkspaceDto
                {
                    Id = workspace.Id,
                    Name = workspace.Name,
                    Description = workspace.Description,
                    Type = workspace.Type,
                    Amenities = workspace.Amenities,
                    Capacity = groupedOptions.Select(o => o.Capacity).Distinct().ToArray(),
                    AvailabilityOptions = groupedOptions
                };
                workspaces.Add(workspaceDto);
                workspaces.Reverse();
            }
            return workspaces;
        }

        public async Task<IEnumerable<WorkspacesListDto>> WorkspacesList()
        {
            var workspacesModels = await _context.Workspaces.ToListAsync();
            return  _mapper.Map<IEnumerable<WorkspacesListDto>>(workspacesModels).ToList();
        }
    }
}
