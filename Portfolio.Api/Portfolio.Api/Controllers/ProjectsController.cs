using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Data;
using Portfolio.Api.DTOs;
using Portfolio.Api.Models;

namespace Portfolio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProjectsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all projects
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
    {
        var projects = await _context.Projects
            .Where(p => p.IsActive)
            .OrderBy(p => p.DisplayOrder)
            .ToListAsync();

        var projectDtos = _mapper.Map<List<ProjectDto>>(projects);
        return Ok(projectDtos);
    }

    /// <summary>
    /// Get project by id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProject(int id)
    {
        var project = await _context.Projects
            .Include(p => p.ProjectImages)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project == null)
        {
            return NotFound(new { message = $"Project with id {id} not found" });
        }

        var projectDto = _mapper.Map<ProjectDto>(project);
        return Ok(projectDto);
    }

    /// <summary>
    /// Get featured projects
    /// </summary>
    [HttpGet("featured")]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetFeaturedProjects()
    {
        var projects = await _context.Projects
            .Where(p => p.IsFeatured && p.IsActive)
            .OrderBy(p => p.DisplayOrder)
            .ToListAsync();

        var projectDtos = _mapper.Map<List<ProjectDto>>(projects);
        return Ok(projectDtos);
    }

    /// <summary>
    /// Create new project
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ProjectDto>> CreateProject(ProjectCreateDto projectCreateDto)
    {
        var project = _mapper.Map<Project>(projectCreateDto);
        project.CreatedAt = DateTime.UtcNow;
        project.IsActive = true;

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var projectDto = _mapper.Map<ProjectDto>(project);
        return CreatedAtAction(nameof(GetProject), new { id = project.Id }, projectDto);
    }

    /// <summary>
    /// Update project
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectDto>> UpdateProject(int id, ProjectUpdateDto projectUpdateDto)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound(new { message = $"Project with id {id} not found" });
        }

        _mapper.Map(projectUpdateDto, project);
        await _context.SaveChangesAsync();

        var projectDto = _mapper.Map<ProjectDto>(project);
        return Ok(projectDto);
    }

    /// <summary>
    /// Delete project
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound(new { message = $"Project with id {id} not found" });
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
